using ZavaStorefront.Models;
using System.Text.Json;

namespace ZavaStorefront.Services
{
    public class CartService
    {
        private const string CartSessionKey = "ShoppingCart";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ProductService _productService;

        public CartService(IHttpContextAccessor httpContextAccessor, ProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
        }

        public List<CartItem> GetCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString(CartSessionKey);

            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }

            return JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }

        /// <summary>
        /// Gets the current catalog availability for a product. Unknown products have no availability.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns>The available quantity, or <see langword="null"/> when the product is unknown.</returns>
        public int? GetAvailableQuantity(int productId)
        {
            return _productService.GetProductById(productId)?.AvailableQuantity;
        }

        /// <summary>
        /// Adds a quantity (cumulative) to the cart if the inclusive stock limit allows it.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <param name="quantity">The parsed quantity; omitted means one.</param>
        /// <returns>The outcome. Every rejection leaves the cart unchanged.</returns>
        public CartOperationResult AddToCart(int productId, QuantityInput quantity)
        {
            return Apply(productId, quantity, isAdd: true);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.Product.Id == productId);

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }
        }

        /// <summary>
        /// Sets an absolute quantity on an existing line if the inclusive stock limit allows it.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <param name="quantity">The parsed quantity; omitted is invalid, and zero removes the line.</param>
        /// <returns>The outcome. Every rejection leaves the cart unchanged.</returns>
        public CartOperationResult UpdateQuantity(int productId, QuantityInput quantity)
        {
            return Apply(productId, quantity, isAdd: false);
        }

        public void ClearCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.Remove(CartSessionKey);
        }

        public int GetCartItemCount()
        {
            var cart = GetCart();
            return cart.Sum(item => item.Quantity);
        }

        public decimal GetCartTotal()
        {
            var cart = GetCart();
            return cart.Sum(item => item.Product.Price * item.Quantity);
        }

        /// <summary>
        /// Applies one ordered rule to both Add (cumulative) and Update (absolute) and stops at the first matching step:
        /// unknown product, invalid quantity, Add default of one, Update on an absent line (silent no-op), explicit
        /// zero (remove), zero availability, inclusive over-limit, then store. Availability always comes from the
        /// current catalog, never from the session snapshot.
        /// </summary>
        private CartOperationResult Apply(int productId, QuantityInput input, bool isAdd)
        {
            var requested = input.Kind == QuantityInputKind.Omitted && isAdd ? 1 : input.Value;
            var product = _productService.GetProductById(productId);

            if (product == null)
            {
                return CartOperationResult.OutOfStock(null, requested);
            }

            var available = product.AvailableQuantity;

            if (input.Kind == QuantityInputKind.Invalid || (input.Kind == QuantityInputKind.Omitted && !isAdd))
            {
                return CartOperationResult.InvalidQuantity(available, requested);
            }

            var quantity = requested ?? 1;
            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(item => item.Product.Id == productId);

            if (!isAdd && existingItem == null)
            {
                return CartOperationResult.NoOp(available, quantity);
            }

            if (quantity == 0)
            {
                if (existingItem == null)
                {
                    return CartOperationResult.NoOp(available, quantity);
                }

                cart.Remove(existingItem);
                SaveCart(cart);
                return CartOperationResult.Removed(available);
            }

            if (available <= 0)
            {
                return CartOperationResult.OutOfStock(available, quantity);
            }

            long resulting = quantity;
            if (isAdd && existingItem != null)
            {
                resulting += existingItem.Quantity;
            }

            if (resulting > available)
            {
                return CartOperationResult.OverLimit(available, quantity);
            }

            if (existingItem != null)
            {
                existingItem.Quantity = (int)resulting;
            }
            else
            {
                cart.Add(new CartItem
                {
                    Product = product,
                    Quantity = (int)resulting
                });
            }

            SaveCart(cart);
            return CartOperationResult.Succeeded(available, quantity);
        }

        private void SaveCart(List<CartItem> cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = JsonSerializer.Serialize(cart);
            session.SetString(CartSessionKey, cartJson);
        }
    }
}
