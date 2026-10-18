# Zava Storefront - ASP.NET Core MVC

A simple e-commerce storefront application built with .NET 6 ASP.NET MVC.

## Features

- **Product Listing**: Browse a catalog of 8 sample products with images, descriptions, and prices
- **Shopping Cart**: Add products to cart with session-based storage
- **Cart Management**: View cart, update quantities, remove items
- **Checkout**: Simple checkout process that clears cart and shows success message
- **Responsive Design**: Mobile-friendly layout using Bootstrap 5

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

1. Navigate to the project directory:
   ```bash
   cd ZavaStorefront
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

3. Open your browser and navigate to:
   ```
   https://localhost:5001
   ```

## Product Images

The application includes 8 sample products. Product images are referenced from:
- `/wwwroot/images/products/`

If images are not found, the application automatically falls back to placeholder images from placeholder.com.

To add custom product images, place JPG files in `wwwroot/images/products/` with these names:
- headphones.jpg
- smartwatch.jpg
- speaker.jpg
- charger.jpg
- usb-hub.jpg
- keyboard.jpg
- mouse.jpg
- webcam.jpg

## Sample Products

1. Wireless Bluetooth Headphones - $89.99
2. Smart Fitness Watch - $199.99
3. Portable Bluetooth Speaker - $49.99
4. Wireless Charging Pad - $29.99
5. USB-C Hub Adapter - $39.99
6. Mechanical Gaming Keyboard - $119.99
7. Ergonomic Wireless Mouse - $34.99
8. HD Webcam - $69.99

## Application Flow

1. **Landing Page**: Displays all products in a responsive grid
2. **Add to Cart**: Click "Buy" button to add products to cart
3. **View Cart**: Click cart icon (top right) to view cart contents
4. **Update Cart**: Modify quantities or remove items
5. **Checkout**: Click "Checkout" button to complete purchase
6. **Success**: View confirmation and return to products

## Inventory Behavior

Each product has an available quantity from the static catalog (`ProductService`). The values are synthetic demo seed data, not warehouse data, and nothing reserves or decrements stock.

- **Status**: more than 5 available shows `In-stock`, 1 to 5 shows `Only {availableQuantity} left` (the count appears only for low stock), and 0 shows `Out of stock`. Buy is disabled for out-of-stock products.
- **Limit**: Add (cumulative) and Update (absolute) both reject a quantity above the available quantity, inclusive, with `Stock unavailable. Available quantity: {availableQuantity}. Reduce the quantity.`. A positive quantity at 0 stock, or an unknown product id, shows `Out of stock`.
- **Zero**: an explicit quantity of `0` removes the item on Add and Update. An omitted Add quantity means 1.
- **Invalid quantity**: a negative, nonnumeric, overflowing, or blank quantity, and a missing quantity on Update, leaves the cart unchanged and shows `Enter a valid whole-number quantity of zero or more.`
- **Feedback**: every rejection returns to the originating page with the last valid cart, shows the message in a focused `role="alert"` region, and logs one warning with only `operation`, `productId`, `requestedQty`, and `availableQty`.

## Tests

```bash
dotnet test tests/ZavaStorefront.Tests/ZavaStorefront.Tests.csproj
```

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
