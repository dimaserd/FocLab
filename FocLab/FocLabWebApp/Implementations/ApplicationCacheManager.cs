using System;
using System.Threading.Tasks;
using Croco.Core.Contract.Cache;
using Croco.Core.Contract.Models;
using Croco.Core.Contract.Models.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace FocLab.Implementations
{
    public class ApplicationCacheManager : ICrocoCacheManager
    {
        private readonly IMemoryCache _cache;

        public ApplicationCacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void AddValue(CrocoCacheValue cacheValue)
        {
            var offSet = cacheValue.AbsoluteExpiration.HasValue ? new DateTimeOffset(cacheValue.AbsoluteExpiration.Value) : DateTimeOffset.MaxValue;

            _cache.Set(cacheValue.Key, cacheValue.Value, offSet);
        }

        public T GetOrAddValue<T>(string key, Func<T> valueFactory, DateTime? absoluteExpiration = null)
        {
            var res = _cache.TryGetValue(key, out T result);

            if (res)
            {
                return result;
            }

            result = valueFactory();

            AddValue(new CrocoCacheValue
            {
                Key = key,
                Value = result,
                AbsoluteExpiration = absoluteExpiration ?? DateTime.MaxValue
            });

            return result;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public CrocoSafeValue<T> GetValue<T>(string key)
        {
            var res = _cache.TryGetValue(key, out T result);

            return new CrocoSafeValue<T>(res, result);
        }

        public async Task<T> GetOrAddValueAsync<T>(string key, Func<Task<T>> valueFactory, DateTime? absoluteExpiration = null)
        {
            var res = _cache.TryGetValue(key, out T result);

            if (res)
            {
                return result;
            }

            result = await valueFactory();

            AddValue(new CrocoCacheValue
            {
                Key = key,
                Value = result,
                AbsoluteExpiration = absoluteExpiration ?? DateTime.MaxValue
            });

            return result;
        }
    }
}