using Catalog.Service.Common;
using Catalog.Service.Data;
using Catalog.Service.Models.Reports;
using Catalog.Service.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Services;

public class ReportsService : IReportsService
{
    private readonly CatalogDbContext _dbContext;

    public ReportsService(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private const int MaxLimit = 100;

    public async Task<Result<IReadOnlyList<TopBrandResponse>>> GetTopBrandsByAvgPriceAsync(TopBrandsRequest request, CancellationToken ct)
    {
        var limit = Math.Clamp(request.Limit, 1, MaxLimit);

        // Raw SQL query was used because EF couldn't translate it from LINQ.
        var query = _dbContext.Database.SqlQuery<TopBrandResponse>($"""
            SELECT
                b."Id",
                b."Name",
                COUNT(DISTINCT p."Id")::int AS "ProductCount",
                AVG(ps."UnitCost")          AS "AverageUnitCost"
            FROM "Brands" b
            INNER JOIN "Products" p         ON p."BrandId" = b."Id"
            INNER JOIN "ProductSuppliers" ps ON ps."ProductId" = p."Id"
            GROUP BY b."Id", b."Name"
            ORDER BY AVG(ps."UnitCost") DESC
            LIMIT {limit}
            """);

        var result = await query.ToListAsync(ct);
        return Result<IReadOnlyList<TopBrandResponse>>.Success(result);
    }

    public async Task<Result<IReadOnlyList<ProductWithoutImageResponse>>> GetProductsWithoutImagesAsync(CancellationToken ct)
    {
        var result = await _dbContext.Products
            .Where(p => !p.Images.Any())
            .Select(p => new ProductWithoutImageResponse(p.Id, p.Sku, p.Name))
            .ToListAsync(ct);

        return Result<IReadOnlyList<ProductWithoutImageResponse>>.Success(result);
    }

    public async Task<Result<IReadOnlyList<CheapestSupplierResponse>>> GetCheapestSupplierPerProductAsync(CancellationToken ct)
    {
        var result = await _dbContext.Products
            .Where(p => p.ProductSuppliers.Any())
            .Select(p => p.ProductSuppliers
                .OrderBy(ps => ps.UnitCost)
                .Select(ps => new CheapestSupplierResponse(
                    p.Id,
                    p.Name,
                    ps.Supplier.Name,
                    ps.UnitCost))
                .First())
            .ToListAsync(ct);

        return Result<IReadOnlyList<CheapestSupplierResponse>>.Success(result);
    }

    public async Task<Result<IReadOnlyList<ProductWithMultipleSuppliersResponse>>> GetProductsWithMultipleSuppliersAsync(CancellationToken ct)
    {
        var result = await _dbContext.Products
            .Where(p => p.ProductSuppliers.Count > 1)
            .Select(p => new ProductWithMultipleSuppliersResponse(
                p.Id,
                p.Sku,
                p.Name,
                p.ProductSuppliers.Count))
            .ToListAsync(ct);

        return Result<IReadOnlyList<ProductWithMultipleSuppliersResponse>>.Success(result);
    }

    public async Task<Result<IReadOnlyList<UnusedBrandResponse>>> GetUnusedBrandsAsync(CancellationToken ct)
    {
        var result = await _dbContext.Brands
            .Where(b => !b.Products.Any())
            .Select(b => new UnusedBrandResponse(
                b.Id,
                b.Name,
                b.Country))
            .ToListAsync(ct);

        return Result<IReadOnlyList<UnusedBrandResponse>>.Success(result);
    }
}
