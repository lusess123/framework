using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    public interface IAtawCache
    {
        T Get<T>(string key);
        IList<string> Getkey();
        void Set<T>(string key, T obj,string dependOn);
        void Set<T>(string key, T obj, DateTime expiration, string dependOn);
       // void Set<T>(string key, T obj,TimeSpan expriration);
        void Delete(string key);
        bool Exists(string key);
        void  Flush();
    }
}
