using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ZavaStorefront.Models;
using ZavaStorefront.Services;

namespace ZavaStorefront.Tests;

public sealed class CartServiceTests : IDisposable
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly TestLogProvider _logs = new();
    private readonly ProductService _products = new();
    private readonly TestSession _session = new();
    private readonly CartService _sut;

    public CartServiceTests()
    {
        _loggerFactory = LoggerFactory.Create(builder => builder.AddProvider(_logs));
        _sut = new CartService(new HttpContextAccessor
        {
            HttpContext = new DefaultHttpContext { Session = _session }
        }, _products, _loggerFactory.CreateLogger<CartService>());
    }

    public void Dispose() => _loggerFactory.Dispose();

    [Theory]
    [InlineData(0, 3, true, 3)]
    [InlineData(2, 1, true, 3)]
    [InlineData(0, 4, false, 0)]
    [InlineData(3, 1, false, 3)]
    [InlineData(2, int.MaxValue, false, 2)]
    [InlineData(0, 0, false, 0)]
    [InlineData(2, 0, false, 2)]
    [InlineData(2, -1, false, 2)]
    [InlineData(2, null, false, 2)]
    public void GivenExistingQuantity_WhenAdding_EnforcesTotalAndPreservesOtherLines(
        int existing, int? requested, bool accepted, int expected)
    {
        _sut.AddToCart(5, 2);
        if (existing > 0)
        {
            _sut.AddToCart(1, existing);
        }
        var before = _session.GetString("ShoppingCart");

        var result = _sut.AddToCart(1, requested);

        Assert.Equal(accepted, result.Succeeded);
        Assert.Equal(expected, _sut.GetCart().SingleOrDefault(i => i.Product.Id == 1)?.Quantity ?? 0);
        Assert.Equal(2, _sut.GetCart().Single(i => i.Product.Id == 5).Quantity);
        if (!accepted)
        {
            Assert.Equal(before, _session.GetString("ShoppingCart"));
        }
    }

    [Theory]
    [InlineData(3, true, 3)]
    [InlineData(4, false, 2)]
    [InlineData(int.MaxValue, false, 2)]
    [InlineData(0, true, 0)]
    [InlineData(-1, true, 0)]
    [InlineData(null, false, 2)]
    public void GivenExistingLine_WhenUpdating_EnforcesLimitOrRemovesOnlyThatLine(
        int? quantity, bool accepted, int expected)
    {
        _sut.AddToCart(1, 2);
        _sut.AddToCart(5, 2);
        var before = _session.GetString("ShoppingCart");

        var result = _sut.UpdateQuantity(1, quantity);

        Assert.Equal(accepted, result.Succeeded);
        Assert.Equal(expected, _sut.GetCart().SingleOrDefault(i => i.Product.Id == 1)?.Quantity ?? 0);
        Assert.Equal(2, _sut.GetCart().Single(i => i.Product.Id == 5).Quantity);
        if (!accepted)
        {
            Assert.Equal(before, _session.GetString("ShoppingCart"));
        }
        if (quantity == 0)
        {
            Assert.Equal("Item removed from your cart.", result.Message);
        }
    }

    [Theory]
    [InlineData("add", 0)]
    [InlineData("update", 0)]
    [InlineData("add", -1)]
    [InlineData("update", -1)]
    [InlineData("add", 999)]
    [InlineData("update", 999)]
    public void GivenInvalidProduct_WhenMutating_ReturnsNeutralCopyAndOmitsUnknownAvailability(
        string operation, int productId)
    {
        _sut.AddToCart(1);
        var before = _session.GetString("ShoppingCart");

        var result = operation == "add" ? _sut.AddToCart(productId) : _sut.UpdateQuantity(productId, 2);

        Assert.False(result.Succeeded);
        Assert.Equal("This product is unavailable. Return to the catalog and choose another product.", result.Message);
        Assert.Equal(before, _session.GetString("ShoppingCart"));
        var log = Assert.Single(_logs.Entries);
        Assert.Equal(LogLevel.Warning, log.Level);
        Assert.Equal(operation, log.Fields["Operation"]);
        Assert.Equal("product-not-found", log.Fields["Reason"]);
        Assert.Equal(productId, log.Fields["ProductId"]);
        Assert.False(log.Fields.ContainsKey("AvailableQuantity"));
        Assert.Equal(new[] { "Operation", "ProductId", "Reason", "RequestedQuantity", "{OriginalFormat}" },
            log.Fields.Keys.OrderBy(k => k, StringComparer.Ordinal).ToArray());
        Assert.DoesNotContain(_session.Id, log.Message);
        Assert.DoesNotContain(before!, log.Message);
    }

    [Theory]
    [InlineData("add")]
    [InlineData("update")]
    public void GivenInventoryRejection_WhenLogged_RecordsOnlyPermittedStructuredContext(string operation)
    {
        _sut.AddToCart(1, 2);

        var result = operation == "add" ? _sut.AddToCart(1, 2) : _sut.UpdateQuantity(1, 4);

        Assert.False(result.Succeeded);
        Assert.Equal("Only 3 available. Your cart has not changed. Choose a total quantity of 3 or fewer.", result.Message);
        var log = Assert.Single(_logs.Entries);
        Assert.Equal(LogLevel.Warning, log.Level);
        Assert.Equal(operation, log.Fields["Operation"]);
        Assert.Equal(1, log.Fields["ProductId"]);
        Assert.Equal(operation == "add" ? 2 : 4, log.Fields["RequestedQuantity"]);
        Assert.Equal(3, log.Fields["AvailableQuantity"]);
        Assert.Equal("inventory-limit", log.Fields["Reason"]);
        Assert.Equal(new[] { "AvailableQuantity", "Operation", "ProductId", "Reason", "RequestedQuantity", "ResultingQuantity", "{OriginalFormat}" },
            log.Fields.Keys.OrderBy(k => k, StringComparer.Ordinal).ToArray());
        Assert.DoesNotContain(_session.Id, log.Message);
    }

    [Theory]
    [InlineData("add")]
    [InlineData("update")]
    public void GivenStaleSessionAvailability_WhenMutating_UsesCurrentCatalogInstead(string operation)
    {
        _sut.AddToCart(1, 2);
        var cart = _sut.GetCart();
        cart[0].Product.AvailableQuantity = 100;
        _session.SetString("ShoppingCart", JsonSerializer.Serialize(cart));
        var before = _session.GetString("ShoppingCart");

        var result = operation == "add" ? _sut.AddToCart(1, 2) : _sut.UpdateQuantity(1, 4);

        Assert.False(result.Succeeded);
        Assert.Equal(before, _session.GetString("ShoppingCart"));
    }

    [Fact]
    public void GivenOutOfStock_WhenAdding_RejectsWithExactZeroAvailability()
    {
        var result = _sut.AddToCart(2);

        Assert.False(result.Succeeded);
        Assert.StartsWith("0 available.", result.Message);
        Assert.Empty(_sut.GetCart());
        Assert.Equal(0, Assert.Single(_logs.Entries).Fields["AvailableQuantity"]);
    }

    [Fact]
    public void GivenNoLine_WhenUpdating_ReturnsExplicitRecovery()
    {
        var result = _sut.UpdateQuantity(1, 2);

        Assert.False(result.Succeeded);
        Assert.Contains("no longer in your cart", result.Message);
        Assert.Empty(_sut.GetCart());
        Assert.Equal("cart-item-not-found", Assert.Single(_logs.Entries).Fields["Reason"]);
    }

    [Fact]
    public void GivenNoQuantity_WhenAdding_PreservesSingleUnitDefault()
    {
        Assert.True(_sut.AddToCart(1).Succeeded);
        Assert.Equal(1, Assert.Single(_sut.GetCart()).Quantity);
    }

    [Fact]
    public void GivenCart_WhenRemovingAndClearing_PreservesCatalogAvailability()
    {
        var stock = _products.GetAllProducts().Select(p => p.AvailableQuantity).ToArray();
        _sut.AddToCart(1);
        _sut.AddToCart(5);

        _sut.RemoveFromCart(1);

        Assert.Equal(5, Assert.Single(_sut.GetCart()).Product.Id);
        _sut.ClearCart();
        Assert.Empty(_sut.GetCart());
        Assert.Equal(stock, _products.GetAllProducts().Select(p => p.AvailableQuantity));
    }
}
