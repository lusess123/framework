using System.Collections;
using Ataw.Framework.Core;
using System;
namespace Ataw.Framework.Web
{
    [CodePlug("PageFlyweight", BaseClass = typeof(IFlyweight),
      CreateDate = "2012-11-30", Author = "zhengyk", Description = "页面级别享元插件对象")]
    public class PageFlyweight : IFlyweight
    {
        public IDictionary PageItems
        {
            get
            {
                return GlobalVariable.PageItems;
            }
        }



        public string CreateSingletonRegName<T>(string name)
        {
            return string.Format(ObjectUtil.SysCulture, "{0}.{1}", typeof(T).ToString(), name);
        }

        public T GetSingleton<T>(string typeRegName)
        {
            string regName = CreateSingletonRegName<T>(typeRegName);
            if (PageItems.Contains(regName))
            {
                if (PageItems[regName] is T)
                {
                    return (T)PageItems[regName];
                }
            }
            PageItems[regName] = typeRegName.PlugGet<T>();
            return (T)PageItems[regName];
        }

        public void SetSingleton<T>(string typeRegName, T obj)
        {
            string regName = CreateSingletonRegName<T>(typeRegName);
            PageItems[regName] = obj;
        }

        public void RemoveAll()
        {
            // throw new System.NotImplementedException();
        }

        public void Remove<T>(string classRegName, string instanceName)
        {
            string regName = CreateInstanceRegName<T>(classRegName, instanceName);
            if (PageItems.Contains(regName))
            {
                PageItems.Remove(regName);
            }
        }

        public string CreateInstanceRegName<T>(string classRegName, string instanceName)
        {
            return string.Format(ObjectUtil.SysCulture, "{0}.{1}.{2}", typeof(T).ToString(), classRegName, instanceName);
        }

        public T GetInstance<T>(string classRegName, string instanceName)
        {
            string regName = CreateInstanceRegName<T>(classRegName, instanceName);
            if (PageItems.Contains(regName))
            {
                if (PageItems[regName] is T)
                {
                    return (T)PageItems[regName];
                }
            }
            PageItems[regName] = classRegName.PlugGet<T>();
            return (T)PageItems[regName];
        }

        public void SetInstance<T>(string classRegName, string instanceName, T obj)
        {
            string regName = CreateInstanceRegName<T>(classRegName, instanceName);
            //T obj = classRegName.PlugGet<T>();
            PageItems[regName] = obj;
        }

        private string GetRegName<T>(string name) {
            return "GetRegName_{0}_{1}".AkFormat(typeof(T).ToString(),name);
        }

        public T Get<T>(string name)
        {
            string regName = GetRegName<T>(name);
            if (PageItems.Contains(regName))
            {
                if (PageItems[regName] is T)
                {
                    AtawTrace.WriteFile(LogType.PageCacheHit,
                  "命中注册 ： {0} {1} 类型 ： {2} {1}".AkFormat(regName,Environment.NewLine ,typeof(T).Name ));
                    return (T)PageItems[regName];
                }
            }
            return default(T);
        }

        public void Set<T>(string name, T obj)
        {
            string regName = GetRegName<T>(name);

            PageItems[regName] = obj;
        }

        public T GetAndSet<T>(string name, System.Func<T> fun)
        {
            T a = Get<T>(name);
            if ( a == null)
            {
                a =  fun();
                this.Set<T>(name,a);
            }
            return a;
        }
    }
}
