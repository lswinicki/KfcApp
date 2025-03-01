using System.Text.Json;
using Application.Common.Interfaces.Services;
using Microsoft.Extensions.Caching.Distributed;
using ZiggyCreatures.Caching.Fusion;

namespace Application.Services;

public class CacheService(IDistributedCache memoryCache) : ICacheService
{
    public async Task SetAsync<T>(string key, T value, int durationHours, int durationMinutes, int durationSeconds,
        CancellationToken cancellationToken = default)
    {
        await SetMemoryCacheValue(key, value, durationHours, durationMinutes, durationSeconds, cancellationToken);
    }

    public async Task<T?> GetValueAsync<T>(string? key, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(key);

        var a = await memoryCache.GetAsync(key, token: cancellationToken);
        return JsonSerializer.Deserialize<T>(a);
    }

    private async Task SetMemoryCacheValue<T>(string? key, T value, int durationHours, int durationMinutes,
        int durationSeconds, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(key);

        if (durationSeconds == 0 && durationHours == 0 && durationMinutes == 0)
        {
            var exception = new Exception("Cache duration cannot be zero.");
            throw exception;
        }

        var date = DateTimeOffset.Now.AddHours(durationHours).AddMinutes(durationMinutes)
            .AddSeconds(durationSeconds);
        var options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(durationHours) +
                                              TimeSpan.FromMinutes(durationMinutes) +
                                              TimeSpan.FromSeconds(durationSeconds)
        };
        var result = JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true });
        await memoryCache.SetStringAsync(key, result, options, token: cancellationToken);
    }
}