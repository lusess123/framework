using System.ComponentModel;
namespace Ataw.Workflow.Core
{
    public enum MistakeReason
    {
        [Description("无")]
        None = 0,
        [Description("插件错误")]
        PlugInError = 1,
        [Description("人员错误")]
        NoActor = 2,
        [Description("路由错误")]
        NoRoute = 3
    }
}
