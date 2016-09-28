using Ataw.Workflow.Core.DataAccess;
namespace Ataw.Workflow.Core
{
    public abstract class AutoProcessor : BaseProcessor
    {
        protected AutoProcessor()
        {
        }

        public abstract void Execute(WF_WORKFLOW_INST workflowRow);
    }
}
