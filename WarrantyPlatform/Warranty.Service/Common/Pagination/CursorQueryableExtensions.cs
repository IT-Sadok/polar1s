using Microsoft.EntityFrameworkCore;

namespace Warranty.Service.Common.Pagination;

public static class CursorQueryableExtensions
{
    public const int MaxLimit = 100;

    public static async Task<CursorPage<T>> ToCursorPageAsync<T, TCursor>(
        this IQueryable<T> query,
        ICursorPaginationParameters parameters,
        Func<T, TCursor> cursorSelector,
        CancellationToken ct)
        where TCursor : notnull
    {
        var limit = Math.Clamp(parameters.Limit, 1, MaxLimit);
        var items = await query.Take(limit + 1).ToListAsync(ct);

        string? nextCursor = null;
        if (items.Count > limit)
        {
            nextCursor = CursorEncoder.Encode(cursorSelector(items[limit - 1]));
            items.RemoveAt(limit);
        }

        return new CursorPage<T> { Items = items, NextCursor = nextCursor };
    }
}
