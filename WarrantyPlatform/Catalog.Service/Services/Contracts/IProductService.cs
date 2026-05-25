using Catalog.Service.Common;
using Catalog.Service.Common.Pagination;
using Catalog.Service.Models;

namespace Catalog.Service.Services.Contracts;

public interface IProductService
{
    Task<Result<PagedList<ProductResponse>>> GetAllAsync(GetProductsRequest request, CancellationToken ct);
    Task<Result<ProductResponse>> GetByIdAsync(Guid id, CancellationToken ct);
    Task<Result<ProductResponse>> CreateAsync(CreateProductRequest productModel, CancellationToken ct);
    Task<Result<ProductResponse>> UpdateAsync(Guid id, UpdateProductRequest productModel, CancellationToken ct);
    Task<Result> DeleteAsync(Guid id, CancellationToken ct);
}
