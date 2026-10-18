using ZavaStorefront.Services;
using ZavaStorefront.Tests.TestDoubles;

namespace ZavaStorefront.Tests;

/// <summary>
/// Exercises the ordered, bounded Add and Update rule against the seeded catalog
/// (product 1 has 0 in stock, 2 has 1, 3 has 5, 4 has 6, 7 has 3).
/// </summary>
public class CartServiceTests
{
    private readonly StorefrontHost _host = new();

    private CartOperationResult Run(bool isAdd, int productId, QuantityInput input) =>
        isAdd ? _host.Cart.AddToCart(productId, input) : _host.Cart.UpdateQuantity(productId, input);

    private static CartLines Lines(params (int, int)[] lines) => new(lines);

    [Fact]
    public void TC_S01_GivenStockFiveAndEmptyCart_WhenAddFive_IsAccepted()
    {
        var result = _host.Cart.AddToCart(3, QuantityInput.Valid(5));

        Assert.Equal((CartOperationOutcome.Succeeded, Lines((3, 5))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S02_GivenLineOfThreeAndStockFive_WhenAddThree_IsOverLimitAndLineStaysThree()
    {
        _host.SeedCart((3, 3));

        var result = _host.Cart.AddToCart(3, QuantityInput.Valid(3));

        Assert.Equal((CartOperationOutcome.OverLimit, Lines((3, 3))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S03_GivenLineEqualToStock_WhenAddOne_IsOverLimitAndCartUnchanged()
    {
        _host.SeedCart((3, 5));

        var result = _host.Cart.AddToCart(3, QuantityInput.Valid(1));

        Assert.Equal((CartOperationOutcome.OverLimit, Lines((3, 5))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S04_GivenLine_WhenUpdateToExactlyStock_IsAccepted()
    {
        _host.SeedCart((3, 2));

        var result = _host.Cart.UpdateQuantity(3, QuantityInput.Valid(5));

        Assert.Equal((CartOperationOutcome.Succeeded, Lines((3, 5))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S05_GivenLine_WhenUpdateToStockPlusOne_IsOverLimitAndPriorQuantityKept()
    {
        _host.SeedCart((3, 2));

        var result = _host.Cart.UpdateQuantity(3, QuantityInput.Valid(6));

        Assert.Equal((CartOperationOutcome.OverLimit, Lines((3, 2))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S06_GivenMultiLineCart_WhenAddAndUpdateAreRejected_EveryLineAndOrderAreIdentical()
    {
        _host.SeedCart((3, 2), (2, 1), (7, 1));
        var before = _host.ReadCart();

        var add = _host.Cart.AddToCart(3, QuantityInput.Valid(4));
        var afterAdd = _host.ReadCart();
        var update = _host.Cart.UpdateQuantity(7, QuantityInput.Valid(4));
        var afterUpdate = _host.ReadCart();

        Assert.Equal(
            (CartOperationOutcome.OverLimit, CartOperationOutcome.OverLimit, before, before),
            (add.Outcome, update.Outcome, afterAdd, afterUpdate));
    }

    [Theory]
    [InlineData(true, 0)]
    [InlineData(true, -1)]
    [InlineData(true, 999)]
    [InlineData(false, 0)]
    [InlineData(false, -1)]
    [InlineData(false, 999)]
    public void TC_S07_S08_GivenInvalidProductId_WhenAddOrUpdate_IsOutOfStockAndCartUnchanged(bool isAdd, int productId)
    {
        _host.SeedCart((3, 2));

        var result = Run(isAdd, productId, QuantityInput.Valid(1));

        Assert.Equal((CartOperationOutcome.OutOfStock, Lines((3, 2))), (result.Outcome, _host.ReadCart()));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TC_S09_GivenInvalidProductId_WhenAddOrUpdate_NoExceptionAndOnlyApprovedTextIsExposed(bool isAdd)
    {
        var result = Run(isAdd, 999, QuantityInput.Valid(1));

        Assert.Equal("Out of stock", result.Message);
    }

    [Fact]
    public void TC_S10_GivenLinePresent_WhenAddZero_LineIsRemoved()
    {
        _host.SeedCart((3, 2), (2, 1));

        var result = _host.Cart.AddToCart(3, QuantityInput.Valid(0));

        Assert.Equal((CartOperationOutcome.Removed, Lines((2, 1))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S10_GivenLineAbsent_WhenAddZero_IsNoOp()
    {
        _host.SeedCart((2, 1));

        var result = _host.Cart.AddToCart(3, QuantityInput.Valid(0));

        Assert.Equal((CartOperationOutcome.NoOp, Lines((2, 1))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S11_GivenLinePresent_WhenUpdateZero_LineIsRemoved()
    {
        _host.SeedCart((3, 2), (2, 1));

        var result = _host.Cart.UpdateQuantity(3, QuantityInput.Valid(0));

        Assert.Equal((CartOperationOutcome.Removed, Lines((2, 1))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S11_GivenLineAbsent_WhenUpdateZero_IsNoOp()
    {
        _host.SeedCart((2, 1));

        var result = _host.Cart.UpdateQuantity(3, QuantityInput.Valid(0));

        Assert.Equal((CartOperationOutcome.NoOp, Lines((2, 1))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S12_GivenStockZero_WhenAddPositive_IsOutOfStockAndNotAdded()
    {
        var result = _host.Cart.AddToCart(1, QuantityInput.Valid(2));

        Assert.Equal((CartOperationOutcome.OutOfStock, 0), (result.Outcome, _host.ReadCart().Count));
    }

    [Fact]
    public void TC_S13_GivenStaleLineAtStockZero_WhenUpdatePositive_IsOutOfStockAndLineKept()
    {
        _host.SeedCart((1, 2));

        var result = _host.Cart.UpdateQuantity(1, QuantityInput.Valid(2));

        Assert.Equal((CartOperationOutcome.OutOfStock, Lines((1, 2))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S13_GivenStaleLineAtStockZero_WhenUpdateZero_StillRemoves()
    {
        _host.SeedCart((1, 2));

        var result = _host.Cart.UpdateQuantity(1, QuantityInput.Valid(0));

        Assert.Equal((CartOperationOutcome.Removed, 0), (result.Outcome, _host.ReadCart().Count));
    }

    [Fact]
    public void TC_S19_GivenEmptyCartAndStockFive_WhenAddOmitted_AddsOne()
    {
        var result = _host.Cart.AddToCart(3, QuantityInput.Omitted);

        Assert.Equal((CartOperationOutcome.Succeeded, Lines((3, 1))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S19_GivenLineAtStock_WhenAddOmitted_IsOverLimit()
    {
        _host.SeedCart((3, 5));

        var result = _host.Cart.AddToCart(3, QuantityInput.Omitted);

        Assert.Equal((CartOperationOutcome.OverLimit, Lines((3, 5))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S19_GivenStockZero_WhenAddOmitted_IsOutOfStock()
    {
        var result = _host.Cart.AddToCart(1, QuantityInput.Omitted);

        Assert.Equal(CartOperationOutcome.OutOfStock, result.Outcome);
    }

    [Fact]
    public void TC_S20_GivenNegativeOneOnAdd_WhenAdded_IsInvalidQuantityAndCartUnchanged()
    {
        _host.SeedCart((3, 2));

        var result = _host.Cart.AddToCart(3, QuantityInput.Invalid(-1));

        Assert.Equal((CartOperationOutcome.InvalidQuantity, Lines((3, 2))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S21_GivenExistingLine_WhenUpdateNegativeOne_IsInvalidQuantityAndLineKeptNotRemoved()
    {
        _host.SeedCart((3, 2));

        var result = _host.Cart.UpdateQuantity(3, QuantityInput.Invalid(-1));

        Assert.Equal((CartOperationOutcome.InvalidQuantity, Lines((3, 2))), (result.Outcome, _host.ReadCart()));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TC_S22_GivenNonnumericOrBlank_WhenAddOrUpdate_IsInvalidQuantityAndCartUnchanged(bool isAdd)
    {
        _host.SeedCart((3, 2));

        var result = Run(isAdd, 3, QuantityInput.Parse("abc", wasSupplied: true));
        var blank = Run(isAdd, 3, QuantityInput.Parse(null, wasSupplied: true));

        Assert.Equal(
            (CartOperationOutcome.InvalidQuantity, CartOperationOutcome.InvalidQuantity, Lines((3, 2))),
            (result.Outcome, blank.Outcome, _host.ReadCart()));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TC_S23_GivenOverflow_WhenAddOrUpdate_IsInvalidQuantityAndCartUnchanged(bool isAdd)
    {
        _host.SeedCart((3, 2));

        var result = Run(isAdd, 3, QuantityInput.Parse("2147483648", wasSupplied: true));

        Assert.Equal((CartOperationOutcome.InvalidQuantity, Lines((3, 2))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S24_GivenUpdateWithMissingQuantity_WhenUpdated_IsInvalidQuantityNoDefaultNoRemoval()
    {
        _host.SeedCart((3, 2));

        var result = _host.Cart.UpdateQuantity(3, QuantityInput.Omitted);

        Assert.Equal((CartOperationOutcome.InvalidQuantity, Lines((3, 2))), (result.Outcome, _host.ReadCart()));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TC_S25_GivenUnknownIdAndInvalidQuantity_WhenApplied_UnknownIdWins(bool isAdd)
    {
        var result = Run(isAdd, 999, QuantityInput.Invalid(-1));

        Assert.Equal(CartOperationOutcome.OutOfStock, result.Outcome);
    }

    [Fact]
    public void TC_S25_GivenInvalidQuantityAndAbsentLine_WhenUpdated_InvalidQuantityWinsOverTheSilentNoOp()
    {
        var result = _host.Cart.UpdateQuantity(3, QuantityInput.Invalid(-1));

        Assert.Equal(CartOperationOutcome.InvalidQuantity, result.Outcome);
    }

    [Theory]
    [InlineData(1, 5)]
    [InlineData(3, 99)]
    public void TC_S25_GivenAbsentLineAndStockProblem_WhenUpdated_SilentNoOpWinsOverStock(int productId, int quantity)
    {
        var result = _host.Cart.UpdateQuantity(productId, QuantityInput.Valid(quantity));

        Assert.Equal((CartOperationOutcome.NoOp, 0), (result.Outcome, _host.ReadCart().Count));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TC_S25_GivenStockZeroAndExplicitZero_WhenApplied_ZeroWinsAndRemoves(bool isAdd)
    {
        _host.SeedCart((1, 2));

        var result = Run(isAdd, 1, QuantityInput.Valid(0));

        Assert.Equal((CartOperationOutcome.Removed, 0), (result.Outcome, _host.ReadCart().Count));
    }

    [Fact]
    public void TC_S26_GivenIntMaxValueOnAdd_WhenAddedToExistingLine_IsOverLimitWithoutWrap()
    {
        _host.SeedCart((3, 2));

        var result = _host.Cart.AddToCart(3, QuantityInput.Valid(int.MaxValue));

        Assert.Equal((CartOperationOutcome.OverLimit, Lines((3, 2))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S26_GivenIntMaxValueOnUpdate_WhenUpdated_IsOverLimitWithoutWrap()
    {
        _host.SeedCart((3, 2));

        var result = _host.Cart.UpdateQuantity(3, QuantityInput.Valid(int.MaxValue));

        Assert.Equal((CartOperationOutcome.OverLimit, Lines((3, 2))), (result.Outcome, _host.ReadCart()));
    }

    [Fact]
    public void TC_S26_GivenStoredLineOfIntMaxValue_WhenAddOne_IsOverLimitWithoutWrap()
    {
        _host.SeedRawCartJson(StaleSnapshot(3, int.MaxValue, "\"AvailableQuantity\":999"));

        var result = _host.Cart.AddToCart(3, QuantityInput.Valid(1));

        Assert.Equal(CartOperationOutcome.OverLimit, result.Outcome);
    }

    [Theory]
    [InlineData("")]
    [InlineData("\"AvailableQuantity\":0")]
    [InlineData("\"AvailableQuantity\":999")]
    public void TC_S27_GivenSnapshotWithMissingZeroOrWrongStock_WhenAddWithinCatalogStock_UsesCatalogNotSnapshot(
        string stockProperty)
    {
        _host.SeedRawCartJson(StaleSnapshot(3, 1, stockProperty));

        var result = _host.Cart.AddToCart(3, QuantityInput.Valid(4));

        Assert.Equal((CartOperationOutcome.Succeeded, Lines((3, 5))), (result.Outcome, _host.ReadCart()));
    }

    [Theory]
    [InlineData("")]
    [InlineData("\"AvailableQuantity\":0")]
    [InlineData("\"AvailableQuantity\":999")]
    public void TC_S27_GivenSnapshotWithMissingZeroOrWrongStock_WhenUpdateAboveCatalogStock_IsOverLimit(
        string stockProperty)
    {
        _host.SeedRawCartJson(StaleSnapshot(3, 1, stockProperty));

        var result = _host.Cart.UpdateQuantity(3, QuantityInput.Valid(6));

        Assert.Equal((CartOperationOutcome.OverLimit, 5), (result.Outcome, result.AvailableQuantity));
    }

    [Fact]
    public void TC_S27_GivenSnapshotWithWrongStock_WhenAvailabilityRead_IsFromCatalog()
    {
        _host.SeedRawCartJson(StaleSnapshot(3, 1, "\"AvailableQuantity\":0"));

        Assert.Equal(5, _host.Cart.GetAvailableQuantity(3));
    }

    [Fact]
    public void TC_S28_GivenAcceptedAddAndUpdate_WhenTwoSessionsBuyFullStock_AvailabilityIsUnchangedAndNothingIsReserved()
    {
        var other = new StorefrontHost();

        var first = _host.Cart.AddToCart(3, QuantityInput.Valid(5));
        var second = other.Cart.AddToCart(3, QuantityInput.Valid(5));
        _host.Cart.UpdateQuantity(3, QuantityInput.Valid(4));

        Assert.Equal(
            (CartOperationOutcome.Succeeded, CartOperationOutcome.Succeeded, 5, 5),
            (first.Outcome, second.Outcome, _host.Products.GetProductById(3)!.AvailableQuantity, other.Cart.GetAvailableQuantity(3)));
    }

    [Fact]
    public void TC_S29_GivenCartWithItems_WhenCheckout_AvailabilityIsUnchanged()
    {
        _host.Cart.AddToCart(3, QuantityInput.Valid(5));

        _host.CartController.Checkout();

        Assert.Equal(5, _host.Products.GetProductById(3)!.AvailableQuantity);
    }

    private static string StaleSnapshot(int productId, int quantity, string stockProperty)
    {
        var stock = string.IsNullOrEmpty(stockProperty) ? string.Empty : "," + stockProperty;
        return "[{\"Product\":{\"Id\":" + productId
            + ",\"Name\":\"Old\",\"Description\":\"Old\",\"Price\":1.0,\"ImageUrl\":\"x\"" + stock
            + "},\"Quantity\":" + quantity + "}]";
    }
}
