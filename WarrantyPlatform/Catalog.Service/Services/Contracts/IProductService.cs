using Catalog.Service.Models;

namespace Catalog.Service.Services.Contracts;

public interface IProductService
{
    Task<IReadOnlyList<ProductResponse>> GetAllAsync(CancellationToken ct);
    Task<ProductResponse?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<ProductResponse?> CreateAsync(CreateProductRequest productModel, CancellationToken ct);
    Task<ProductResponse?> UpdateAsync(Guid id, UpdateProductRequest productModel, CancellationToken ct);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
}
