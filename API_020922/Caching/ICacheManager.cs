namespace API_020922.Caching
{
    public interface ICacheManager
    {
        Task<T> TryGetAsync<T>(string key);
        Task TrySetAsync<T>(string key, T entry);
    }
}