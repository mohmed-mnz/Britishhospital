using BussinesLayer.Interfaces.Token;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace BussinesLayer.Interfaces;

public class InMemoryPresistanceService : IPresistanceService
{
    private readonly IMemoryCache _cache;
    private readonly ConcurrentBag<string> _keys;  // Track all keys

    public InMemoryPresistanceService(IMemoryCache cache)
    {
        _cache = cache;
        _keys = new ConcurrentBag<string>();

    }
    public T Set<T>(string key,T data,TimeSpan Expiry)
    {
        return _cache.Set<T>(key, data, Expiry);   
    }
    public T? Get<T>(string key)
    {
        return _cache.Get<T>(key);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }
    public void Clear()
    {
        foreach (var key in _keys)
        {
            _cache.Remove(key);
        }

        _keys.Clear();
    }
}
