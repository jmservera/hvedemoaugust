using Microsoft.AspNetCore.Mvc;
using ZavaStorefront.Tests.TestDoubles;

namespace ZavaStorefront.Tests;

/// <summary>
/// Pins the behavior that the inventory change must preserve: catalog names, prices and images,
/// remove, checkout, totals and badge count. Written and run against unmodified source first.
/// </summary>
public class BaselineBehaviorTests
{
    private readonly StorefrontHost _host = new();

    public static IEnumerable<object[]> CatalogSnapshot => new List<object[]>
    {
        new object[] { 1, "Wireless Noise-Canceling Headphones", 199.99m, "https://picsum.photos/id/1010/200/200" },
        new object[] { 2, "Smart Fitness Watch", 149.95m, "https://picsum.photos/id/1025/200/200" },
        new object[] { 3, "4K Action Camera", 129.50m, "https://picsum.photos/id/1003/200/200" },
        new object[] { 4, "Bluetooth Speaker", 89.99m, "https://picsum.photos/id/103/200/200" },
        new object[] { 5, "Laptop Stand", 39.95m, "https://picsum.photos/id/1050/200/200" },
        new object[] { 6, "Mechanical Keyboard", 99.00m, "https://picsum.photos/id/1070/200/200" },
        new object[] { 7, "Ergonomic Office Chair", 249.00m, "https://picsum.photos/id/1080/200/200" },
        new object[] { 8, "USB-C Hub Docking Station", 59.99m, "https://picsum.photos/id/109/200/200" },
        new object[] { 9, "Smart LED Light Bulb", 24.99m, "https://picsum.photos/id/110/200/200" },
        new object[] { 10, "Electric Standing Desk", 499.00m, "https://picsum.photos/id/111/200/200" },
    };

    [Fact]
    public void TC_S16_GivenCatalog_WhenListed_HasTenProductsWithIdsOneToTen()
    {
        var ids = _host.Products.GetAllProducts().Select(p => p.Id).ToList();

        Assert.Equal(Enumerable.Range(1, 10), ids);
    }

    [Theory]
    [MemberData(nameof(CatalogSnapshot))]
    public void TC_S16_GivenCatalog_WhenLookedUp_NamesAndPricesMatchBaseline(
        int id, string name, decimal price, string imageUrl)
    {
        var product = _host.Products.GetProductById(id)!;

        Assert.Equal((name, price), (product.Name, product.Price));
        Assert.NotNull(imageUrl);
    }

    [Theory]
    [MemberData(nameof(CatalogSnapshot))]
    public void TC_S17_GivenCatalog_WhenLookedUp_ImageUrlsMatchBaseline(
        int id, string name, decimal price, string imageUrl)
    {
        var product = _host.Products.GetProductById(id)!;

        Assert.Equal(imageUrl, product.ImageUrl);
        Assert.NotNull(name);
        Assert.True(price > 0);
    }

    [Fact]
    public void TC_S29_GivenCartWithItems_WhenCheckout_CartIsClearedAndRedirectsToSuccess()
    {
        _host.SeedCart((1, 2), (2, 1));

        var result = _host.CartController.Checkout();

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(("CheckoutSuccess", 0), (redirect.ActionName, _host.Cart.GetCartItemCount()));
    }

    [Fact]
    public void TC_S29_GivenEmptyCart_WhenCheckout_RedirectsToSuccessWithEmptyCart()
    {
        var result = _host.CartController.Checkout();

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(("CheckoutSuccess", 0), (redirect.ActionName, _host.Cart.GetCartItemCount()));
    }

    [Fact]
    public void TC_S30_GivenKnownLine_WhenRemoved_OnlyThatLineIsRemoved()
    {
        _host.SeedCart((1, 2), (2, 1));

        _host.Cart.RemoveFromCart(1);

        Assert.Equal(new List<(int, int)> { (2, 1) }, _host.ReadCart());
    }

    [Fact]
    public void TC_S30_GivenUnknownProductId_WhenRemoved_CartIsUnchanged()
    {
        _host.SeedCart((1, 2));

        _host.Cart.RemoveFromCart(999);

        Assert.Equal(new List<(int, int)> { (1, 2) }, _host.ReadCart());
    }

    [Fact]
    public void TC_S30_GivenRemoveAction_WhenPosted_RedirectsToCartIndex()
    {
        _host.SeedCart((1, 2));

        var result = _host.CartController.RemoveFromCart(1);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(("Index", 0), (redirect.ActionName, _host.Cart.GetCartItemCount()));
    }

    [Fact]
    public void TC_S30_GivenCartLines_WhenTotalled_TotalIsPriceTimesQuantity()
    {
        _host.SeedCart((1, 2), (5, 3));

        Assert.Equal(199.99m * 2 + 39.95m * 3, _host.Cart.GetCartTotal());
    }

    [Fact]
    public void TC_S30_GivenCartLines_WhenCounted_BadgeCountIsSumOfQuantities()
    {
        _host.SeedCart((1, 2), (5, 3));

        Assert.Equal(5, _host.CartController.GetCartCount());
    }

    [Fact]
    public void TC_S30_GivenEmptyCart_WhenTotalledAndCounted_BothAreZero()
    {
        Assert.Equal((0m, 0), (_host.Cart.GetCartTotal(), _host.Cart.GetCartItemCount()));
    }
}
