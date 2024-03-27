using System.Text;
using Microsoft.Extensions.Caching.Distributed;

namespace Northwind.Reposotories.Caches;

public class Redis
{
    private readonly IDistributedCache _cache;

    public Redis(IDistributedCache cache)
    {
        _cache = cache;
    }

    public void Set(string key, string value, int expirationTime)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expirationTime)

        };

        _cache.Set(
            key: key,
            value: Encoding.UTF8.GetBytes(value),
            options: options);
        
    }

    public string? Get(string key)
    {
        var _data = _cache.Get(key: key);
        return _data is not null ? Encoding.UTF8.GetString(_data) : null;
    }
}