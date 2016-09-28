using Ataw.Workflow.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Workflow.Core.View
{
  public  class WorkflowDefView
    {
      public WorkflowDefView()
      {
          WorkflowInsts = new List<WF_WORKFLOW_INST>();
      }

      public string Title { get; set; }
      public int WorkflowCount { get; set; }

      public List<WF_WORKFLOW_INST> WorkflowInsts { get; set; }

    }
}
