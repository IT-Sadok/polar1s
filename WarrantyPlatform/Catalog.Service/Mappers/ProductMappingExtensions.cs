using Catalog.Service.Entities;
using Catalog.Service.Models;

namespace Catalog.Service.Mappers;

public static class ProductMappingExtensions
{
    public static ProductResponse ToResponse(this ProductEntity p)
        => new(p.Id, p.Name, p.Sku, p.Category, p.CreatedAt,
            new BrandResponse(p.Brand.Id, p.Brand.Name, p.Brand.Country));
}
