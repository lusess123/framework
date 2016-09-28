using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;

namespace Ataw.Workflow.Core
{
    public interface IPrivateModule
    {
        string GetModule(WF_WORKFLOW_INST mainRow, IUnitOfData source);
    }
}
