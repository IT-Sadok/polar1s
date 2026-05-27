using Warranty.Service.Entities.Const;
using Warranty.Service.Entities.Contracts;

namespace Warranty.Service.Entities;

public class WarrantyEntity : IAuditable
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime ExpiresAt { get; set; }
    public WarrantyStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
