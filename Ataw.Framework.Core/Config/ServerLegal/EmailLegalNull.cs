using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    [CodePlug("EmailLegalNull", BaseClass = typeof(IServerLegal),
     CreateDate = "2013-08-09", Author = "zgl", Description = "服务端验证邮箱地址插件（可以为空）")]
    public class emailServerLegal : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            Regex reg = new Regex("^([a-zA-Z0-9]+[_|_|.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|_|.]?)*[a-zA-Z0-9]+\\.(?:com|cn)$");
            if (value != null && value != "")
            {
                if (!reg.IsMatch(value))
                {
                    isLegal = false;
                    error = "电子邮箱格式不正确";
                }
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}
