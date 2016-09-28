using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Ataw.Framework.Web
{
   public sealed class GlobalContext
   {
       private const string Key_ContextName = "_GlobalContext";

       #region 单例模式

       public static GlobalContext Current
        {
            get
            {
                 return HttpContext.Current.Items[Key_ContextName] as GlobalContext;
            }
        }

       /// <summary>
        /// 创建上下文实例
        /// </summary>
        internal static void Create(GlobalContext context)
        {
            HttpContext.Current.Items[Key_ContextName] = context;
        }

        /// <summary>
        /// 移除上下文实例
        /// </summary>
        internal static void Remove()
        {
            HttpContext.Current.Items.Remove(Key_ContextName);
        }

       #endregion

       #region web 相关
       //public static 

       #endregion

   }
}
