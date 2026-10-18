namespace ZavaStorefront.Services
{
    /// <summary>
    /// Stock state of a product, derived only from its available quantity.
    /// </summary>
    public enum StockStatus
    {
        OutOfStock,
        LowStock,
        InStock
    }

    /// <summary>
    /// The single place for the low-stock threshold, the stock status rule and the approved customer-facing copy.
    /// </summary>
    public static class InventoryRules
    {
        /// <summary>
        /// Highest available quantity still shown as low stock (inclusive). Zero is out of stock.
        /// </summary>
        public const int LowStockThreshold = 5;

        public const string InStockText = "In-stock";

        public const string OutOfStockText = "Out of stock";

        public const string InvalidQuantityText = "Enter a valid whole-number quantity of zero or more.";

        /// <summary>
        /// Gets the stock status. Zero (or less) is out of stock, 1 to 5 inclusive is low, above 5 is in stock.
        /// </summary>
        /// <param name="availableQuantity">The current catalog availability.</param>
        /// <returns>The stock status.</returns>
        public static StockStatus GetStatus(int availableQuantity)
        {
            if (availableQuantity <= 0)
            {
                return StockStatus.OutOfStock;
            }

            return availableQuantity <= LowStockThreshold ? StockStatus.LowStock : StockStatus.InStock;
        }

        /// <summary>
        /// Gets the approved status text. Only low stock shows a count.
        /// </summary>
        /// <param name="availableQuantity">The current catalog availability.</param>
        /// <returns>The status text shown on Home and Cart.</returns>
        public static string GetStatusText(int availableQuantity)
        {
            return GetStatus(availableQuantity) switch
            {
                StockStatus.OutOfStock => OutOfStockText,
                StockStatus.LowStock => $"Only {availableQuantity} left",
                _ => InStockText
            };
        }

        /// <summary>
        /// Gets the rejection text for a valid positive quantity above a positive availability.
        /// </summary>
        /// <param name="availableQuantity">The current catalog availability.</param>
        /// <returns>The approved overstock message including the available quantity.</returns>
        public static string GetOverstockText(int availableQuantity)
        {
            return $"Stock unavailable. Available quantity: {availableQuantity}. Reduce the quantity.";
        }
    }
}
