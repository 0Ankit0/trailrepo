using System.Collections.Concurrent;

namespace SingletonDesignPattern.Service
{
    public sealed class SharedDataManager
    {
        private static readonly Lazy<SharedDataManager> instance =
            new Lazy<SharedDataManager>(() => new SharedDataManager());

        private readonly ConcurrentDictionary<string, string> sharedData;

        private SharedDataManager()
        {
            sharedData = new ConcurrentDictionary<string, string>();
        }

        public static SharedDataManager Instance
        {
            get
            {
                return instance.Value;
            }
        }

        // Method to add or update data in the dictionary
        public void AddOrUpdate(string key, string value)
        {
            sharedData[key] = value;
        }

        // Method to add a new key-value pair if the key doesn't already exist
        public bool TryAdd(string key, string value)
        {
            // Try adding the key-value pair to the dictionary; return false if the key already exists
            return sharedData.TryAdd(key, value);
        }

        // Method to get a value by key
        public string Get(string key)
        {
            return sharedData.TryGetValue(key, out var value) ? value : null;
        }

        // Method to get all data
        public ConcurrentDictionary<string, string> GetAllData()
        {
            return new ConcurrentDictionary<string, string>(sharedData);
        }
    }
}
