using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    //与此插件对应的客户端JS验证在AtawTextLegal.js中
    [CodePlug("mobile", BaseClass = typeof(IServerLegal),
     CreateDate = "2013-08-09", Author = "zgl", Description = "服务端验证电话号码插件")]
    class Mobile : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            Regex reg = new Regex("^1[34578]\\d{9}$");
            if (!reg.IsMatch(value))
            {
                isLegal = false;
                error = "手机格式不对,必须以13、14、15、17、18开头并且为11位";
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}

