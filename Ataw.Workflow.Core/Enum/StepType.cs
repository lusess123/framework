using System.ComponentModel;
using Ataw.Framework.Core;
namespace Ataw.Workflow.Core
{
    [CodePlug("StepType", Author = "ace", Description = "步骤类型")]
    public enum StepType
    {
        [Description("未定义")]
        None = 0,
        [Description("开始")]
        Begin = 1,
        [Description("结束")]
        End = 2,
        [Description("人工")]
        Manual = 3,
        [Description("路由")]
        Route = 4,
        [Description("自动")]
        Auto = 5,
        [Description("聚合")]
        Merge = 6
    }
}
