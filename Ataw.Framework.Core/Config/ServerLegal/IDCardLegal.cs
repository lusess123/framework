﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    [CodePlug("IDCardLegal", BaseClass = typeof(IServerLegal),
     CreateDate = "2013-08-12", Author = "zgl", Description = "服务端验证身份证插件")]
    class IDCardLegal : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            Regex reg = new Regex("(^\\d{15}$)|(^\\d{17}([0-9]|X)$)");
            if (!reg.IsMatch(value))
            {
                isLegal = false;
                error = "身份证号码格式不正确"; 
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}
