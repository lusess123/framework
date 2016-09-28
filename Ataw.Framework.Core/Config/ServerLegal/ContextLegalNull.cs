using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    [CodePlug("ContextLegalNull", BaseClass = typeof(IServerLegal),
     CreateDate = "2013-08-12", Author = "zgl", Description = "服务端验证备注插件（可以为空）")]
    class ContextLegalNull : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            Regex reg = new Regex("^.{1,200}$");
            if (value != null && value != "")
            {
                if (!reg.IsMatch(value))
                {
                    isLegal = false;
                    error = "备注介绍不得大于200字";
                }
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}
