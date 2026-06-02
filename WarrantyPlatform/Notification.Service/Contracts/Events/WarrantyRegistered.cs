namespace Notification.Service.Contracts.Events;

public sealed record WarrantyRegistered(
    Guid WarrantyId,
    Guid CustomerId,
    Guid ProductId,
    DateTime PurchaseDate,
    DateTime ExpiresAt);
