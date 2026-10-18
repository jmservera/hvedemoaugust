using ZavaStorefront.Services;

namespace ZavaStorefront.Tests;

public class QuantityInputTests
{
    public static IEnumerable<object?[]> InvalidInputs => new List<object?[]>
    {
        new object?[] { null, null },
        new object?[] { "", null },
        new object?[] { "   ", null },
        new object?[] { "-1", -1 },
        new object?[] { "abc", null },
        new object?[] { "3.5", null },
        new object?[] { "1e2", null },
        new object?[] { "2147483648", null },
        new object?[] { "-2147483649", null },
    };

    [Fact]
    public void TC_P01_GivenAddKeyNotPosted_WhenParsed_IsOmitted()
    {
        var input = QuantityInput.Parse(null, wasSupplied: false);

        Assert.Equal(QuantityInputKind.Omitted, input.Kind);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void TC_P02_GivenKeyPostedBlank_WhenParsed_IsInvalidWithNullRequestedNotZeroNotDefault(string? raw)
    {
        var input = QuantityInput.Parse(raw, wasSupplied: true);

        Assert.Equal((QuantityInputKind.Invalid, (int?)null), (input.Kind, input.Value));
    }

    [Fact]
    public void TC_P03_GivenUpdateKeyNotPosted_WhenParsed_IsOmittedSoTheServiceTreatsItAsInvalid()
    {
        // The parser reports Omitted; CartService maps Omitted on Update to InvalidQuantity (TC_S24).
        var input = QuantityInput.Parse(null, wasSupplied: false);

        Assert.Equal((QuantityInputKind.Omitted, (int?)null), (input.Kind, input.Value));
    }

    [Fact]
    public void TC_P04_GivenUpdateKeyPostedBlank_WhenParsed_IsInvalidWithNullRequested()
    {
        var input = QuantityInput.Parse(string.Empty, wasSupplied: true);

        Assert.Equal((QuantityInputKind.Invalid, (int?)null), (input.Kind, input.Value));
    }

    [Fact]
    public void TC_P05_GivenNegativeOne_WhenParsed_IsInvalidWithSafeNegativeRequested()
    {
        var input = QuantityInput.Parse("-1", wasSupplied: true);

        Assert.Equal((QuantityInputKind.Invalid, (int?)-1), (input.Kind, input.Value));
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("3.5")]
    [InlineData("1e2")]
    public void TC_P06_GivenNonnumeric_WhenParsed_IsInvalidWithNullRequested(string raw)
    {
        var input = QuantityInput.Parse(raw, wasSupplied: true);

        Assert.Equal((QuantityInputKind.Invalid, (int?)null), (input.Kind, input.Value));
    }

    [Theory]
    [InlineData("2147483648")]
    [InlineData("-2147483649")]
    public void TC_P07_GivenOverflow_WhenParsed_IsInvalidWithoutAFabricatedNumber(string raw)
    {
        var input = QuantityInput.Parse(raw, wasSupplied: true);

        Assert.Equal((QuantityInputKind.Invalid, (int?)null), (input.Kind, input.Value));
    }

    [Theory]
    [InlineData("0", 0)]
    [InlineData(" 3 ", 3)]
    [InlineData("2147483647", 2147483647)]
    public void TC_P08_GivenWholeNumber_WhenParsed_IsValidWithThatQuantity(string raw, int expected)
    {
        var input = QuantityInput.Parse(raw, wasSupplied: true);

        Assert.Equal((QuantityInputKind.Valid, (int?)expected), (input.Kind, input.Value));
    }

    [Theory]
    [MemberData(nameof(InvalidInputs))]
    public void TC_P09_GivenAnyInvalidInput_WhenParsed_NeverReturnsValidZero(string? raw, int? expectedValue)
    {
        var input = QuantityInput.Parse(raw, wasSupplied: true);

        Assert.Equal((QuantityInputKind.Invalid, expectedValue), (input.Kind, input.Value));
    }
}
