using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System.Text;

namespace AllPractice.Models
{
    public class CacheModel
    {
        public void SetMemoryCache(IMemoryCache memoryCache, string key, List<Work> cacheValue)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            cacheEntryOptions.SetSlidingExpiration(TimeSpan.FromSeconds(5));
            cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);

            memoryCache.Set(key, cacheValue, cacheEntryOptions);
        }
        public string GetOrSetTimeCache(IConnectionMultiplexer connectionMultiplexer, string key)
        {
            var xxx = connectionMultiplexer.GetDatabase().StringGet(key);
            if (!connectionMultiplexer.GetDatabase().StringGet(key).IsNull)
                return Encoding.UTF8.GetString(connectionMultiplexer.GetDatabase().StringGet(key));


            var currentTime = DateTime.UtcNow;
            byte[] encodedCurrentTime;

            currentTime = GetDiffTimeZone(key, currentTime);

            encodedCurrentTime = Encoding.UTF8.GetBytes(currentTime.ToString());
            if (key == "cachedTimeUTC")
                connectionMultiplexer.GetDatabase().StringSet(key, encodedCurrentTime, TimeSpan.FromSeconds(5));
            if (key == "cachedTimeTW")
                connectionMultiplexer.GetDatabase().StringSet(key, encodedCurrentTime);
            return Encoding.UTF8.GetString(connectionMultiplexer.GetDatabase().StringGet(key));
        }
        public DateTime GetDiffTimeZone(string key, DateTime currentTime)
        {
            if (key == "cachedTimeUTC")
            {
            }
            if (key == "cachedTimeTW")
            {
                TimeZoneInfo TWTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                currentTime = TimeZoneInfo.ConvertTimeFromUtc(currentTime, TWTimeZone);
            }

            return currentTime;
        }
    }
}
