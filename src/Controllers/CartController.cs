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
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantity(int? productId, int? quantity)
        {
            if (!ModelState.IsValid)
            {
                quantity = null;
            }
            var result = _cartService.UpdateQuantity(productId.GetValueOrDefault(), quantity);
            if (result.Message != null)
            {
                TempData["CartMessage"] = result.Message;
                TempData["CartProductId"] = result.ProductId;
                TempData["CartRejected"] = !result.Succeeded;
            }
            if (result.Succeeded)
            {
                _logger.LogInformation("Updated product {ProductId} quantity to {Quantity}", result.ProductId, quantity);
            }
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
