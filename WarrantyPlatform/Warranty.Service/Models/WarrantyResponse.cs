using Warranty.Service.Entities.Const;

namespace Warranty.Service.Models;

public sealed record WarrantyResponse(
    Guid Id,
    Guid ProductId,
    Guid CustomerId,
    DateTime PurchaseDate,
    DateTime ExpiresAt,
    WarrantyStatus Status,
    DateTime CreatedAt);
