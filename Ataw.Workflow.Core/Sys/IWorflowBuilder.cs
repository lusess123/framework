using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Workflow.Core.Sys
{
    interface IWorflowBuilder
    {
        Workflow StartWorkFlow(string key, string defineName, string creatorId);

      //  string Con { get; }
    }
}
