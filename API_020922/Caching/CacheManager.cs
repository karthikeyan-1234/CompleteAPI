using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace API_020922.Caching
{
#pragma warning disable CS8603 // Possible null reference return.
    public class CacheManager : ICacheManager
    {
        IDistributedCache cache;
        DistributedCacheEntryOptions options;

        public CacheManager(IDistributedCache cache)
        {
            this.cache = cache;
            options = new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30), SlidingExpiration = TimeSpan.FromSeconds(15) };
        }

        public async Task TrySetAsync<T>(string key, T entry)
        {
            var str = JsonConvert.SerializeObject(entry);
            await cache.SetStringAsync(key, str);
        }

        public async Task<T> TryGetAsync<T>(string key)
        {
            var str = await cache.GetStringAsync(key);
            if (str != null)
                return JsonConvert.DeserializeObject<T>(str);
            return default(T?);
        }
    }
#pragma warning restore CS8603 // Possible null reference return.

}
