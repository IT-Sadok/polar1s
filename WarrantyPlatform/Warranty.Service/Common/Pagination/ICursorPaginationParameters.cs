namespace Warranty.Service.Common.Pagination;

public interface ICursorPaginationParameters
{
    string? Cursor { get; set; }
    int Limit { get; set; }
}
