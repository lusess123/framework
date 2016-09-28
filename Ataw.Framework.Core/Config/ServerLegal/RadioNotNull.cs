using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{
    [CodePlug("RadioNotNull", BaseClass = typeof(IServerLegal),
        CreateDate = "2013-08-12", Author = "zgl", Description = "服务端验证单选非空插件")]
    class RadioNotNull : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool isLegal = true;
            string error = "";
            if (value == null || value == "")
            {
                isLegal = false;
                error = "请选择{0}".AkFormat(displayName);
            }
            return new LegalObject { IsLegal = isLegal, ErrorMessage = error };
        }
    }
}
