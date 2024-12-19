using CacheProxyDesignPattern.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace CacheProxyDesignPattern.Services
{
    public class CacheProxyDataService : IDataService
    {
        private readonly IDataService _realDataService;
        private readonly IMemoryCache _cache;

        public CacheProxyDataService(IDataService realDataService, IMemoryCache cache)
        {
            _realDataService = realDataService;
            _cache = cache;
        }

        public async Task<string> GetDataAsync(string key)
        {
            if (_cache.TryGetValue(key, out string cachedData))
            {
                return cachedData; // Return cached data
            }

            // Fetch data from real service
            var data = await _realDataService.GetDataAsync(key);

            // Cache the data
            _cache.Set(key, data, TimeSpan.FromMinutes(5)); // Cache for 5 minutes

            return data;
        }
    }
}
