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

    public async Task<IReadOnlyList<TopBrandResponse>> GetTopBrandsByAvgPriceAsync(int limit, CancellationToken ct)
    {
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

        return await query.ToListAsync(ct);
    }

    public async Task<IReadOnlyList<ProductWithoutImageResponse>> GetProductsWithoutImagesAsync(CancellationToken ct)
    {
        return await _dbContext.Products
            .Where(p => !p.Images.Any())
            .Select(p => new ProductWithoutImageResponse(p.Id, p.Sku, p.Name))
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<CheapestSupplierResponse>> GetCheapestSupplierPerProductAsync(CancellationToken ct)
    {
        return await _dbContext.Products
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
    }

    public async Task<IReadOnlyList<ProductWithMultipleSuppliersResponse>> GetProductsWithMultipleSuppliersAsync(CancellationToken ct)
    {
        return await _dbContext.Products
            .Where(p => p.ProductSuppliers.Count > 1)
            .Select(p => new ProductWithMultipleSuppliersResponse(
                p.Id,
                p.Sku,
                p.Name,
                p.ProductSuppliers.Count
                ))
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<UnusedBrandResponse>> GetUnusedBrandsAsync(CancellationToken ct)
    {
        return await _dbContext.Brands
            .Where(b => !b.Products.Any())
            .Select(b => new UnusedBrandResponse(
                b.Id,
                b.Name,
                b.Country
                ))
            .ToListAsync(ct);
    }
}
