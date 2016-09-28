using System.Collections.Generic;
using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;

namespace Ataw.Workflow.Core
{
    public interface IActor
    {
        IEnumerable<string> GetActors(WF_WORKFLOW_INST mainRow, IUnitOfData source);
    }
}
