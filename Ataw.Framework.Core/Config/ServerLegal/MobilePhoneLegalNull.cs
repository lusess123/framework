using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    [CodePlug("MobilePhoneLegalNull", BaseClass = typeof(IServerLegal),
     CreateDate = "2013-08-09", Author = "zgl", Description = "服务端验证电话号码插件（可以为空）")]
    class MobilePhoneLegalNull : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            Regex reg = new Regex("(^1[3578]\\d{9}$)|(^((0\\d{2,3})-)(\\d{7,8})(-(\\d{3,}))?$)");
            if (value != null && value != "")
            {
                if (!reg.IsMatch(value))
                {
                    isLegal = false;
                    error = "必须是座机号或手机号，座机号格式为(0(2或者3位)-(7或者8位)";
                }
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}

