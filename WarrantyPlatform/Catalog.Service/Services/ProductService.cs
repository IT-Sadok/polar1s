using Catalog.Service.Common;
using Catalog.Service.Common.Cache;
using Catalog.Service.Common.Configuration;
using Catalog.Service.Common.Pagination;
using Catalog.Service.Data;
using Catalog.Service.Mappers;
using Catalog.Service.Models;
using Catalog.Service.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Catalog.Service.Services;

public class ProductService : IProductService
{
    private readonly CatalogDbContext _dbContext;
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheTtl;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        CatalogDbContext dbContext,
        IDistributedCache cache,
        IOptions<CacheOptions> cacheOptions,
        ILogger<ProductService> logger)
    {
        _dbContext = dbContext;
        _cache = cache;
        _cacheTtl = cacheOptions.Value.AbsoluteExpiration;
        _logger = logger;
    }

    public async Task<Result<PagedList<ProductResponse>>> GetAllAsync(GetProductsRequest request, CancellationToken ct)
    {
        var pagedList = await _dbContext.Products
            .OrderBy(p => p.Id)
            .Select(p => new ProductResponse(
                p.Id, p.Name, p.Sku, p.Category, p.CreatedAt,
                new BrandResponse(p.Brand.Id, p.Brand.Name, p.Brand.Country)))
            .ToPagedListAsync(request, ct);

        return Result<PagedList<ProductResponse>>.Success(pagedList);
    }

    public async Task<Result<ProductResponse>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        try
        {
            var cached = await _cache.GetRecordAsync<ProductResponse>(id.ToString(), ct);
            if (cached is not null)
            {
                return cached;
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Cache read failed for product {ProductId}", id);
        }

        var product = await _dbContext.Products
            .Where(p => p.Id == id)
            .Select(p => new ProductResponse(
                p.Id, p.Name, p.Sku, p.Category, p.CreatedAt,
                new BrandResponse(p.Brand.Id, p.Brand.Name, p.Brand.Country)))
            .FirstOrDefaultAsync(ct);

        if (product is null)
        {
            return Error.NotFound($"Product {id} not found");
        }

        try
        {
            await _cache.SetRecordAsync(id.ToString(), product, ct, _cacheTtl);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Cache write failed for product {ProductId}", id);
        }

        return product;
    }

    public async Task<Result<ProductResponse>> CreateAsync(CreateProductRequest productModel, CancellationToken ct)
    {
        var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.Id == productModel.BrandId, ct);
        if (brand is null)
        {
            return Error.NotFound($"Brand {productModel.BrandId} not found");
        }

        var product = productModel.ToEntity(brand);
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync(ct);

        _logger.LogInformation("Product {ProductId} created", product.Id);

        return product.ToResponse();
    }

    public async Task<Result<ProductResponse>> UpdateAsync(Guid id, UpdateProductRequest productModel, CancellationToken ct)
    {
        var product = await _dbContext.Products
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id, ct);

        if (product is null)
        {
            return Error.NotFound($"Product {id} not found");
        }

        product.Name = productModel.Name;
        product.Category = productModel.Category;

        await _dbContext.SaveChangesAsync(ct);

        try
        {
            await _cache.InvalidateCacheAsync(id, ct);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Cache invalidation failed for product {ProductId}", id);
        }

        _logger.LogInformation("Product {ProductId} updated", id);

        return product.ToResponse();
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken ct)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, ct);
        if (product is null)
        {
            return Error.NotFound($"Product {id} not found");
        }

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(ct);

        try
        {
            await _cache.InvalidateCacheAsync(id, ct);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Cache invalidation failed for product {ProductId}", id);
        }

        _logger.LogInformation("Product {ProductId} deleted", id);

        return Result.Success();
    }
}
