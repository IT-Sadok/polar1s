using Warranty.Service.Common;
using Warranty.Service.Common.Pagination;
using Warranty.Service.Models;

namespace Warranty.Service.Services.Contracts;

public interface IWarrantyService
{
    Task<Result<CursorPage<WarrantyResponse>>> GetByCustomerAsync(
        GetWarrantiesByCustomerRequest request,
        CancellationToken ct);

    Task<Result<WarrantyResponse>> GetByIdAsync(Guid id, CancellationToken ct);
}
