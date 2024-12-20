using CacheProxyDesignPattern.Interfaces;

namespace CacheProxyDesignPattern.Services
{
    public class RealDataService : IDataService
    {
        // In-memory store for key-value pairs
        private static readonly Dictionary<string, string> _dataStore = new Dictionary<string, string>();

        public async Task<string> GetDataAsync(string key)
        {
            await Task.Delay(500); // Simulate latency

            // Check if the key exists in the store
            if (_dataStore.TryGetValue(key, out var value))
            {
                return value;
            }
            else
            {
                return $"No data found for key: {key}";
            }
        }

        public async Task<string> SetDataAsync(string key, string value)
        {
            await Task.Delay(500); // Simulate latency

            // Store or update the value
            _dataStore[key] = value;
            return $"Data for key: {key} set to: {value}";
        }

        public async Task<string> DeleteDataAsync(string key)
        {
            await Task.Delay(500); // Simulate latency

            if (_dataStore.Remove(key))
            {
                return $"Data for key: {key} deleted.";
            }
            else
            {
                return $"No data found for key: {key}, nothing deleted.";
            }
        }
    }
}
