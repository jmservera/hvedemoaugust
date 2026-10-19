using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ZavaStorefront.Services;

namespace ZavaStorefront.Tests;

public sealed class CartHttpTests : IClassFixture<StorefrontFactory>
{
    private readonly StorefrontFactory _factory;

    public CartHttpTests(StorefrontFactory factory)
    {
        _factory = factory;
    }

    private HttpClient CreateClient() => _factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        AllowAutoRedirect = false,
        BaseAddress = new Uri("https://localhost")
    });

    private static async Task<HttpResponseMessage> PostAsync(HttpClient client, string path,
        string? productId, string? quantity, bool includeToken = true)
    {
        var html = await client.GetStringAsync("/");
        var fields = new Dictionary<string, string>();
        if (includeToken)
        {
            fields["__RequestVerificationToken"] = Regex.Match(html,
                "name=\"__RequestVerificationToken\"[^>]*value=\"([^\"]+)\"").Groups[1].Value;
            Assert.NotEmpty(fields["__RequestVerificationToken"]);
        }
        if (productId != null)
        {
            fields["productId"] = productId;
        }
        if (quantity != null)
        {
            fields["quantity"] = quantity;
        }
        return await client.PostAsync(path, new FormUrlEncodedContent(fields));
    }

    [Fact]
    public async Task GivenCatalog_WhenRendered_ShowsTextAndDisablesOutOfStockAdd()
    {
        using var client = CreateClient();

        var html = await client.GetStringAsync("/");

        Assert.Contains("id=\"stock-2\" class=\"fw-semibold\">Out of stock", html);
        Assert.Contains("id=\"stock-3\" class=\"fw-semibold\">1 left in stock", html);
        Assert.Contains("id=\"stock-4\" class=\"fw-semibold\">5 left in stock", html);
        Assert.Contains("id=\"stock-5\" class=\"fw-semibold\">In stock", html);
        Assert.DoesNotContain("6 left in stock", html);
        Assert.Matches("id=\"quantity-2\"[^>]*disabled", html);
        Assert.Contains("aria-label=\"Shopping cart\"", html);
    }

    [Theory]
    [InlineData("/Home/AddToCart", "/")]
    [InlineData("/Cart/UpdateQuantity", "/Cart")]
    public async Task GivenOverLimit_WhenPosted_ReturnsOneTimeAssociatedFeedbackOnOrigin(string path, string origin)
    {
        using var client = CreateClient();
        await PostAsync(client, "/Home/AddToCart", "1", "2");

        var response = await PostAsync(client, path, "1", "4");

        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Equal(origin, response.Headers.Location?.OriginalString);
        var html = await client.GetStringAsync(origin);
        Assert.Contains("Only 3 available.", html);
        Assert.Contains("role=\"alert\"", html);
        Assert.Contains("data-focus-target=\"quantity-1\"", html);
        Assert.Contains("cart-feedback\"", html);
        Assert.Contains("aria-invalid=\"true\"", html);
        if (origin == "/Cart")
        {
            Assert.True(html.IndexOf("id=\"cart-feedback\"", StringComparison.Ordinal)
                > html.IndexOf("<div class=\"col-md-4\">", StringComparison.Ordinal));
            Assert.Matches("id=\"quantity-1\"[^>]*value=\"2\"", html);
        }
        Assert.DoesNotContain("Only 3 available.", await client.GetStringAsync(origin));
    }

    [Theory]
    [InlineData("/Home/AddToCart", "0")]
    [InlineData("/Home/AddToCart", "999")]
    [InlineData("/Home/AddToCart", "abc")]
    [InlineData("/Home/AddToCart", null)]
    [InlineData("/Cart/UpdateQuantity", "0")]
    [InlineData("/Cart/UpdateQuantity", "999")]
    [InlineData("/Cart/UpdateQuantity", "abc")]
    [InlineData("/Cart/UpdateQuantity", null)]
    public async Task GivenInvalidProduct_WhenPosted_UsesNeutralRecovery(string path, string? productId)
    {
        using var client = CreateClient();
        await PostAsync(client, "/Home/AddToCart", "1", "2");

        var response = await PostAsync(client, path, productId, "1");
        var html = await client.GetStringAsync(response.Headers.Location!);

        Assert.Contains("This product is unavailable. Return to the catalog and choose another product.", html);
        Assert.DoesNotContain("Exception", html);
        Assert.DoesNotContain("StackTrace", html);
        Assert.Contains("value=\"2\"", await client.GetStringAsync("/Cart"));
    }

    [Theory]
    [InlineData("/Home/AddToCart", "abc")]
    [InlineData("/Home/AddToCart", "2147483648")]
    [InlineData("/Cart/UpdateQuantity", "abc")]
    [InlineData("/Cart/UpdateQuantity", "2147483648")]
    [InlineData("/Cart/UpdateQuantity", null)]
    public async Task GivenMalformedQuantity_WhenPosted_DoesNotAddOrRemove(string path, string? quantity)
    {
        using var client = CreateClient();
        await PostAsync(client, "/Home/AddToCart", "1", "2");

        var response = await PostAsync(client, path, "1", quantity);
        var html = await client.GetStringAsync(response.Headers.Location!);

        Assert.Contains("Your cart has not changed.", html);
        Assert.Contains("value=\"2\"", await client.GetStringAsync("/Cart"));
    }

    [Theory]
    [InlineData("/Home/AddToCart")]
    [InlineData("/Cart/UpdateQuantity")]
    public async Task GivenNoAntiforgeryToken_WhenPosted_RejectsMutation(string path)
    {
        using var client = CreateClient();

        var response = await PostAsync(client, path, "1", "1", false);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GivenZeroQuantities_WhenPosted_RejectsAddButRemovesUpdate()
    {
        using var client = CreateClient();
        await PostAsync(client, "/Home/AddToCart", "1", "2");
        await PostAsync(client, "/Home/AddToCart", "5", "1");

        var add = await PostAsync(client, "/Home/AddToCart", "1", "0");
        Assert.Contains("Choose a quantity of at least 1.", await client.GetStringAsync(add.Headers.Location!));
        Assert.Contains("id=\"quantity-1\"", await client.GetStringAsync("/Cart"));
        var update = await PostAsync(client, "/Cart/UpdateQuantity", "1", "0");
        var cart = await client.GetStringAsync(update.Headers.Location!);

        Assert.Contains("Item removed from your cart.", cart);
        Assert.DoesNotContain("id=\"quantity-1\"", cart);
        Assert.Contains("id=\"quantity-5\"", cart);
    }

    [Fact]
    public async Task GivenOutOfStock_WhenForgedAddPosted_RejectsWithZeroAvailability()
    {
        using var client = CreateClient();

        var response = await PostAsync(client, "/Home/AddToCart", "2", "1");

        Assert.Contains("0 available.", await client.GetStringAsync(response.Headers.Location!));
        Assert.Contains("Your cart is empty", await client.GetStringAsync("/Cart"));
    }

    [Fact]
    public async Task GivenCart_WhenCheckedOut_ClearsOnlySessionWithoutChangingStock()
    {
        using var client = CreateClient();
        using var otherClient = CreateClient();
        var products = _factory.Services.GetRequiredService<ProductService>();
        var stock = products.GetAllProducts().Select(p => p.AvailableQuantity).ToArray();
        await PostAsync(client, "/Home/AddToCart", "1", "3");
        Assert.Contains("Your cart is empty", await otherClient.GetStringAsync("/Cart"));
        await PostAsync(otherClient, "/Home/AddToCart", "1", "3");

        var response = await PostAsync(client, "/Cart/Checkout", null, null);

        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Equal("/Cart/CheckoutSuccess", response.Headers.Location?.OriginalString);
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync(response.Headers.Location)).StatusCode);
        Assert.Contains("Your cart is empty", await client.GetStringAsync("/Cart"));
        Assert.Contains("id=\"quantity-1\"", await otherClient.GetStringAsync("/Cart"));
        Assert.Equal(stock, products.GetAllProducts().Select(p => p.AvailableQuantity));
        Assert.Equal(TimeSpan.FromMinutes(30),
            _factory.Services.GetRequiredService<IOptions<SessionOptions>>().Value.IdleTimeout);
    }

    [Fact]
    public async Task GivenCart_WhenRemoving_PreservesOtherLines()
    {
        using var client = CreateClient();
        await PostAsync(client, "/Home/AddToCart", "1", "1");
        await PostAsync(client, "/Home/AddToCart", "5", "1");

        await PostAsync(client, "/Cart/RemoveFromCart", "1", null);
        var html = await client.GetStringAsync("/Cart");

        Assert.DoesNotContain("id=\"quantity-1\"", html);
        Assert.Contains("id=\"quantity-5\"", html);
    }
}
