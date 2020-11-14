using System;
using Croco.Core.Abstractions.Cache;

namespace FocLab.Implementations
{
    public class ApplicationCacheValue : ICrocoCacheValue
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public DateTime? AbsoluteExpiration { get; set; }
    }
}