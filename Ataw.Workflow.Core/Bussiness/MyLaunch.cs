using System;
using System.Linq.Expressions;
using Ataw.Workflow.Core;
using Ataw.Workflow.Core.DataAccess;

namespace Ataw.Workflow.Web.Bussiness
{
    public class MyLaunch : MyWorkflowINST
    {
        protected override Action<BaseTree> ResultForEach
        {
            get
            {
                return (m => m.CurrentProcessUserID = string.IsNullOrEmpty(m.CurrentProcessUserID) ? "" : MRP.BussinessLogic.CommonRight.Ataw_UsersGet(m.CurrentProcessUserID).NickName);
            }
        }
        protected override Expression<Func<WF_WORKFLOW_INST, bool>> InstWhereExpress
        {
            get
            {
                return (m =>
                     m.WI_STEP_TYPE == (int)StepType.Manual
                    && m.WI_CREATE_USER == UserId
                    );
            }
        }

        protected override Expression<Func<WorkflowInstView, BaseTree>> Selector
        {
            get
            {
                return (
                    workFlowInst => new BaseTree()
                    {
                        ID = workFlowInst.WF_WORKFLOW_INST.WI_ID,
                        Name = workFlowInst.WF_WORKFLOW_INST.WI_NAME,
                        CreateTime = workFlowInst.WF_WORKFLOW_INST.WI_CREATE_DATE,
                        CurrentStep = workFlowInst.WF_WORKFLOW_INST.WI_CURRENT_STEP_NAME,
                        EndTime = workFlowInst.WF_WORKFLOW_INST.WI_END_DATE,
                        ReceiveTime = workFlowInst.WF_WORKFLOW_INST.WI_RECEIVE_DATE,
                        _parentId = workFlowInst.WF_WORKFLOW_INST.WI_WD_NAME,
                        LastName = workFlowInst.WF_WORKFLOW_INST.WI_LAST_STEP_NAME,
                        WDName = workFlowInst.WF_WORKFLOW_DEF.WD_NAME,
                        LastManualName = workFlowInst.WF_WORKFLOW_INST.WI_LAST_MANUAL_NAME,
                        CurrentProcessUserID = workFlowInst.WF_WORKFLOW_INST.WI_PROCESS_ID,
                    }
                    );
            }
        }
    }
}