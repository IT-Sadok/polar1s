using Catalog.Service.Models.Reports;

namespace Catalog.Service.Services.Contracts;

public interface IReportsService
{
    Task<IReadOnlyList<TopBrandResponse>> GetTopBrandsByAvgPriceAsync(int limit, CancellationToken ct);
    Task<IReadOnlyList<ProductWithoutImageResponse>> GetProductsWithoutImagesAsync(CancellationToken ct);
    Task<IReadOnlyList<CheapestSupplierResponse>> GetCheapestSupplierPerProductAsync(CancellationToken ct);
    Task<IReadOnlyList<ProductWithMultipleSuppliersResponse>> GetProductsWithMultipleSuppliersAsync(CancellationToken ct);
    Task<IReadOnlyList<UnusedBrandResponse>> GetUnusedBrandsAsync(CancellationToken ct);
}
