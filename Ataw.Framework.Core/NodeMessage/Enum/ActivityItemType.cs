using System.ComponentModel;

namespace Ataw.Framework.Core
{
    [CodePlug("ActivityItemType", Author = "sj", Description = "动态项目类型")]
    public enum ActivityItemType
    {
        [Description("发布微博")]
        CreateMicroBlog = 0,
        [Description("评论微博")]
        CommentMicroBlog = 1,
        [Description("新增文章")]
        CreateArticle = 2,
        [Description("上传图片")]
        CreatePicture = 3,
        [Description("上传文件")]
        CreateDocument = 4,
        [Description("工作流")]
        WorkFlow = 5,
        [Description("签到")]
        Signin = 6,
        [Description("申请加入圈子")]
        ApplyClub = 7,
        [Description("加入圈子")]
        JoinClub = 8,
        [Description("退出圈子")]
        QuitClub = 9,
        [Description("我的工作")]
        MyWork = 10,
        [Description("发送邮件")]
        SendMail = 11,
        [Description("待办事项")]
        ToDoItems = 12,
        [Description("系统公告")]
        SystemNotice = 13,
        [Description("页面帮助")]
        PageHelp = 14,
        [Description("投票")]
        Vote = 15,
        [Description("回复微博")]
        ReplyMicroBlog = 16,
        [Description("未分类")]
        Else = 100
    }
}
