using Warranty.Service.Contracts.Events;
using Warranty.Service.Entities;
using Warranty.Service.Entities.Const;
using Warranty.Service.Models;

namespace Warranty.Service.Mappers;

public static class WarrantyMappingExtensions
{
    public static WarrantyResponse ToResponse(this WarrantyEntity warranty)
    {
        return new WarrantyResponse(
            warranty.Id,
            warranty.ProductId,
            warranty.CustomerId,
            warranty.PurchaseDate,
            warranty.ExpiresAt,
            warranty.Status,
            warranty.CreatedAt);
    }

    public static WarrantyEntity ToEntity(this CreateWarrantyRequest request)
    {
        return new WarrantyEntity
        {
            Id = Guid.NewGuid(),
            ProductId = request.ProductId,
            CustomerId = request.CustomerId,
            PurchaseDate = request.PurchaseDate,
            ExpiresAt = request.ExpiresAt,
            Status = WarrantyStatus.Active
        };
    }

    public static WarrantyRegisteredEvent ToRegisteredEvent(this WarrantyEntity warranty)
    {
        return new WarrantyRegisteredEvent(
            warranty.Id,
            warranty.CustomerId,
            warranty.ProductId,
            warranty.PurchaseDate,
            warranty.ExpiresAt);
    }
}
