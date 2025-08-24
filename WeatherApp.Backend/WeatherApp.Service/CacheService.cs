using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DomainModel.Contracts;


namespace WeatherApp.Service
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CacheService> _logger;

        public CacheService(IMemoryCache cache, ILogger<CacheService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public Task AddToCache<T>(string key, T value, TimeSpan expiration)
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };
            _cache.Set(key, value, cacheOptions);
            return Task.CompletedTask;

        }

        public Task DeleteByKeyAsync<T>(string key)
        {
           _cache.Remove(key);
            return Task.CompletedTask;
        }

        public Task<T?> GetByKeyAsync<T>(string key)
        {
            if (_cache.TryGetValue(key, out var value))
            {
                return Task.FromResult<T?>((T?)value);
            }
            return Task.FromResult(default(T?));

        }
    }
}
