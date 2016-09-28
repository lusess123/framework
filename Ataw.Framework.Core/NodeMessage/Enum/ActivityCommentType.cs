using System.ComponentModel;

namespace Ataw.Framework.Core
{
    [CodePlug("ActivityCommentType", Author = "cl", Description = "动态评论类别")]
    public enum ActivityCommentType
    {
        [Description("微博")]
        MicroBlog = 0,
        [Description("签到")]
        Signin = 1,
        [Description("工作流")]
        WorkFlow = 2
    }
}

