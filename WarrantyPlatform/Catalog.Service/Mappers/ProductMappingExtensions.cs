using Catalog.Service.Domain;
using Catalog.Service.Models;

namespace Catalog.Service.Mappers;

public static class ProductMappingExtensions
{
    public static ProductResponse ToResponse(this Product p)
        => new(p.Id, p.Name, p.Sku, p.Category, p.CreatedAt,
            new BrandResponse(p.Brand.Id, p.Brand.Name, p.Brand.Country));
}
