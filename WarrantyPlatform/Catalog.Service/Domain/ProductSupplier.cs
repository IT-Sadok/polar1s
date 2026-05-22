namespace Catalog.Service.Domain;

public class ProductSupplier
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public decimal UnitCost { get; set; }
    public int LeadTimeDays { get; set; }
}
