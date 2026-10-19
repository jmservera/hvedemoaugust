using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ZavaStorefront.Models;
using ZavaStorefront.Services;

namespace ZavaStorefront.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ProductService _productService;
    private readonly CartService _cartService;

    public HomeController(ILogger<HomeController> logger, ProductService productService, CartService cartService)
    {
        _logger = logger;
        _productService = productService;
        _cartService = cartService;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Loading products page");
        var products = _productService.GetAllProducts();
        return View(products);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddToCart(int? productId, int? quantity = 1)
    {
        if (!ModelState.IsValid)
        {
            quantity = null;
        }
        var result = _cartService.AddToCart(productId.GetValueOrDefault(), quantity);
        if (result.Message != null)
        {
            TempData["CartMessage"] = result.Message;
            TempData["CartProductId"] = result.ProductId;
            TempData["CartRejected"] = !result.Succeeded;
        }
        if (result.Succeeded)
        {
            _logger.LogInformation("Added product {ProductId} to cart", result.ProductId);
        }

        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
