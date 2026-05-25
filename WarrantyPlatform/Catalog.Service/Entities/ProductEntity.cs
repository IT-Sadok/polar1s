using Catalog.Service.Entities.Const;
using Catalog.Service.Entities.Contracts;

namespace Catalog.Service.Entities;

public class ProductEntity : IAuditable
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Sku { get; set; }
    public ProductCategory Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid BrandId { get; set; }

    public BrandEntity Brand { get; set; } = null!;

    public ICollection<ProductImageEntity> Images { get; set; } = [];
    public ICollection<ProductSupplierEntity> ProductSuppliers { get; set; } = [];
}
