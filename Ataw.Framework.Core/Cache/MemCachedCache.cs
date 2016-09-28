using System;
using System.Collections.Generic;
using System.Linq;
using Ataw.Framework.Core.Cache;
namespace Ataw.Framework.Core
{
    public class MemCachedCache : IAtawCache
    {


        public static MemCachedCache Current
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new MemCachedCache();
                            //Cache = MemoryCache.Default;
                            //  Current = new MemCachedCache();
                            var xml = AtawAppContext.Current.ApplicationXml.Memcached;
                            string[] serverlist = xml.ServerList.ToArray();

                            // initialize the pool for memcache servers
                            SockIOPool pool = SockIOPool.GetInstance();

                            pool.SetServers(serverlist);

                            pool.InitConnections = xml.Init;
                            pool.MinConnections = xml.Min;
                            pool.MaxConnections = xml.Max;

                            pool.SocketConnectTimeout = 1000;
                            pool.SocketTimeout = 3000;

                            pool.MaintenanceSleep = 30;
                            pool.Failover = true;

                            pool.Nagle = false;
                            pool.Initialize();

                            instance.mc = new MemcachedClient();
                            instance.mc.EnableCompression = false;
                        }
                    }
                }
                return instance;
            }
        }

        private static object _lock = new object();
        private static MemCachedCache instance;
        //  private SockIOPool pool = null;

        private MemcachedClient mc = null;

        public T Get<T>(string key)
        {
            return mc.Get(key).Value<T>();
        }

        public IList<string> Getkey()
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T obj, string dependOn)
        {
            mc.Set(key, obj);
        }

        public void Set<T>(string key, T obj, DateTime expiration, string dependOn)
        {
            throw new NotImplementedException();
        }

        public void Delete(string key)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string key)
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }
    }
}
