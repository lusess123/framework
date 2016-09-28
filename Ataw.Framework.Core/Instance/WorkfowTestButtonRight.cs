using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    [CodePlug("WorkfowTest", BaseClass = typeof(IButtonRight),
       CreateDate = "2013-1-07", Author = "zhengyk", Description = "工作流测试用的权限插件")]
    public class WorkfowTestButtonRight : IButtonRight
    {
        int i = 0;
        public string GetButtons(ObjectData data, IEnumerable<ObjectData> listData)
        {
            if (i == 1)
            {
                i++;
                return "Detail";
            }
            else
            {
                i++;
                return "Update|Detail|Delete";
            }

        }
    }
}
