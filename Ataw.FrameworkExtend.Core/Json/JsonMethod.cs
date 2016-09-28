using Ataw.Framework.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.FrameworkExtend.Core.Json
{
    [CodePlug("JsonMethod", BaseClass = typeof(IMethod),
CreateDate = "2016-01-15", Author = "zhengyk", Description = "")]
    public class JsonMethod : BaseSingleMethod
    {

       private static string SetJson(string _json){
           if (_json.Length > 5)
           {
               if (_json.ToUpper().IndexOf(".JSON") == (_json.Length - 5))
               {
                   return _json;
               }
           }
           return _json + ".json";
       }

        public override void Exe()
        {
            if (this.Params.AllKeys.ToList().IndexOf("json") >= 0)
            {
                string _json = this.Params["json"];
                _json = SetJson(_json);              
                string _path = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH, "json", _json);
                string json = FileUtil.ReadStringByFile(_path);
                ResultObj = AtawAppContext.Current.TryToObject<object>(json);
            }
            else {
                throw new AtawException("json参数不能为空",this);
            }


        }

       
    }
}
