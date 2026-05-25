using Catalog.Service.Entities.Contracts;

namespace Catalog.Service.Entities;

public class BrandEntity : IAuditable
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Country { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<ProductEntity> Products { get; set; } = [];
}
