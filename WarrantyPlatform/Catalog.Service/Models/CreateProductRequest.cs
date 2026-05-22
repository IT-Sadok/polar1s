using Catalog.Service.Domain.Const;

namespace Catalog.Service.Models;

public record CreateProductRequest(
    string Name,
    string Sku,
    ProductCategory Category,
    Guid BrandId);
