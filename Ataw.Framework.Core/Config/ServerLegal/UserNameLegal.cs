using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    [CodePlug("UserNameLegal", BaseClass = typeof(IServerLegal),
        CreateDate = "2013-08-12", Author = "zgl", Description = "服务端验证用户名插件")]
    class UserNameLegal : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            Regex reg = new Regex("^[0-9a-zA-Z]w{5,17}$");
            if (!reg.IsMatch(value))
            {
                isLegal = false;
                error = "用户名长度在6-18位之间，只能包含字符、数字、下划线";
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}
