using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ZavaStorefront.Services;

namespace ZavaStorefront.Controllers
{
    /// <summary>
    /// Numeric codes for the warning log's operation field (a closed set).
    /// </summary>
    internal enum CartOperation
    {
        Add = 1,
        Update = 2
    }

    /// <summary>
    /// Shared handling of a cart Add or Update: raw quantity parsing, one safe warning per rejection, and the
    /// message carried by redirect to the originating page.
    /// </summary>
    internal static class InventoryFeedback
    {
        public const string MessageKey = "InventoryMessage";

        public const string ProductIdKey = "InventoryProductId";

        /// <summary>
        /// Parses the posted quantity, telling an omitted key from an explicit blank (MVC maps both to null).
        /// </summary>
        public static QuantityInput ParseQuantity(HttpRequest request, string? quantity)
        {
            var wasSupplied = request.HasFormContentType && request.Form.ContainsKey("quantity");
            return QuantityInput.Parse(quantity, wasSupplied);
        }

        /// <summary>
        /// For a rejection, logs exactly one structured warning (four numeric or nullable fields, never raw input)
        /// and stores the approved message, plus the product id when the product exists, for the next request.
        /// Accepted changes and silent no-ops add no message and no warning.
        /// </summary>
        public static void ReportRejection(
            ILogger logger,
            ITempDataDictionary tempData,
            CartOperation operation,
            int productId,
            CartOperationResult result)
        {
            if (!result.IsRejection)
            {
                return;
            }

            logger.LogWarning(
                "Inventory request rejected: operation {Operation}, product {ProductId}, requested {RequestedQty}, available {AvailableQty}",
                (int)operation,
                productId,
                result.RequestedQuantity,
                result.AvailableQuantity);

            tempData[MessageKey] = result.Message;
            if (result.AvailableQuantity.HasValue)
            {
                tempData[ProductIdKey] = productId;
            }
        }
    }
}
