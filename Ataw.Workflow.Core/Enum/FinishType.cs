using Ataw.Framework.Core;
using System.ComponentModel;
namespace Ataw.Workflow.Core
{
    [CodePlug("FinishType", Author = "ace", Description = "完成状态")]
    public enum FinishType
    {
        [Description("-")]
        None = 0,
        /// <summary>
        /// 正常结束
        /// </summary>
        [Description("正常结束")]
        Normal = 1,
        /// <summary>
        /// 移除
        /// </summary>
        [Description("移除")]
        ModifiedNormal,
        /// <summary>
        /// 终止
        /// </summary>
        [Description("终止")]
        Abort,
        /// <summary>
        /// 异常
        /// </summary>
        [Description("异常")]
        Error,
        /// <summary>
        /// 超出重试次数
        /// </summary>
        [Description("超出重试次数")]
        OverTryTimes,
        /// <summary>
        /// 回退到开始
        /// </summary>
        [Description("回退到开始")]
        ReturnBegin
    }
}
