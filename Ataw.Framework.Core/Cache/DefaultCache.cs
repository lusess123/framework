using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Caching;

namespace Ataw.Framework.Core
{
   public class DefaultAtawCache:IAtawCache
    {

       public static  DefaultAtawCache Current
       {
           get;
           private set;
       }

       private static ObjectCache Cache;

       static DefaultAtawCache()
       {
           Cache = MemoryCache.Default;
           Current = new DefaultAtawCache();
       }

        public T Get<T>(string key)
        {
            if ((Cache.Contains(key)))
            {
                AtawTrace.WriteFile(LogType.AppCacheHit,
                 "命中注册 ： {0} {1} 类型 ： {2} {1}".AkFormat(key, Environment.NewLine, typeof(T).Name));
               // return (T)PageItems[regName];
                return (T)Cache[key];
            }
            else

                 return default(T);
            
        }

        public IList<string> Getkey()
        {
            return null;
        }

        public void Set<T>(string key, T obj, string dependOn)
        {
             var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now.AddYears(1);
            Cache.Add(new CacheItem(key, obj), policy);
        }

        public void Set<T>(string key, T obj, DateTime expiration, string dependOn)
        {
            if (obj == null)
                return;

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = expiration;
            Cache.Add(new CacheItem(key, obj), policy);
        }


        public void Delete(string key)
        {
            Cache.Remove(key);
        }

        public bool Exists(string key)
        {
            return (Cache.Contains(key));
        }

        public void Flush()
        {
            foreach (var item in Cache)
                Delete(item.Key);
        }
    }
}
