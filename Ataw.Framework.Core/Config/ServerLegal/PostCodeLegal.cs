using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    [CodePlug("PostCodeLegal", BaseClass = typeof(IServerLegal),
     CreateDate = "2013-08-12", Author = "zgl", Description = "服务端验证邮政编码插件")]
    public class PostCodeLegal : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            Regex reg = new Regex("^\\d{6}$"); 
            if (!reg.IsMatch(value))
            {
                isLegal = false;
                error = "邮政编码为6为整数";
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}

