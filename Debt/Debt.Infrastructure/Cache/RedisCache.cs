using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace DebtManager.Infrastructure;

internal sealed class RedisCache : ICache
{
    private readonly IDistributedCache _cache;

    public RedisCache(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string key, Func<Task<T?>> set, CancellationToken cancellationToken = default)
        where T : class
    {
        var result = await GetAsync<T>(key, cancellationToken);

        if (result is null)
        {
            result = await set();
            if (result is not null)
                await SetAsync(key, result, cancellationToken);
        }

        return result;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) 
        where T : class
    {
        var value = await _cache.GetStringAsync(key, cancellationToken);
        if (value is null)
            return null;

        var ob = JsonConvert.DeserializeObject<T>(value!);
        return ob;
    }

    public Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
        where T : class
    {
        var json = JsonConvert.SerializeObject(value);
        return _cache.SetStringAsync(key, json, cancellationToken);
    }
}
