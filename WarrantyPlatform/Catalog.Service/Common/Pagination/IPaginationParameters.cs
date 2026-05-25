namespace Catalog.Service.Common.Pagination;

public interface IPaginationParameters
{
    int PageNumber { get; set; }
    int PageSize { get; set; }
}
