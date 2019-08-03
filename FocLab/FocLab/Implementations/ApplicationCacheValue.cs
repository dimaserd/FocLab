using System;
using Croco.Core.Cache.Abstractions;

namespace FocLab.Implementations
{
    public class ApplicationCacheValue : ICrocoCacheValue
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public DateTime? AbsoluteExpiration { get; set; }
    }
}
