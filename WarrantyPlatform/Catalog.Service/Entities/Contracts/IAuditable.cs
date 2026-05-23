namespace Catalog.Service.Entities.Contracts;

public interface IAuditable
{
    DateTime CreatedAt { get; set; }
}
