using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZavaStorefront.Tests.TestDoubles;

namespace ZavaStorefront.Tests;

/// <summary>
/// Drives both actions with a real form collection and a capturing logger.
/// Products: 1 has stock 0, 3 has stock 5, 7 has stock 3.
/// </summary>
public class InventoryControllerTests
{
    private const string InvalidText = "Enter a valid whole-number quantity of zero or more.";
    private const string OutOfStockText = "Out of stock";

    private readonly StorefrontHost _host = new();

    private static CartLines Lines(params (int, int)[] lines) => new(lines);

    private IActionResult Post(bool isAdd, int productId, params (string Key, string Value)[] extraFields)
    {
        var fields = new List<(string, string)> { ("productId", productId.ToString()) };
        fields.AddRange(extraFields);
        _host.PostForm(fields.ToArray());

        var quantity = extraFields.Where(f => f.Key == "quantity").Select(f => f.Value).FirstOrDefault();
        return isAdd
            ? _host.Home.AddToCart(productId, quantity)
            : _host.CartController.UpdateQuantity(productId, quantity);
    }

    private string? Message => _host.TempData.Peek("InventoryMessage") as string;

    private List<LogEntry> Warnings() =>
        _host.AllLogEntries.Where(e => e.Level == LogLevel.Warning).ToList();

    [Fact]
    public void TC_C01_GivenAddOverstock_WhenPosted_RedirectsHomeWithExactMessageProductIdAndUnchangedCart()
    {
        _host.SeedCart((3, 3));

        var result = Post(true, 3, ("quantity", "3"));

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(
            ("Index", null, "Stock unavailable. Available quantity: 5. Reduce the quantity.", 3, Lines((3, 3))),
            (redirect.ActionName, redirect.ControllerName, Message, _host.TempData.Peek("InventoryProductId"), _host.ReadCart()));
    }

    [Fact]
    public void TC_C02_GivenUpdateOverstock_WhenPosted_RedirectsToCartWithSameMessageAndUnchangedCart()
    {
        _host.SeedCart((3, 2));

        var result = Post(false, 3, ("quantity", "6"));

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(
            ("Index", "Stock unavailable. Available quantity: 5. Reduce the quantity.", 3, Lines((3, 2))),
            (redirect.ActionName, Message, _host.TempData.Peek("InventoryProductId"), _host.ReadCart()));
    }

    [Fact]
    public void TC_C03_GivenAddAtStockZero_WhenPosted_MessageIsOutOfStockNotTheOverstockTemplate()
    {
        Post(true, 1, ("quantity", "2"));

        Assert.Equal(OutOfStockText, Message);
    }

