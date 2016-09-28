using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Workflow.Core.DataAccess.DA
{
   public class DAWorkflow
    {

       public DAWorkflow(WorkflowDbContext workflowDbContext)
       {
           this.WorkflowDbContext = workflowDbContext;
       }

       private WorkflowDbContext WorkflowDbContext
       {
           get;
           set;
       }
       public IQueryable<WorkflowInstView> GetWorkflowInstView()
       {
           return WorkflowDbContext.WF_WORKFLOW_INST.Join(WorkflowDbContext.WF_WORKFLOW_DEF
               , a => a.WI_WD_NAME, 
               b => b.WD_SHORT_NAME, 
               (a,b)=> new WorkflowInstView(){
                WF_WORKFLOW_INST =a , WF_WORKFLOW_DEF = b
               });

       }
       }
    }

