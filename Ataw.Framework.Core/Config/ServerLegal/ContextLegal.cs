using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    [CodePlug("ContextLegal", BaseClass = typeof(IServerLegal),
     CreateDate = "2013-08-12", Author = "zgl", Description = "服务端验证备注插件")]
    public class ContextLegal : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            string ssss = DateTime.Now.ToString();
            Regex reg = new Regex("^.{1,200}$");
            if (value == null || value == "")
            {
                isLegal = false;
                error = "备注不能为空，请确认！";
            }
            else if (!reg.IsMatch(value))
            {
                    isLegal = false;
                    error = "备注介绍不得大于200字";
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}