    [Fact]
    public void TC_C03_GivenUpdateOnStaleLineAtStockZero_WhenPosted_MessageIsOutOfStock()
    {
        _host.SeedCart((1, 2));

        Post(false, 1, ("quantity", "2"));

        Assert.Equal((OutOfStockText, Lines((1, 2))), (Message, _host.ReadCart()));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TC_C03_GivenUnknownProductId_WhenPosted_MessageIsOutOfStockWithoutProductId(bool isAdd)
    {
        var result = Post(isAdd, 999, ("quantity", "1"));

        Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal((OutOfStockText, null), (Message, _host.TempData.Peek("InventoryProductId")));
    }

    [Theory]
    [InlineData(true, "-1")]
    [InlineData(true, "abc")]
    [InlineData(true, "2147483648")]
    [InlineData(true, "")]
    [InlineData(false, "-1")]
    [InlineData(false, "abc")]
    [InlineData(false, "2147483648")]
    [InlineData(false, "")]
    public void TC_C04_GivenInvalidQuantity_WhenPosted_ExactA1MessageOriginRedirectAndCartUnchanged(bool isAdd, string raw)
    {
        _host.SeedCart((3, 2));

        var result = Post(isAdd, 3, ("quantity", raw));

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(("Index", InvalidText, Lines((3, 2))), (redirect.ActionName, Message, _host.ReadCart()));
    }

    [Fact]
    public void TC_C04_GivenUpdateWithMissingQuantity_WhenPosted_ExactA1MessageAndCartUnchanged()
    {
        _host.SeedCart((3, 2));

        Post(false, 3);

        Assert.Equal((InvalidText, Lines((3, 2))), (Message, _host.ReadCart()));
    }

    [Fact]
    public void TC_C05_GivenAddWithKeyAbsent_WhenPosted_AddsOne()
    {
        Post(true, 3);

        Assert.Equal((Lines((3, 1)), (string?)null), (_host.ReadCart(), Message));
    }

    [Fact]
    public void TC_C05_GivenAddWithKeyPostedEmpty_WhenPosted_IsInvalidAndNotDefaultedToOne()
    {
        Post(true, 3, ("quantity", ""));

        Assert.Equal((0, InvalidText), (_host.ReadCart().Count, Message));
    }

    [Fact]
    public void TC_C05_GivenUpdateWithKeyAbsent_WhenPosted_IsInvalid()
    {
        _host.SeedCart((3, 2));

        Post(false, 3);

        Assert.Equal((InvalidText, Lines((3, 2))), (Message, _host.ReadCart()));
    }

    [Fact]
    public void TC_C05_GivenNonFormRequest_WhenAdd_KeyIsTreatedAsAbsent()
    {
        _host.HttpContext.Request.ContentType = null;

        _host.Home.AddToCart(3, null);

        Assert.Equal(Lines((3, 1)), _host.ReadCart());
    }

    public static IEnumerable<object[]> RejectionScenarios => new List<object[]>
    {
        new object[] { true, 3, "6", 1, 6, 5 },
        new object[] { false, 3, "6", 2, 6, 5 },
        new object[] { true, 1, "2", 1, 2, 0 },
        new object[] { true, 999, "1", 1, 1, null! },
        new object[] { false, 999, "1", 2, 1, null! },
        new object[] { true, 3, "-1", 1, -1, 5 },
        new object[] { false, 3, "-1", 2, -1, 5 },
        new object[] { true, 3, "abc", 1, null!, 5 },
        new object[] { false, 3, "2147483648", 2, null!, 5 },
        new object[] { true, 3, "", 1, null!, 5 },
    };

    [Theory]
    [MemberData(nameof(RejectionScenarios))]
    public void TC_C06_C07_GivenRejection_WhenPosted_ExactlyOneWarningWithTheFourSafeProperties(
        bool isAdd, int productId, string raw, int operation, int? requested, int? available)
    {
        _host.SeedCart((3, 2));

        Post(isAdd, productId, ("quantity", raw));

        var warning = Assert.Single(Warnings());
        Assert.Equal(
            new[] { "AvailableQty", "Operation", "ProductId", "RequestedQty" },
            warning.Properties.Keys.OrderBy(k => k, StringComparer.Ordinal));
        Assert.Equal(
            (operation, productId, requested, available),
            ((int)warning.Properties["Operation"]!, (int)warning.Properties["ProductId"]!,
                (int?)warning.Properties["RequestedQty"], (int?)warning.Properties["AvailableQty"]));
    }

    [Fact]
    public void TC_C07_GivenAddOmittedAtOverStock_WhenPosted_RequestedIsTheSubmittedDefaultOneNotACumulativeTotal()
    {
        _host.SeedCart((3, 5));

        Post(true, 3);

        var warning = Assert.Single(Warnings());
        Assert.Equal((int?)1, (int?)warning.Properties["RequestedQty"]);
    }

    [Fact]
    public void TC_C07_GivenUpdateWithMissingQuantity_WhenPosted_RequestedIsNull()
    {
        _host.SeedCart((3, 2));

        Post(false, 3);

        var warning = Assert.Single(Warnings());
        Assert.Null(warning.Properties["RequestedQty"]);
    }

    [Theory]
    [InlineData(true, "zzz-raw-input")]
    [InlineData(false, "zzz-raw-input")]
    [InlineData(true, "<b>zzz</b>")]
    [InlineData(true, "99999999999")]
    public void TC_C08_GivenRejectionWithUntrustedInput_WhenPosted_NoLogEntryAtAnyLevelLeaksRawInputSessionOrCart(
        bool isAdd, string raw)
    {
        _host.SeedCart((3, 2));

        Post(isAdd, 3, ("quantity", raw));

        var everything = _host.AllLogEntries
            .SelectMany(e => new[] { e.Message }.Concat(e.State.Select(p => $"{p.Key}={p.Value}")))
            .ToList();
        var forbidden = new[] { raw.Trim('-'), "ShoppingCart", _host.Session.Id, "4K Action Camera", "Quantity\":" };
        Assert.DoesNotContain(everything, text => forbidden.Any(f => text.Contains(f, StringComparison.Ordinal)));
    }

    [Fact]
    public void TC_C09_GivenAcceptedAdd_WhenPosted_NoMessageAndNoWarning()
    {
        Post(true, 3, ("quantity", "2"));

        Assert.Equal((Lines((3, 2)), (string?)null, 0), (_host.ReadCart(), Message, Warnings().Count));
    }

    [Fact]
    public void TC_C09_GivenAcceptedUpdate_WhenPosted_NoMessageAndNoWarning()
    {
        _host.SeedCart((3, 2));

        Post(false, 3, ("quantity", "4"));

        Assert.Equal((Lines((3, 4)), (string?)null, 0), (_host.ReadCart(), Message, Warnings().Count));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TC_C09_GivenExplicitZero_WhenPosted_RemovesWithNoMessageAndNoWarning(bool isAdd)
    {
        _host.SeedCart((3, 2));

        Post(isAdd, 3, ("quantity", "0"));

        Assert.Equal((0, (string?)null, 0), (_host.ReadCart().Count, Message, Warnings().Count));
    }

    [Fact]
    public void TC_C09_GivenUpdateOnAbsentLine_WhenPosted_IsSilentNoOpWithNoMessageAndNoWarning()
    {
        Post(false, 3, ("quantity", "2"));

        Assert.Equal((0, (string?)null, 0), (_host.ReadCart().Count, Message, Warnings().Count));
    }

    [Fact]
    public void TC_C10_GivenStaleSnapshotStock_WhenCartIndexRendered_AvailabilityMapComesFromTheCatalog()
    {
        _host.SeedRawCartJson(
            "[{\"Product\":{\"Id\":3,\"Name\":\"Old\",\"Description\":\"Old\",\"Price\":1.0,\"ImageUrl\":\"x\",\"AvailableQuantity\":0},\"Quantity\":1}]");

        var view = Assert.IsType<ViewResult>(_host.CartController.Index());

        var availability = Assert.IsType<Dictionary<int, int>>(view.ViewData["Availability"]);
        Assert.Equal(5, availability[3]);
    }

    [Fact]
    public void TC_C11_GivenAddUnknownId_WhenPosted_TheOldNonExistentProductWarningNoLongerOccurs()
    {
        Post(true, 999, ("quantity", "1"));

        Assert.DoesNotContain(_host.HomeLog.Entries, e => e.Message.Contains("non-existent", StringComparison.Ordinal));
    }

    [Fact]
    public void TC_C11_GivenUpdate_WhenPosted_TheRawQuantityInformationLogNoLongerOccurs()
    {
        _host.SeedCart((3, 2));

        Post(false, 3, ("quantity", "4"));

        Assert.DoesNotContain(_host.CartLog.Entries, e => e.Message.Contains("quantity to", StringComparison.Ordinal));
    }
}
