using Catalog.Service.Common;
using Catalog.Service.Models.Reports;

namespace Catalog.Service.Services.Contracts;

public interface IReportsService
{
    Task<Result<IReadOnlyList<TopBrandResponse>>> GetTopBrandsByAvgPriceAsync(TopBrandsRequest request, CancellationToken ct);
    Task<Result<IReadOnlyList<ProductWithoutImageResponse>>> GetProductsWithoutImagesAsync(CancellationToken ct);
    Task<Result<IReadOnlyList<CheapestSupplierResponse>>> GetCheapestSupplierPerProductAsync(CancellationToken ct);
    Task<Result<IReadOnlyList<ProductWithMultipleSuppliersResponse>>> GetProductsWithMultipleSuppliersAsync(CancellationToken ct);
    Task<Result<IReadOnlyList<UnusedBrandResponse>>> GetUnusedBrandsAsync(CancellationToken ct);
}
