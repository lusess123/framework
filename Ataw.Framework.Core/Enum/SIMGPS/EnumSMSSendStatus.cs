using System.ComponentModel;

namespace Ataw.Framework.Core 
{
    public enum EnumSMSSendStatus
    {
        [Description("发送成功")]
        发送成功  = 1000,
        [Description("发送失败")]
         发送失败  = 1001,
        [Description("用户未设置密码")]
         用户未设置密码 = 1002,
        [Description("密码不对")]
         密码不对  = 1003,
        [Description("用户不存在")]
        用户不存在  = 1004,
        [Description("手机号码长度不正确")]
         手机号码长度不正确  = 1005,

                [Description("手机号码中有非数字字符")]
         手机号码中有非数字字符  = 1006,
                [Description("手机号码非法")]
         手机号码非法  = 1007,
                [Description("群发短信内容超过700字")]
                群发短信内容超过700字 = 1008,
                [Description("发送的手机号码(DestAddr)个数与参数(SMSCount)的值不一致")]
         发送的手机号码个数与参数的值不一致  = 1009,
                [Description("发送的短信内容为空")]
         发送的短信内容为空  = 1010,
                [Description("单条短信发送内容超过70字")]
         单条短信发送内容超过70字  = 1011
    }
}
