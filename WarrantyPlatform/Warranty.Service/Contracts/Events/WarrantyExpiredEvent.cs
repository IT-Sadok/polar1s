namespace Warranty.Service.Contracts.Events;

public sealed record WarrantyExpiredEvent(
    Guid WarrantyId,
    Guid CustomerId,
    Guid ProductId,
    DateTime ExpiresAt);
