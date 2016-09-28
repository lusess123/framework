using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    [CodePlug("nonnegativeInteger", BaseClass = typeof(IServerLegal),
     CreateDate = "2013-08-12", Author = "zgl", Description = "服务端验证非负整数插件")]
    public class NonnegativeInteger : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            Regex reg = new Regex("^([1-9]\\d{0,}|0)$");
            if (!reg.IsMatch(value))
            {
                isLegal = false;
                error = "必须为非负整数";
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}

