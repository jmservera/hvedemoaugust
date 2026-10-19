---
title: Zava Storefront
description: Run and verify the local ASP.NET Core inventory-aware cart demo.
---

## Local demo

The storefront uses ASP.NET Core MVC on .NET 6, responsive Razor/Bootstrap
views, a static ten-product catalog, and a session-backed cart. Checkout
confirms the demo journey and clears the cart; it does not process payment,
create an order, reserve stock, or decrement availability.

The [Inventory-Aware Cart PRD](../docs/prds/inventory-aware-cart.md) is approved
for this local demo only, not an authoritative shared-release document.

## Features

- **Product Listing**: Browse ten products with stock text, images, descriptions, and unchanged prices
- **Shopping Cart**: Add products to cart with session-based storage
- **Cart Management**: View cart, update quantities, remove items
- **Checkout**: Simple checkout process that clears cart and shows success message
- **Responsive Design**: Mobile-friendly layout using Bootstrap 5
- **Inventory Validation**: Adds include quantities already in the cart; updates set the total quantity
- **Recovery Feedback**: Rejections preserve the cart and return to the originating page with associated feedback

## Technology Stack

- .NET 6
- ASP.NET Core MVC
- Bootstrap 5
- Bootstrap Icons
- Session-based state management (no database)

## Project Structure

```
ZavaStorefront/
├── Controllers/
│   ├── HomeController.cs      # Products listing and add to cart
│   └── CartController.cs       # Cart operations and checkout
├── Models/
│   ├── Product.cs              # Product model
│   └── CartItem.cs             # Cart item model
├── Services/
│   ├── ProductService.cs       # Static product data
│   └── CartService.cs          # Session-based cart management
├── Views/
│   ├── Home/
│   │   └── Index.cshtml        # Products listing page
│   ├── Cart/
│   │   ├── Index.cshtml        # Shopping cart page
│   │   └── CheckoutSuccess.cshtml  # Checkout success page
│   └── Shared/
│       └── _Layout.cshtml      # Main layout with cart icon
└── wwwroot/
    ├── css/
    │   └── site.css            # Custom styles
    └── images/
        └── products/           # Product images directory
```

## How to Run

From the repository root, with the .NET SDK and .NET 6 ASP.NET Core runtime installed:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Development"
dotnet run --project src\ZavaStorefront.csproj --no-launch-profile --urls http://localhost:5186
```

Open <http://localhost:5186>. .NET 6 is out of support; upgrading the runtime
is separate work, not part of this demo.

## Product Images

The existing catalog image URLs at picsum.photos and the placeholder fallback
are unchanged. Images require network access.

## Sample Products

| ID | Product | Price | Available quantity |
|----|---------|-------|--------------------|
| 1 | Wireless Noise-Canceling Headphones | $199.99 | 3 |
| 2 | Smart Fitness Watch | $149.95 | 0 |
| 3 | 4K Action Camera | $129.50 | 1 |
| 4 | Bluetooth Speaker | $89.99 | 5 |
| 5 | Laptop Stand | $39.95 | 6 |
| 6 | Mechanical Keyboard | $99.00 | 12 |
| 7 | Ergonomic Office Chair | $249.00 | 8 |
| 8 | USB-C Hub Docking Station | $59.99 | 15 |
| 9 | Smart LED Light Bulb | $24.99 | 20 |
| 10 | Electric Standing Desk | $499.00 | 2 |

Stock descriptions show "Out of stock" at zero, an exact count from 1 through
5, and "In stock" above 5 without the listing count. Out-of-stock add controls
are disabled; server validation also rejects forged requests.

## Application Flow

1. **Landing Page**: Displays all products in a responsive grid
2. **Add to Cart**: Choose a quantity and click "Buy"; repeated adds count units already in the cart
3. **View Cart**: Click cart icon (top right) to view cart contents
4. **Update Cart**: Set a total quantity and select "Update"; 0 removes the line, while a zero add is rejected
5. **Checkout**: Click "Checkout" to confirm the demo without a commercial transaction
6. **Success**: View confirmation and return to products

## Session Management

- Cart data is stored in session
- Session timeout: 30 minutes
- No data persistence (cart clears when session expires)
- Cart is cleared after successful checkout

## Logging

The application includes structured logging for:
- Product page loads
- Adding products to cart
- Cart operations (update, remove)
- Checkout process

Logs are written to console during development.

Inventory rejections emit structured warnings with `ProductId`,
`RequestedQuantity`, `ResultingQuantity`, `AvailableQuantity`, `Operation`
and `Reason`. Invalid products use `Reason=product-not-found` and omit
`AvailableQuantity`. Logs do not include serialized carts, session IDs,
customer information or credentials. No customer analytics or business-metric
collection was added.

## Automated validation

From the repository root:

```powershell
dotnet build src\ZavaStorefront.sln
dotnet test tests\ZavaStorefront.Tests\ZavaStorefront.Tests.csproj
```

The xUnit tests cover catalog fixtures, boundary and repeated adds, updates,
zero behavior, overflow, malformed inputs, invalid products, warning fields,
antiforgery, one-time feedback, session isolation, removal and checkout.

## Demo walkthrough and owner review

1. Inspect stock text for the watch (0), camera (1), speaker (5), and stand (6).
2. Add three headphones, then try one more. Confirm exact-limit acceptance
   and a rejection that leaves the cart unchanged.
3. In the cart, update headphones to 4. Confirm the previous quantity of 3,
   feedback near the summary, and keyboard focus on the affected quantity.
4. Update to 0 to remove the headphones. Add another item and use Remove.
5. Check out and confirm an empty cart with unchanged catalog stock.

Owner review remains required in desktop Chrome and Edge. Record browser
versions, viewport size and assistive technology used. Verify that feedback
is announced, associated with the affected control and recoverable using only
the keyboard, including a no-JavaScript pass. Automated markup and focus
checks are not a substitute for real screen-reader review or certification.
Business outcome measurement remains deferred until after the demo.
