using Catalog.Service.Entities.Contracts;

namespace Catalog.Service.Entities;

public class ProductSupplierEntity : IAuditable
{
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; } = null!;

    public Guid SupplierId { get; set; }
    public SupplierEntity Supplier { get; set; } = null!;

    public decimal UnitCost { get; set; }
    public int LeadTimeDays { get; set; }
    public DateTime CreatedAt { get; set; }
}
