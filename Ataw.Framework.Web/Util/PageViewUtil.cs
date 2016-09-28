using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ataw.Framework.Core;
using System.Data;
using Newtonsoft.Json;

namespace Ataw.Framework.Web
{
   public static class PageViewUtil
    {

       public static AtawPageConfigView Module(string data, string ds, string xml, string pageStyle, string keyValue)
       {
           //AtawDebug.AssertNotNullOrEmpty(xml, "亲 ,modulexml 注册名不可以为空的", controller);
           ModuleConfig mc = xml.SingletonByPage<ModuleConfig>();
           if (mc.Mode == ModuleMode.None)
           {
               throw new AtawException("ModuleXml的Mode节点不能为空", xml);
           }

           AtawBasePageViewCreator pageCreator = (pageStyle + "PageView").SingletonByPage<AtawBasePageViewCreator>();
           pageCreator.Initialize(mc, JsonConvert.DeserializeObject<DataSet>(ds ?? ""), keyValue, "", false);
           var apcv = pageCreator.Create();
           apcv.RegName = xml;

           return apcv;
       }

    }
}
