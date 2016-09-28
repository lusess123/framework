using System;
using System.Data;
using System.Linq.Expressions;
using Ataw.Framework.Core;
using Ataw.Framework.Web;
using Ataw.Workflow.Core;
using Ataw.Workflow.Core.DataAccess;

namespace Ataw.Workflow.Web.Bussiness
{
    public class MyWork : MyWorkflowINST
    {
        protected override Action<BaseTree> ResultForEach
        {
            get
            {
                return null;
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
                    }
                    );
            }
        }

        protected override Expression<Func<WF_WORKFLOW_INST, bool>> InstWhereExpress
        {
            get
            {
                return
                    (m =>
                      m.WI_STEP_TYPE == (int)StepType.Manual
                      && (
                        m.WI_RECEIVE_ID == UserId
                        ||
                        (m.WI_STATUS == (int)StepState.NotReceive
                        && m.WI_RECEIVE_COUNT > 1
                        && m.WI_RECEIVE_LIST.Contains(UserId))
                         )
                );
            }
        }
        public string WorkflowProcess(string wid, DataSet PostDataSet)
        {
            Workflow.Core.Workflow wf = Workflow.Core.Workflow.CreateWorkflow(context, wid);
            WorkflowConfig.ConnString = PlugAreaRegistration.CONN;
            ManualStepConfig config = wf.CurrentStep.Config as ManualStepConfig;
            AtawDebug.AssertNotNull(config, "调用时机有误，当前的步骤必须是人工步骤，现在不是", this);

            WorkflowContent content = WorkflowInstUtil.CreateContent(wf.WorkflowRow);
            if (!string.IsNullOrEmpty(config.Process.UIOperation.RegName))
            {
                UIProcessor processor = AtawIocContext.Current.FetchInstance<UIProcessor>(
                    config.Process.UIOperation.PlugIn);
                processor.Config = config;
                processor.Source = context;
                processor.Content = content;

                processor.UIData = PostDataSet;
                processor.Execute(wf.WorkflowRow);
                WorkflowInstUtil.ManualSendWorkflow(wf.WorkflowRow, GlobalVariable.UserId, processor);
            }
            else
            {
                wf.WorkflowRow.WI_PROCESS_ID = GlobalVariable.UserId.ToString();
                wf.WorkflowRow.WI_PROCESS_DATE = context.Now;
                wf.WorkflowRow.WI_STATUS = (int)StepState.ProcessNotSend;
            }
            //context.Submit();
            wf.UpdateState(ManualPNSState.Instance);
            string url = wf.GetWorkflowUrl();
            JsResponseResult<string> res = new JsResponseResult<string>()
            {
                ActionType = JsActionType.Url,
                Content = url
            };
            return res.ToJSON();
        }
    }
}