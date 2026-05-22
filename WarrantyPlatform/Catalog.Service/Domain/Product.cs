using Catalog.Service.Domain.Const;

namespace Catalog.Service.Domain;

public class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Sku { get; set; }
    public ProductCategory Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid BrandId { get; set; }

    public Brand Brand { get; set; } = null!;

    public ICollection<ProductImage> Images { get; set; } = [];
    public ICollection<ProductSupplier> ProductSuppliers { get; set; } = [];
}
