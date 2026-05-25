using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Catalog.Service.Common.Cache;

public static class DistributedCacheExtensions
{
    public static async Task SetRecordAsync<T>(
        this IDistributedCache cache,
        string recordId,
        T data,
        CancellationToken ct,
        TimeSpan? absoluteExpireTime = null,
        TimeSpan? slidingExpireTime = null)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromMinutes(60),
            SlidingExpiration = slidingExpireTime,
        };

        var json = JsonSerializer.Serialize(data);
        await cache.SetStringAsync(recordId, json, options, ct);
    }

    public static async Task<T?> GetRecordAsync<T>(
        this IDistributedCache cache,
        string recordId,
        CancellationToken ct)
    {
        var json = await cache.GetStringAsync(recordId, ct);
        return json is null ? default : JsonSerializer.Deserialize<T>(json);
    }

    public static async Task InvalidateCacheAsync(
        this IDistributedCache cache,
        Guid id,
        CancellationToken ct) => await cache.RemoveAsync(id.ToString(), ct);
}
