namespace Catalog.Service.Domain;

public class ProductImage
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public required string Url { get; set; }
    public bool IsPrimary { get; set; }
    public DateTime CreatedAt { get; set; }
}
