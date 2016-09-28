using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    [CodePlug("MorethanZeroLegalNull", BaseClass = typeof(IServerLegal),
     CreateDate = "2014-01-10", Author = "zgl", Description = "服务端非负数验证可以为空插件")]
    class MorethanZeroLegalNull : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            Regex reg = new Regex("^(?!(0[0-9]{0,}$))[0-9]{1,}[.]{0,}[0-9]{0,}$");
            if (value != null && value != "")
            {
                if (!reg.IsMatch(value))
                {
                    isLegal = false;
                    error = "必须为非负数且大于0";
                }
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}
