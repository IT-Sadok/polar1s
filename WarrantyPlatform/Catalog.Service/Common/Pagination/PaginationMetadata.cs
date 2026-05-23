using System.Text.Json.Serialization;

namespace Catalog.Service.Common.Pagination;

public class PaginationMetadata
{
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("currentPage")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("hasNext")]
    public bool HasNext { get; set; }

    [JsonPropertyName("hasPrevious")]
    public bool HasPrevious { get; set; }
}
