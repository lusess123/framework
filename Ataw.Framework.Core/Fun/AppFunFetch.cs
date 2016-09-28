using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
     [CodePlug("App", BaseClass = typeof(IFunFetch),
   CreateDate = "2014-4-18", Author = "", Description = "全局配置开关")]
   public class AppFunFetch:BaseFunFetch
    {

        public override int Fetch()
        {
            var bean = AtawAppContext.Current.ApplicationXml.AppSettings[RightKey];
            if (bean != null)
            {
                return bean.Value.Value<bool>()?1:0;
            }
            return 0;

            //throw new NotImplementedException();
        }
    }
}
