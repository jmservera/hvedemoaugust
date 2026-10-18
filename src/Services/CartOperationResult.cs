namespace ZavaStorefront.Services
{
    /// <summary>
    /// What happened to an Add or Update request.
    /// </summary>
    public enum CartOperationOutcome
    {
        Succeeded,
        Removed,
        NoOp,
        OutOfStock,
        OverLimit,
        InvalidQuantity
    }

    /// <summary>
    /// The result of an Add or Update, carrying only what the controller needs to respond and log safely.
    /// </summary>
    public sealed class CartOperationResult
    {
        private CartOperationResult(
            CartOperationOutcome outcome,
            int? availableQuantity,
            int? requestedQuantity)
        {
            Outcome = outcome;
            AvailableQuantity = availableQuantity;
            RequestedQuantity = requestedQuantity;
        }

        public CartOperationOutcome Outcome { get; }

        /// <summary>
        /// Gets the current catalog availability, or <see langword="null"/> when the product is unknown.
        /// </summary>
        public int? AvailableQuantity { get; }

        /// <summary>
        /// Gets the submitted safe integer (never a cumulative total), or <see langword="null"/> when none applies.
        /// </summary>
        public int? RequestedQuantity { get; }

        /// <summary>
        /// Gets a value indicating whether the request was rejected (out of stock, over limit or invalid quantity).
        /// </summary>
        public bool IsRejection =>
            Outcome is CartOperationOutcome.OutOfStock
                or CartOperationOutcome.OverLimit
                or CartOperationOutcome.InvalidQuantity;

        /// <summary>
        /// Gets the exact approved message for a rejection, or <see langword="null"/> when there is none.
        /// </summary>
        public string? Message => Outcome switch
        {
            CartOperationOutcome.OutOfStock => InventoryRules.OutOfStockText,
            CartOperationOutcome.OverLimit => InventoryRules.GetOverstockText(AvailableQuantity ?? 0),
            CartOperationOutcome.InvalidQuantity => InventoryRules.InvalidQuantityText,
            _ => null
        };

        public static CartOperationResult Succeeded(int available, int requested) =>
            new(CartOperationOutcome.Succeeded, available, requested);

        public static CartOperationResult Removed(int available) =>
            new(CartOperationOutcome.Removed, available, 0);

        public static CartOperationResult NoOp(int? available, int? requested) =>
            new(CartOperationOutcome.NoOp, available, requested);

        public static CartOperationResult OutOfStock(int? available, int? requested) =>
            new(CartOperationOutcome.OutOfStock, available, requested);

        public static CartOperationResult OverLimit(int available, int requested) =>
            new(CartOperationOutcome.OverLimit, available, requested);

        public static CartOperationResult InvalidQuantity(int? available, int? requested) =>
            new(CartOperationOutcome.InvalidQuantity, available, requested);
    }
}
