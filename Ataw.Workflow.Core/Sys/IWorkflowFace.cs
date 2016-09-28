using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Workflow.Core
{
   public interface IWorkflowFace
    {
       InstState ExeProcessPage(string wid);
    }
}
