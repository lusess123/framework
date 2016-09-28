using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Workflow.Core
{
    [CodePlug("FlowAction", Author = "ace", Description = "流转方式")]
    public enum FlowAction
    {
        [Description("流转")]
        Flow,
        [Description("回退")]
        Back
    }
}
