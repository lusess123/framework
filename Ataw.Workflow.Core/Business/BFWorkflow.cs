using Ataw.Workflow.Core.DataAccess;
using Ataw.Workflow.Core.DataAccess.DA;
using Ataw.Workflow.Core.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Workflow.Core.Business
{
   public  class BFWorkflow
    {
       public IEnumerable<WorkflowDefView> GetMyTopWorkFlowView()
       {
           WorkflowDbContext db = new WorkflowDbContext();
        //   db.WF_WORKFLOW_INST.Join(db.WF_WORKFLOW_DEF,a=>a.)
           DAWorkflow da = new DAWorkflow(db);
           var res = da.GetWorkflowInstView().ToList().GroupBy(a => a.WF_WORKFLOW_DEF.WD_SHORT_NAME).ToList();
             var ff =   res.Select(a => {

                   return new WorkflowDefView() { 
                    Title  = a.Key ,
                     WorkflowCount = a.Count(),
                      WorkflowInsts = a.Select(b=>b.WF_WORKFLOW_INST).ToList()
                   
                   };
               
               });


          return ff.ToList(); ;
       }

    }
}
