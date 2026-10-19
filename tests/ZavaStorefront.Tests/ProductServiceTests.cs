using ZavaStorefront.Models;
using ZavaStorefront.Services;

namespace ZavaStorefront.Tests;

public sealed class ProductServiceTests
{
    [Theory]
    [InlineData(0, "Out of stock", true)]
    [InlineData(1, "1 left in stock", false)]
    [InlineData(5, "5 left in stock", false)]
    [InlineData(6, "In stock", false)]
    public void GivenAvailability_WhenDescribingStock_AppliesDisclosurePolicy(int quantity, string text, bool unavailable)
    {
        var product = new Product { AvailableQuantity = quantity };

        Assert.Equal(text, product.StockDescription);
        Assert.Equal(unavailable, product.IsOutOfStock);
    }

    [Fact]
    public void GivenCatalog_WhenInspected_PreservesAllPricesImagesAndRequiredStockFixtures()
    {
        var products = new ProductService().GetAllProducts();
        var prices = new[] { 199.99m, 149.95m, 129.50m, 89.99m, 39.95m, 99.00m, 249.00m, 59.99m, 24.99m, 499.00m };
        var imageIds = new[] { 1010, 1025, 1003, 103, 1050, 1070, 1080, 109, 110, 111 };

        Assert.Equal(10, products.Count);
        Assert.Equal(Enumerable.Range(1, 10), products.Select(p => p.Id));
        Assert.Equal(prices, products.Select(p => p.Price));
        Assert.Equal(imageIds.Select(id => $"https://picsum.photos/id/{id}/200/200"), products.Select(p => p.ImageUrl));
        Assert.All(products, p => Assert.True(p.AvailableQuantity >= 0));
        Assert.All(new[] { 0, 1, 3, 5, 6 }, stock => Assert.Contains(products, p => p.AvailableQuantity == stock));
    }
}
