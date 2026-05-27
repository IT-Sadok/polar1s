namespace Warranty.Service.Models;

public sealed record CreateWarrantyRequest(
    Guid ProductId,
    Guid CustomerId,
    DateTime PurchaseDate,
    DateTime ExpiresAt);
