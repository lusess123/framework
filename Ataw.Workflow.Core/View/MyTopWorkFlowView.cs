using Ataw.Workflow.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Workflow.Core.View
{
  public  class MyTopWorkFlowView
    {
      public DateTime? CreateTime { get; set; }
      public int WorkflowCount { get; set; }
      public List<WorkflowDefView> WorkflowDefs { get; set; }

    }
}
