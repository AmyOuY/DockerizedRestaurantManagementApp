using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RMDataLibrary.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task SetEntryAsync<T>(this IDistributedCache cache,
            string key,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? idleExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = idleExpireTime;

            var jsonData = JsonSerializer.Serialize(data);

            await cache.SetStringAsync(key, jsonData, options);
        }


        public static async Task<T> GetEntryAsync<T>(this IDistributedCache cache, string key)
        {
            var jsonData = await cache.GetStringAsync(key);

            if (jsonData is null)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
