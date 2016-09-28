
using System;
using System.Collections;
namespace Ataw.Framework.Core
{
    public interface IFlyweight
    {
        IDictionary PageItems { get; }

        string CreateSingletonRegName<T>(string classRegName);
        T GetSingleton<T>(string classRegName);
        void SetSingleton<T>(string classRegName, T obj);

        void RemoveAll();
        void Remove<T>(string classRegName, string instanceName);

        string CreateInstanceRegName<T>(string classRegName, string instanceName);
        T GetInstance<T>(string classRegName, string instanceName);
        void SetInstance<T>(string classRegName, string instanceName, T obj);

        T Get<T>(string name);
        void Set<T>(string name, T obj);
         T GetAndSet<T>(string name , Func<T> fun);

    }
}
