using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Workflow.Core
{
    [CodePlug("ErrorProcessType", Author = "ace", Description = "错误处理类型")]
    public enum ErrorProcessType
    {
        [Description("终止")]
        Abort,
        [Description("重试")]
        Retry
    }
}
