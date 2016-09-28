using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    //与此插件对应的客户端JS验证在AtawTextLegal.js中
    [CodePlug("username", BaseClass = typeof(IServerLegal),
     CreateDate = "2013-08-09", Author = "zgl", Description = "服务端验证电话号码插件")]
    class UserName : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            Regex reg = new Regex("^[a-zA-Z]{1}([a-zA-Z0-9]|[._]){4,19}$");
            if (!reg.IsMatch(value))
            {
                isLegal = false;
                error = "只能输入5-20个以字母开头、可带数字、“_”、“.”的字串!";
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}

