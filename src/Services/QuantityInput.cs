using System.Globalization;

namespace ZavaStorefront.Services
{
    /// <summary>
    /// How a posted quantity field was understood.
    /// </summary>
    public enum QuantityInputKind
    {
        /// <summary>The field was not posted.</summary>
        Omitted,

        /// <summary>A whole number of zero or more.</summary>
        Valid,

        /// <summary>Posted but blank, nonnumeric, overflowing or negative.</summary>
        Invalid
    }

    /// <summary>
    /// A posted quantity parsed from the raw string. A parse failure is never treated as zero.
    /// </summary>
    public sealed record QuantityInput
    {
        private QuantityInput(QuantityInputKind kind, int? value)
        {
            Kind = kind;
            Value = value;
        }

        /// <summary>
        /// Gets how the field was understood.
        /// </summary>
        public QuantityInputKind Kind { get; }

        /// <summary>
        /// Gets the safe integer: the quantity for <see cref="QuantityInputKind.Valid"/>, a negative number for a
        /// negative <see cref="QuantityInputKind.Invalid"/> input, otherwise <see langword="null"/>.
        /// </summary>
        public int? Value { get; }

        /// <summary>
        /// Gets an input for a field that was not posted.
        /// </summary>
        public static QuantityInput Omitted { get; } = new(QuantityInputKind.Omitted, null);

        /// <summary>
        /// Creates a valid input.
        /// </summary>
        /// <param name="quantity">A quantity of zero or more.</param>
        /// <returns>The valid input.</returns>
        public static QuantityInput Valid(int quantity) => new(QuantityInputKind.Valid, quantity);

        /// <summary>
        /// Creates an invalid input.
        /// </summary>
        /// <param name="requestedQuantity">The safe negative integer, or <see langword="null"/> when none applies.</param>
        /// <returns>The invalid input.</returns>
        public static QuantityInput Invalid(int? requestedQuantity = null) =>
            new(QuantityInputKind.Invalid, requestedQuantity);

        /// <summary>
        /// Parses the posted quantity string.
        /// </summary>
        /// <param name="raw">The bound string; MVC turns an empty string into <see langword="null"/>.</param>
        /// <param name="wasSupplied">Whether the <c>quantity</c> form key was posted at all.</param>
        /// <returns>Omitted, valid, or invalid input.</returns>
        public static QuantityInput Parse(string? raw, bool wasSupplied)
        {
            if (!wasSupplied)
            {
                return Omitted;
            }

            if (string.IsNullOrWhiteSpace(raw)
                || !int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var number))
            {
                return Invalid();
            }

            return number < 0 ? Invalid(number) : Valid(number);
        }
    }
}
