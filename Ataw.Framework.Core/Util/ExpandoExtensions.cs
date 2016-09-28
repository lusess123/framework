using System.Collections.Generic;
using System.Dynamic;

namespace Ataw.Framework.Core
{
    public static class ExpandoExtensions
    {
        public static void SetPro(this ExpandoObject expando, string name, object obj)
        {
            IDictionary<string, object> oo = (IDictionary<string, object>)expando;
            oo.Add(name, obj);
        }

        public static void AddPro(this ExpandoObject expando, string name, object obj)
        {
            IDictionary<string, object> oo = (IDictionary<string, object>)expando;
            string mesg = string.Format(ObjectUtil.SysCulture, "已经有成员{0}了，再添加就要覆盖了", name);
            AtawDebug.Assert(!oo.ContainsKey(name), mesg, expando);
            //if (oo.ContainsKey(name)) ;
            oo.Add(name, obj);
            // SetPro();
            // oo.Add(name, obj);
        }
    }
}
