using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    //与此插件对应的客户端JS验证在AtawTextLegal.js中
    [CodePlug("email", BaseClass = typeof(IServerLegal),
     CreateDate = "2013-08-12", Author = "zgl", Description = "服务端验证邮箱地址插件")]
    class Email : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            Regex reg = new Regex("^([a-zA-Z0-9]+[_|_|.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|_|.]?)*[a-zA-Z0-9]+\\.(?:com|cn)$");
            if (!reg.IsMatch(value))
            {
                isLegal = false;
                error = "邮箱格式不对，格式如admin@163.com";
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}
