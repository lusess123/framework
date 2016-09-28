using Ataw.Framework.Core;

namespace Ataw.Workflow.Core.Plug
{
    [CodePlug(REG_NAME, BaseClass = typeof(IWorkflowDefBuilder),
 CreateDate = "2012-11-16", Author = "ace", Description = "工作流模式复制")]
    public class WorflowDefBuilder : IWorkflowDefBuilder
    {
        public const string REG_NAME = "WorflowDefBuilder";
        public int CopyWorkflowDef(string fControlUnitID)
        {
            return 1;
        }
    }
}
