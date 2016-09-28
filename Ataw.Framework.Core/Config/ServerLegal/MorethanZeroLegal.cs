using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    //与此插件对应的客户端JS验证在AtawTextLegal.js中
    [CodePlug("MorethanZeroLegal", BaseClass = typeof(IServerLegal),
     CreateDate = "2014-01-10", Author = "zgl", Description = "服务端非负数验证插件")]
    class MorethanZeroLegal : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            Regex reg = new Regex("^(?!(0[0-9]{0,}$))[0-9]{1,}[.]{0,}[0-9]{0,}$");
            if (!reg.IsMatch(value))
            {
                isLegal = false;
                error = "必须为非负数且大于0";
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}
