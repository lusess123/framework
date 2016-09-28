using System.ComponentModel;

namespace Ataw.Framework.Core
{

    public enum MessageTypeEnum
    {
        //工作流提醒 = 0,
        //短信费用不足提醒 = 1,
        //邮件 = 2,
        //公告 = 3,
        //订单=4
        [Description("工作流提醒")]
        WorkFlow = 0,
        [Description("短信费用不足提醒")]
        SMSMessages = 1,
        [Description("邮件")]
        EMail = 2,
        [Description("公告")]
        Notice = 3,
        [Description("订单")]
        Order = 4,
        [Description("站内消息")]
        Message = 5,
        [Description("GPS报警")]
        GPS = 6
    }

}
