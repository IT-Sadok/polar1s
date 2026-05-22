namespace Catalog.Service.Domain;

public class Supplier
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string ContactEmail { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<ProductSupplier> ProductSuppliers { get; set; } = [];
}
