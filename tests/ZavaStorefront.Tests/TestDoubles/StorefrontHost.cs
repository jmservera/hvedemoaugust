using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using ZavaStorefront.Controllers;
using ZavaStorefront.Models;
using ZavaStorefront.Services;

namespace ZavaStorefront.Tests.TestDoubles;

/// <summary>
/// Builds the real services and controllers around a fake session, a real form collection,
/// a <see cref="TempDataDictionary"/>, and capturing loggers.
/// </summary>
internal sealed class StorefrontHost
{
    public const string CartSessionKey = "ShoppingCart";

    private readonly Dictionary<string, StringValues> _form = new();

    public StorefrontHost(FakeSession? session = null)
    {
        Session = session ?? new FakeSession();
        HttpContext = new DefaultHttpContext { Session = Session };
        HttpContext.Request.Method = "POST";
        TempData = new TempDataDictionary(HttpContext, new NullTempDataProvider());

        var accessor = new FixedHttpContextAccessor { HttpContext = HttpContext };
        Products = new ProductService();
        Cart = new CartService(accessor, Products);

        HomeLog = new CapturingLogger<HomeController>();
        CartLog = new CapturingLogger<CartController>();

        var actionContext = new ActionContext(HttpContext, new RouteData(), new ControllerActionDescriptor());
        Home = new HomeController(HomeLog, Products, Cart)
        {
            ControllerContext = new ControllerContext(actionContext),
            TempData = TempData,
        };
        CartController = new CartController(CartLog, Cart)
        {
            ControllerContext = new ControllerContext(actionContext),
            TempData = TempData,
        };
    }

    public FakeSession Session { get; }

    public DefaultHttpContext HttpContext { get; }

    public TempDataDictionary TempData { get; }

    public ProductService Products { get; }

    public CartService Cart { get; }

    public CapturingLogger<HomeController> HomeLog { get; }

    public CapturingLogger<CartController> CartLog { get; }

    public HomeController Home { get; }

    public CartController CartController { get; }

    public IEnumerable<LogEntry> AllLogEntries => HomeLog.Entries.Concat(CartLog.Entries);

    /// <summary>
    /// Sets a real urlencoded form so a test can post an absent key, an empty value, or a value.
    /// </summary>
    public void PostForm(params (string Key, string Value)[] fields)
    {
        _form.Clear();
        foreach (var (key, value) in fields)
        {
            _form[key] = value;
        }

        HttpContext.Request.ContentType = "application/x-www-form-urlencoded";
        HttpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(_form));
    }

    /// <summary>
    /// Writes the cart session JSON directly, independent of the service under test.
    /// </summary>
    public void SeedCart(params (int ProductId, int Quantity)[] lines)
    {
        var items = lines.Select(line => new CartItem
        {
            Product = Products.GetProductById(line.ProductId)!,
            Quantity = line.Quantity,
        }).ToList();
        Session.Set(CartSessionKey, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(items)));
    }

    /// <summary>
    /// Writes raw session JSON, for stale-snapshot cases.
    /// </summary>
    public void SeedRawCartJson(string json) =>
        Session.Set(CartSessionKey, Encoding.UTF8.GetBytes(json));

    /// <summary>
    /// Reads the cart as (product id, quantity) pairs in stored order, straight from the session.
    /// </summary>
    public CartLines ReadCart()
    {
        if (!Session.TryGetValue(CartSessionKey, out var bytes))
        {
            return new CartLines();
        }

        var items = JsonSerializer.Deserialize<List<CartItem>>(Encoding.UTF8.GetString(bytes))!;
        return new CartLines(items.Select(item => (item.Product.Id, item.Quantity)));
    }

    public string? RawCartJson() =>
        Session.TryGetValue(CartSessionKey, out var bytes) ? Encoding.UTF8.GetString(bytes) : null;

    private sealed class FixedHttpContextAccessor : IHttpContextAccessor
    {
        // The framework accessor keeps its context in a static AsyncLocal, so two hosts would overwrite each other.
        public HttpContext? HttpContext { get; set; }
    }

    private sealed class NullTempDataProvider : ITempDataProvider
    {
        public IDictionary<string, object> LoadTempData(HttpContext context) =>
            new Dictionary<string, object>();

        public void SaveTempData(HttpContext context, IDictionary<string, object> values)
        {
        }
    }
}
