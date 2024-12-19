using CacheProxyDesignPattern.Interfaces;

namespace CacheProxyDesignPattern.Services
{
    public class RealDataService : IDataService
    {
        public async Task<string> GetDataAsync(string key)
        {
            // Simulate data retrieval
            await Task.Delay(500); // Simulate latency
            return $"Data for key: {key}";
        }
    }

}
