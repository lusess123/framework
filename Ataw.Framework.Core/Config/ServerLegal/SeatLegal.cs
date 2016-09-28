using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
   [CodePlug("SeatLegal", BaseClass = typeof(IServerLegal),
       CreateDate = "20130-08-12", Author = "zgl", Description = "服务端验证座位插件")]
    class SeatLegal : IServerLegal
    {
       public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
       {
           string name = colConfig.Name;
           string displayName = colConfig.DisplayName;
           string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            Regex reg = new Regex("^[0-9]*[1-9][0-9]*$");
            if (!reg.IsMatch(value))
            {
                isLegal = false;
                error = "必须是大于0的整数";
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}
