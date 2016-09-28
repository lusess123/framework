using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("notNull", BaseClass = typeof(IServerLegal),
     CreateDate = "2013-08-09", Author = "zgl", Description = "服务端验证非空插件")]
    public class NotNullServerLegal : IServerLegal
    {
      public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            string name = colConfig.Name;
            string displayName = colConfig.DisplayName;
            string value = data.Row[name].ToString();
            bool IsLegal = true;
            string error = "";
            if (value == "" || value == null)
            {
                IsLegal = false;
                error = "{0}的值不能为空".AkFormat(displayName);
            }
            return new LegalObject { IsLegal = IsLegal, ErrorMessage = error };
        }
    }
}
