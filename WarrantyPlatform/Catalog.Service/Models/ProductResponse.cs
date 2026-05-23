using Catalog.Service.Entities.Const;

namespace Catalog.Service.Models;

public record ProductResponse(
    Guid Id,
    string Name,
    string Sku,
    ProductCategory Category,
    DateTime CreatedAt,
    BrandResponse Brand);
