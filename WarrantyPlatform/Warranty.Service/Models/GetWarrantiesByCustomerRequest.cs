using Warranty.Service.Common.Pagination;

namespace Warranty.Service.Models;

public sealed record GetWarrantiesByCustomerRequest : ICursorPaginationParameters
{
    public Guid CustomerId { get; set; }
    public string? Cursor { get; set; }
    public int Limit { get; set; } = 10;
}
