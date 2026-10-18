using ZavaStorefront.Services;
using ZavaStorefront.Tests.TestDoubles;

namespace ZavaStorefront.Tests;

public class InventoryRulesAndCatalogTests
{
    private readonly ProductService _products = new();

    [Theory]
    [InlineData(0, StockStatus.OutOfStock)]
    [InlineData(1, StockStatus.LowStock)]
    [InlineData(5, StockStatus.LowStock)]
    [InlineData(6, StockStatus.InStock)]
    [InlineData(25, StockStatus.InStock)]
    public void TC_S14_GivenAvailableQuantity_WhenStatusDetermined_ThresholdIsFiveInclusiveAndZeroIsOut(
        int available, StockStatus expected)
    {
        Assert.Equal(expected, InventoryRules.GetStatus(available));
    }

    [Fact]
    public void TC_S14_GivenThreshold_WhenRead_IsFive()
    {
        Assert.Equal(5, InventoryRules.LowStockThreshold);
    }

    [Fact]
    public void TC_S15_GivenSeededCatalog_WhenListed_HasTenProductsCoveringEveryStateAndBoundary()
    {
        var quantities = _products.GetAllProducts().Select(p => p.AvailableQuantity).ToList();

        Assert.Equal(10, quantities.Count);
        Assert.Subset(quantities.ToHashSet(), new HashSet<int> { 0, 1, 5, 6 });
    }

    [Fact]
    public void TC_S15_GivenSeededCatalog_WhenStatusesComputed_AllThreeStatesArePresent()
    {
        var statuses = _products.GetAllProducts()
            .Select(p => InventoryRules.GetStatus(p.AvailableQuantity))
            .ToHashSet();

        Assert.Equal(3, statuses.Count);
    }

    [Fact]
    public void TC_S18_GivenStatusTexts_WhenFormatted_AreExactlyTheApprovedCopy()
    {
        var texts = new[]
        {
            InventoryRules.GetStatusText(25),
            InventoryRules.GetStatusText(3),
            InventoryRules.GetStatusText(0),
        };

        Assert.Equal(new[] { "In-stock", "Only 3 left", "Out of stock" }, texts);
    }

    [Theory]
    [InlineData(5, "Only 5 left")]
    [InlineData(1, "Only 1 left")]
    [InlineData(6, "In-stock")]
    public void TC_S18_GivenBoundaryQuantity_WhenFormatted_CountAppearsOnlyForLowStock(int available, string expected)
    {
        Assert.Equal(expected, InventoryRules.GetStatusText(available));
    }

    [Fact]
    public void TC_S18_GivenOverstockAndInvalidTexts_WhenRead_AreExactlyTheApprovedCopy()
    {
        Assert.Equal(
            new[]
            {
                "Stock unavailable. Available quantity: 4. Reduce the quantity.",
                "Enter a valid whole-number quantity of zero or more.",
                "Out of stock",
            },
            new[]
            {
                InventoryRules.GetOverstockText(4),
                InventoryRules.InvalidQuantityText,
                InventoryRules.OutOfStockText,
            });
    }

    [Fact]
    public void TC_S15_GivenCatalog_WhenInspected_StockIsSyntheticAndNeverSerializedIntoSession()
    {
        var host = new StorefrontHost();
        host.SeedCart((3, 2));

        Assert.DoesNotContain("AvailableQuantity", host.RawCartJson());
    }
}
