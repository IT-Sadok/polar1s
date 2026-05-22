using Catalog.Service.Data;
using Catalog.Service.Domain;
using Catalog.Service.Mappers;
using Catalog.Service.Models;
using Catalog.Service.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Services;

public class ProductService : IProductService
{
    private readonly CatalogDbContext _dbContext;

    public ProductService(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ProductResponse>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.Products
            .Select(p => new ProductResponse(
                p.Id, p.Name, p.Sku, p.Category, p.CreatedAt,
                new BrandResponse(p.Brand.Id, p.Brand.Name, p.Brand.Country)))
            .ToListAsync(ct);
    }

    public async Task<ProductResponse?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Products
            .Where(p => p.Id == id)
            .Select(p => new ProductResponse(
                p.Id, p.Name, p.Sku, p.Category, p.CreatedAt,
                new BrandResponse(p.Brand.Id, p.Brand.Name, p.Brand.Country)))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<ProductResponse?> CreateAsync(CreateProductRequest productModel, CancellationToken ct)
    {
        var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.Id == productModel.BrandId, ct);
        if (brand is null)
        {
            return null;
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = productModel.Name,
            Sku = productModel.Sku,
            Category = productModel.Category,
            BrandId = productModel.BrandId,
            Brand = brand,
        };

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync(ct);

        return product.ToResponse();
    }

    public async Task<ProductResponse?> UpdateAsync(Guid id, UpdateProductRequest productModel, CancellationToken ct)
    {
        var product = await _dbContext.Products
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id, ct);

        if (product is null)
        {
            return null;
        }

        product.Name = productModel.Name;
        product.Category = productModel.Category;

        await _dbContext.SaveChangesAsync(ct);

        return product.ToResponse();
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, ct);
        if (product is null)
        {
            return false;
        }

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(ct);

        return true;
    }
}
