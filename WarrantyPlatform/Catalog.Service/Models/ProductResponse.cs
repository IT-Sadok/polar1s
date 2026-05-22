using Catalog.Service.Domain.Const;

namespace Catalog.Service.Models;

public record ProductResponse(
    Guid Id,
    string Name,
    string Sku,
    ProductCategory Category,
    DateTime CreatedAt,
    BrandResponse Brand);
