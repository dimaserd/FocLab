using System.Collections.Generic;
using System.Linq;
using Croco.Core.Application;
using Croco.Core.Contract.Models.Cache;

namespace FocLab.App.Logic.Extensions
{
    public static class QueryableCacheExtensions
    {
        public static IEnumerable<T> Cached<T>(this IQueryable<T> source) where T : class
        {
            var key = $"_cachedT{typeof(T).FullName}";

            var cacheValue = CrocoApp.Application.CacheManager.GetValue<List<T>>(key);

            if (cacheValue.IsSucceeded)
            {
                return cacheValue.Value;
            }

            var result = source.ToList();

            CrocoApp.Application.CacheManager.AddValue(new CrocoCacheValue
            {
                Key = key,
                Value = result,
                AbsoluteExpiration = CrocoApp.Application.DateTimeProvider.Now.AddHours(1)
            });

            return result;
        }
    }
}