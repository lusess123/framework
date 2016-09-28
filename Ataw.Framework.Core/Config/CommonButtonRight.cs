using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    [CodePlug("Common", BaseClass = typeof(IButtonRight),
        CreateDate = "2012-11-24", Author = "sj", Description = "通用Button权限插件")]
    public class CommonButtonRight : IButtonRight
    {
        public string GetButtons(ObjectData data, IEnumerable<ObjectData> listData)
        {
            return "Update|Detail|Delete";
        }
    }
}
