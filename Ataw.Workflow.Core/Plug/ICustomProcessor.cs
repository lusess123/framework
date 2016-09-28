using Ataw.Workflow.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Workflow.Core.Plug
{
    public interface ICustomProcessor
    {
        WorkflowDbContext DbContext { get; set; }
        WF_WORKFLOW_INST WorkflowInst { get; set; }
        string PostJson { get; set; }
        void Exe();
    }
}
