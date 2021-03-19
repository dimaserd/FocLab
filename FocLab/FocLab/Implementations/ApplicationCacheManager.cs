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
            var offSet = cacheValue.AbsoluteExpiration.HasValue? new DateTimeOffset(cacheValue.AbsoluteExpiration.Value) : DateTimeOffset.MaxValue;

            _cache.Set(cacheValue.Key, cacheValue.Value, offSet);
        }

        public T GetOrAddValue<T>(string key, Func<T> valueFactory, DateTime? absoluteExpiration = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOrAddValueAsync<T>(string key, Func<Task<T>> valueFactory, DateTime? absoluteExpiration = null)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public CrocoSafeValue<T> GetValue<T>(string key)
        {
            throw new NotImplementedException();
        }
    }
}
