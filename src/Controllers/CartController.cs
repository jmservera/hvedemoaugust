using Microsoft.AspNetCore.Mvc;
using ZavaStorefront.Services;

namespace ZavaStorefront.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly CartService _cartService;

        public CartController(ILogger<CartController> logger, CartService cartService)
        {
            _logger = logger;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Loading cart page with {ItemCount} items", _cartService.GetCartItemCount());
            var cart = _cartService.GetCart();
            ViewBag.CartTotal = _cartService.GetCartTotal();

            // Stock is read from the current catalog, never from the session snapshot of each line.
            var availability = new Dictionary<int, int>();
            foreach (var item in cart)
            {
                availability[item.Product.Id] = _cartService.GetAvailableQuantity(item.Product.Id) ?? 0;
            }

            ViewBag.Availability = availability;
            return View(cart);
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, string? quantity)
        {
            _logger.LogInformation("Updating quantity for product {ProductId}", productId);
            var input = InventoryFeedback.ParseQuantity(Request, quantity);
            var result = _cartService.UpdateQuantity(productId, input);
            InventoryFeedback.ReportRejection(_logger, TempData, CartOperation.Update, productId, result);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            _logger.LogInformation("Removing product {ProductId} from cart", productId);
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            var itemCount = _cartService.GetCartItemCount();
            var total = _cartService.GetCartTotal();
            _logger.LogInformation("Processing checkout for {ItemCount} items, total: {Total:C}", itemCount, total);
            _cartService.ClearCart();
            return RedirectToAction("CheckoutSuccess");
        }

        public IActionResult CheckoutSuccess()
        {
            _logger.LogInformation("Displaying checkout success page");
            return View();
        }

        public int GetCartCount()
        {
            return _cartService.GetCartItemCount();
        }
    }
}
