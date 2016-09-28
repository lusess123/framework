using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Workflow.Core
{
    [CodePlug("ApprovalOpinion", Author = "ace", Description = "审核意见")]
    public enum ApprovalOpinion
    {
        [Description("不同意")]
        No = 0,
        [Description("同意")]
        Yes
    }
}
