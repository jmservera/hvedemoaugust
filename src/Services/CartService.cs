using ZavaStorefront.Models;
using System.Text.Json;

namespace ZavaStorefront.Services
{
    public class CartService
    {
        private const string CartSessionKey = "ShoppingCart";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CartService> _logger;
        private readonly ProductService _productService;

        public CartService(IHttpContextAccessor httpContextAccessor, ProductService productService,
            ILogger<CartService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
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

        public CartMutationResult AddToCart(int productId, int? quantity = 1)
        {
            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                return RejectMissingProduct(productId, quantity, "add");
            }

            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(item => item.Product.Id == productId);
            var resultingQuantity = (long)(existingItem?.Quantity ?? 0) + quantity.GetValueOrDefault();
            var rejection = ValidateQuantity(product, quantity, resultingQuantity, "add");
            if (rejection != null)
            {
                return rejection;
            }

            if (existingItem != null)
            {
                existingItem.Quantity = (int)resultingQuantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity.GetValueOrDefault()
                });
            }

            SaveCart(cart);
            return new CartMutationResult(true, productId);
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

        public CartMutationResult UpdateQuantity(int productId, int? quantity)
        {
            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                return RejectMissingProduct(productId, quantity, "update");
            }

            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.Product.Id == productId);
            if (item == null)
            {
                _logger.LogWarning(
                    "Cart mutation rejected: {Operation} product {ProductId}, requested {RequestedQuantity}, reason {Reason}",
                    "update", productId, quantity, "cart-item-not-found");
                return new CartMutationResult(false, productId,
                    "This item is no longer in your cart. Return to the catalog to add it again.");
            }

            if (quantity <= 0)
            {
                cart.Remove(item);
                SaveCart(cart);
                return new CartMutationResult(true, productId, "Item removed from your cart.");
            }

            var rejection = ValidateQuantity(product, quantity, quantity.GetValueOrDefault(), "update");
            if (rejection != null)
            {
                return rejection;
            }

            item.Quantity = quantity.GetValueOrDefault();
            SaveCart(cart);
            return new CartMutationResult(true, productId);
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

        private CartMutationResult RejectMissingProduct(int productId, int? quantity, string operation)
        {
            _logger.LogWarning(
                "Cart mutation rejected: {Operation} product {ProductId}, requested {RequestedQuantity}, reason {Reason}",
                operation, productId, quantity, "product-not-found");
            return new CartMutationResult(false, productId,
                "This product is unavailable. Return to the catalog and choose another product.");
        }

        private CartMutationResult? ValidateQuantity(Product product, int? requestedQuantity,
            long resultingQuantity, string operation)
        {
            if (requestedQuantity > 0 && resultingQuantity <= product.AvailableQuantity)
            {
                return null;
            }

            var reason = requestedQuantity is null or <= 0 ? "invalid-quantity" : "inventory-limit";
            _logger.LogWarning(
                "Cart mutation rejected: {Operation} product {ProductId}, requested {RequestedQuantity}, resulting {ResultingQuantity}, available {AvailableQuantity}, reason {Reason}",
                operation, product.Id, requestedQuantity, resultingQuantity, product.AvailableQuantity, reason);

            var message = requestedQuantity is null or <= 0
                ? "Choose a quantity of at least 1. Your cart has not changed."
                : product.IsOutOfStock
                    ? "0 available. This product is out of stock. Your cart has not changed. Choose another product."
                    : $"Only {product.AvailableQuantity} available. Your cart has not changed. Choose a total quantity of {product.AvailableQuantity} or fewer.";

            return new CartMutationResult(false, product.Id, message);
        }

        private void SaveCart(List<CartItem> cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = JsonSerializer.Serialize(cart);
            session.SetString(CartSessionKey, cartJson);
        }
    }
}
