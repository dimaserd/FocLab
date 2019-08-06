﻿using System;
using System.Collections.Generic;
using System.Linq;
using Croco.Core.Cache.Abstractions;
using Croco.Core.Application;
using Croco.Core.Common.Utils;

namespace FocLab.Logic.Extensions
{
    public class CrocoCacheValue : ICrocoCacheValue
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public DateTime? AbsoluteExpiration { get; set; }
    }

    public static class QueryableCacheExtensions
    {
        public static IEnumerable<T> Cached<T>(this IQueryable<T> source) where T : class
        {
            var key = $"_cachedT{typeof(T).FullName}";

            var cacheValue = CrocoApp.Application.CacheManager.GetValue<List<T>>(key);

            if (cacheValue != null)
            {
                return cacheValue;
            }

            cacheValue = source.ToList();
            CrocoApp.Application.CacheManager.AddValue(new CrocoCacheValue
            {
                Key = key,
                Value = cacheValue,
                AbsoluteExpiration = CrocoApp.Application.DateTimeProvider.Now.AddHours(1)
            });

            return cacheValue;
        }
    }
}
