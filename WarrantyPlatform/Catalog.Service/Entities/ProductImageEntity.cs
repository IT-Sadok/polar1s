using Catalog.Service.Entities.Contracts;

namespace Catalog.Service.Entities;

public class ProductImageEntity : IAuditable
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; } = null!;
    public required string Url { get; set; }
    public bool IsPrimary { get; set; }
    public DateTime CreatedAt { get; set; }
}
