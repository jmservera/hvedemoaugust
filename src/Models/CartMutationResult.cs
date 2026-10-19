namespace ZavaStorefront.Models
{
    public record CartMutationResult(bool Succeeded, int ProductId, string? Message = null);
}
