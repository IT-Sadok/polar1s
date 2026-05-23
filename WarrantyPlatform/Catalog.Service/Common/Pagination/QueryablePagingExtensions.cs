using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Common.Pagination;

public static class QueryablePagingExtensions
{
    public const int MaxPageSize = 100;

    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        IPaginationParameters parameters,
        CancellationToken ct)
    {
        var pageNumber = Math.Max(1, parameters.PageNumber);
        var pageSize = Math.Clamp(parameters.PageSize, 1, MaxPageSize);

        var count = await source.CountAsync(ct);
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
