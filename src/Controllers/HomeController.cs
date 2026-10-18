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
    public IActionResult AddToCart(int productId, string? quantity)
    {
        var input = InventoryFeedback.ParseQuantity(Request, quantity);
        var result = _cartService.AddToCart(productId, input);

        if (result.IsRejection)
        {
            InventoryFeedback.ReportRejection(_logger, TempData, CartOperation.Add, productId, result);
        }
        else if (result.Outcome == CartOperationOutcome.Succeeded)
        {
            var product = _productService.GetProductById(productId);
            _logger.LogInformation("Adding product {ProductId} ({ProductName}) to cart", productId, product?.Name);
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
