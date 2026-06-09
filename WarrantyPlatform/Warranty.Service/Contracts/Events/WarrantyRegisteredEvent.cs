namespace Warranty.Service.Contracts.Events;

public sealed record WarrantyRegisteredEvent(
    Guid WarrantyId,
    Guid CustomerId,
    Guid ProductId,
    DateTime PurchaseDate,
    DateTime ExpiresAt);
