namespace CacheProxyDesignPattern.Interfaces
{
    public interface IDataService
    {
        Task<string> GetDataAsync(string key);
        Task<string> SetDataAsync(string key, string value);
        Task<string> DeleteDataAsync(string key);
    }

}
