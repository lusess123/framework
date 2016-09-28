using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    [CodePlug("customReg", BaseClass = typeof(IServerLegal),
     CreateDate = "2013-08-12", Author = "zgl", Description = "服务端验证自定义正则表达式插件")]
    class customReg : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            string reg = colConfig.ControlLegal.Reg;
            var match = Regex.Match(value, reg, RegexOptions.ECMAScript);
            if (!match.Success)
            {
                isLegal = false;
                error = "格式不正确！";
            }

            //if (!reg.IsMatch(value))
            //{
            //    isLegal = false;
            //    error = "电子邮箱格式不正确！";
            //}
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}
