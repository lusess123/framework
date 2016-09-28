using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    //与此插件对应的客户端JS验证在AtawTextLegal.js中
    [CodePlug("tel", BaseClass = typeof(IServerLegal),
     CreateDate = "2013-08-09", Author = "zgl", Description = "服务端验证电话号码插件")]
    class Tel : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            Regex reg = new Regex("^((0\\d{2,3})-)(\\d{7,8})(-(\\d{3,}))?$");
            if (!reg.IsMatch(value))
            {
                isLegal = false;
                error = "座机号码格式不对,必须为(0(2或者3位)-(7或者8位),如0571-8888888";
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}

