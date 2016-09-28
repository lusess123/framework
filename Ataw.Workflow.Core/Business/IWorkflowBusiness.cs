using System.Text;
using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;
namespace Ataw.Workflow.Core
{
    interface IWorkflowBusiness
    {
        void BeginStepExecute(StepConfig stepConfig, int priority, string workflowName, WorkflowContent content, IUnitOfData context);
    }

    public class WorkflowBusiness : IWorkflowBusiness
    {

        public void BeginStepExecute(StepConfig stepConfig, int priority, string workflowName, WorkflowContent content, IUnitOfData context)
        {
            StringBuilder sb = new StringBuilder();
            content.SaveStringBuilder(sb);
            // throw new System.NotImplementedException();

            WorkflowDbContext wfContent = context as WorkflowDbContext;

            //ATAW_WORKFLOWContext wfContent = context as ATAW_WORKFLOWContext;
            WF_WORKFLOW_INST inst = new WF_WORKFLOW_INST();
            inst.WI_ID = wfContent.GetUniId();
            inst.WI_CONTENT_XML = sb.ToString();
            inst.WI_NAME = workflowName;
            wfContent.WF_WORKFLOW_INST.Add(inst);

            //步骤基本信息
            StepUtil.SetWorkflowByStep(inst, stepConfig.Name, stepConfig.DisplayName, wfContent.Now,
                (int)stepConfig.StepType, (int)StepState.ProcessNotSend);
            inst.WI_INDEX = 1;
            inst.WI_PRIORITY = priority;
            //扩展信息
            //父子流程
            //主表控制
            content.SetAllMainRow(wfContent, stepConfig, inst.WI_ID);
        }

        public void BeginStepExecute(int priority, string workflowName, string content, IUnitOfData context)
        {
            throw new System.NotImplementedException();
        }
    }
}
