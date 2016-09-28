using System.ComponentModel;
using Ataw.Framework.Core;
namespace Ataw.Workflow.Core
{
    [CodePlug("StepState", Author = "ace", Description = "步骤状态")]
    public enum StepState
    {
        [Description("未接收")]
        NotReceive = 0,
        [Description("接收未打开")]
        ReceiveNotOpen = 1,
        [Description("打开未处理")]
        OpenNotProcess = 2,
        [Description("处理未发送")]
        ProcessNotSend = 3,
        [Description("错误")]
        Mistake = 4
    }
}
