using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    [CodePlug("PriceLegal", BaseClass = typeof(IServerLegal),
     CreateDate = "2013-08-12", Author = "zgl", Description = "服务端验证价格插件")]
    public class PriceLegal : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            Regex reg = new Regex("^\\d+(\\.\\d{1,2})?$");
            if (!reg.IsMatch(value)) 
            {
                    isLegal = false;
                    error = "{0}必须是整数或小数点后1-2位的小数".AkFormat(displayName);
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}


