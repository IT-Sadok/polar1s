using Catalog.Service.Entities;
using Catalog.Service.Models;

namespace Catalog.Service.Mappers;

public static class ProductMappingExtensions
{
    public static ProductResponse ToResponse(this ProductEntity p)
        => new(p.Id, p.Name, p.Sku, p.Category, p.CreatedAt,
            new BrandResponse(p.Brand.Id, p.Brand.Name, p.Brand.Country));

    public static ProductEntity ToEntity(this CreateProductRequest request, BrandEntity brand)
        => new()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Sku = request.Sku,
            Category = request.Category,
            BrandId = brand.Id,
            Brand = brand,
        };
}
