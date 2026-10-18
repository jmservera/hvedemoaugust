namespace ZavaStorefront.Tests.TestDoubles;

/// <summary>
/// Ordered (product id, quantity) pairs with value equality, so cart state can sit inside tuple assertions.
/// </summary>
internal sealed class CartLines : List<(int ProductId, int Quantity)>
{
    public CartLines()
    {
    }

    public CartLines(IEnumerable<(int ProductId, int Quantity)> lines)
        : base(lines)
    {
    }

    public override bool Equals(object? obj) => obj is CartLines other && this.SequenceEqual(other);

    public override int GetHashCode() => this.Aggregate(17, (hash, line) => HashCode.Combine(hash, line));

    public override string ToString() => string.Join(", ", this.Select(l => $"{l.ProductId}x{l.Quantity}"));
}
