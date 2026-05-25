using Catalog.Service.Entities.Contracts;

namespace Catalog.Service.Entities;

public class SupplierEntity: IAuditable
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string ContactEmail { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<ProductSupplierEntity> ProductSuppliers { get; set; } = [];
}
