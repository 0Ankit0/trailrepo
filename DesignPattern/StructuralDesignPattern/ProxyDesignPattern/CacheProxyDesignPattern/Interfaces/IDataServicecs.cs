namespace CacheProxyDesignPattern.Interfaces
{
    public interface IDataService
    {
        Task<string> GetDataAsync(string key);
    }

}
