using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;

namespace Ataw.Workflow.Core
{
    public interface IConnection
    {
        bool Match(WF_WORKFLOW_INST mainRow, WorkflowContent content, IUnitOfData source);
    }
}
