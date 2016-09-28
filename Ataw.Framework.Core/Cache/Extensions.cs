using System;

namespace Ataw.Framework.Core
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class CacheExtensions
    {

        public static T GetAndSet<T>(this IAtawCache cacheManager, string key,  Func<T> acquire,string dependOn = "") 
        {
            if (cacheManager.Exists(key))
            {
                return cacheManager.Get<T>(key);
            }
            else
            {
                var result = acquire();
                if (result != null)
                {
                    cacheManager.Set(key, result, dependOn);
                }
                return result;
            }
        }
    }
}
