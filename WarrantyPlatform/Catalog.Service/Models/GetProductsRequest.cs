using Catalog.Service.Common.Pagination;

namespace Catalog.Service.Models;

public record GetProductsRequest : IPaginationParameters
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
