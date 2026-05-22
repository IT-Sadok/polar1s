namespace Catalog.Service.Domain;

public class Brand
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Country { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<Product> Products { get; set; } = [];
}
