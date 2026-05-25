using System.Text.Json;

namespace Catalog.Service.Common.Pagination;

public class PagedList<T> : List<T>
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        AddRange(items);
    }

    public int CurrentPage { get; }
    public int TotalPages { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public string CreateMetadata()
        => JsonSerializer.Serialize(new PaginationMetadata
        {
            TotalCount = TotalCount,
            PageSize = PageSize,
            CurrentPage = CurrentPage,
            TotalPages = TotalPages,
            HasNext = HasNext,
            HasPrevious = HasPrevious,
        }, JsonOptions);
}
