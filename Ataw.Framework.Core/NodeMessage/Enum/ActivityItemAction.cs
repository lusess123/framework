using System.ComponentModel;

namespace Ataw.Framework.Core
{
    [CodePlug("ActivityItemAction", Author = "sj", Description = "动态项目操作")]
    public enum ActivityItemAction
    {
        [Description("新增")]
        Create= 0,
        [Description("评论")]
        Comment = 1,
    }
}
