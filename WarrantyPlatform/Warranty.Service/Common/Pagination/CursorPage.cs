namespace Warranty.Service.Common.Pagination;

public class CursorPage<T>
{
    public required IReadOnlyList<T> Items { get; init; }
    public string? NextCursor { get; init; }
    public bool HasMore => NextCursor is not null;
}
