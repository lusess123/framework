using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;
using System;

namespace Ataw.Workflow.Core
{
    public class ManualStep : Step
    {
        public ManualStep(Workflow workflow, StepConfig config)
            : base(workflow, config)
        {
        }

        protected override bool Execute()
        {
            return false;
        }

        //private void SaveWorkflowInst()
        //{
        //    WorkflowResolver.SetCommands(AdapterCommand.Update);
        //    UpdateUtil.UpdateTableResolvers(Source.Context, null, WorkflowResolver);
        //}

        //public bool IsTimeout
        //{
        //    get
        //    {
        //        ManualStepConfig manualConfig = Config as ManualStepConfig;
        //        bool isTimeout = TimeLimitConfig.IsTimeOut(manualConfig.TimeLimiter,
        //            Source.Context, WorkflowRow, "WI_CURRENT_CREATE_DATE");
        //        if (!isTimeout)
        //            isTimeout = TimeLimitConfig.IsTimeOut(Workflow.Config.TimeLimiter,
        //                Source.Context, WorkflowRow, "WI_CREATE_DATE");
        //        return isTimeout;
        //    }
        //}

        protected override void Send(StepConfig nextStep)
        {
            //if (IsTimeout)
            //    WorkflowRow["WI_IS_TIMEOUT"] = true;
            StepUtil.SendStep(Workflow, nextStep, Source);
        }

        public override State GetState(StepState state)
        {
            switch (state)
            {
                case StepState.NotReceive:
                    return ManualNRState.Instance;
                case StepState.ReceiveNotOpen:
                    return ManualRNOState.Instance;
                case StepState.OpenNotProcess:
                    return ManualONPState.Instance;
                case StepState.ProcessNotSend:
                    return ManualPNSState.Instance;
                case StepState.Mistake:
                    return ManualMState.Instance;
                default:
                    AtawDebug.ThrowImpossibleCode(this);
                    return null;
            }
        }

        public bool ProcessManualWorkflow(WF_WORKFLOW_INST workflowRow, string userId)
        {
            WorkflowDbContext dbContext = Source as WorkflowDbContext;
            StepState state = (StepState)workflowRow.WI_STATUS;
            switch (state)
            {
                case StepState.NotReceive:
                    // NR:
                    // 检查userId是否在接受人列表中，是接收并打开，返回true，否则返回false
                    //如果接受人有多个，你只是其中的一个，并且你是上个流程的处理者，不能接收 这一点可以根据配置来.....
                    string receiveIds = workflowRow.WI_RECEIVE_LIST;
                    QuoteIdList ulReceive = QuoteIdList.LoadFromString(receiveIds);
                    if (ulReceive.Contains(userId))
                    {
                        bool _isAppAutoStep = "WrokflowAutoStep".AppKv<bool>(false);
                        string _ManualAskUser = AtawAppContext.Current.GetItem("ManualAskUser").Value<string>();
                        if (ulReceive.Count > 0 && (_ManualAskUser == userId) && !_isAppAutoStep)
                        {
                            //.............
                            return false;
                        }
                        else
                        {
                            workflowRow.WI_RECEIVE_ID = userId;
                            workflowRow.WI_STATUS = (int)StepState.OpenNotProcess;
                            workflowRow.WI_RECEIVE_DATE = dbContext.Now;
                            Source.Submit();
                        }
                        return true;
                    }
                    else
                        return false;
                case StepState.ReceiveNotOpen:
                    // RNO:
                    // 检查接收人是否是userId，是打开，返回true，否则返回false
                    if (userId == workflowRow.WI_RECEIVE_ID)
                    {
                        workflowRow.WI_STATUS = (int)StepState.OpenNotProcess;
                        Source.Submit();
                        return true;
                    }
                    else
                        return false;

                case StepState.OpenNotProcess:
                    // ONP：
                    // 检查接收人是否是userId，是返回true，否则返回false
                    return userId == workflowRow.WI_RECEIVE_ID;
                default:
                    AtawDebug.ThrowImpossibleCode(this);
                    break;
            }

            return false;
        }

        //public void Back(string userId)
        //{
        //    WorkflowContent content = WorkflowInstResolver.CreateContent(WorkflowRow);
        //    using (content)
        //    {
        //        Back(userId, content, null, null);
        //    }
        //}

        public void Unlock()
        {
            ManualStepConfig manualConfig = Config as ManualStepConfig;
            // manualConfig.
            if (manualConfig.HaveUnlock)
            {
                var row = Workflow.WorkflowRow;
                StepState stepState = row.WI_STATUS.Value<StepState>();
                if (stepState == StepState.OpenNotProcess)
                {
                    //row.BeginEdit();
                    row.WI_STATUS = (int)StepState.NotReceive;
                    row.WI_RECEIVE_DATE = null;
                    row.WI_RECEIVE_ID = "";
                    row.WI_CURRENT_CREATE_DATE = DateTime.Now;
                    Workflow.Source.Submit();
                    // row.EndEdit();
                    //   WorkflowResolver.SetCommands(AdapterCommand.Update);
                    // UpdateUtil.UpdateTableResolvers(null, WorkflowResolver);
                }
                else
                {
                    throw new AtawException("步骤{0} 的状态不是未接收 无法解锁".AkFormat(manualConfig.DisplayName), this);
                }
            }
            else
            {
                throw new AtawException("步骤{0} 配置 禁止解锁".AkFormat(manualConfig.DisplayName),this);
            }
        }

        public bool Back(string backStepName)
        {
            ManualStepConfig manualConfig = Config as ManualStepConfig;
           // var backStepName = string.Empty;
            if (manualConfig.HaveBack)
            {
                if (backStepName.IsAkEmpty())
                {
                    // 否则退到上一人工步骤，如果没有则报错
                    backStepName = WorkflowRow.WI_LAST_MANUAL;
                }
                AtawDebug.AssertNotNullOrEmpty(backStepName, string.Format(ObjectUtil.SysCulture,
                    "人工步骤{0}没有可以回退的上一个人工步骤", manualConfig.Name), this);
            }

            StepConfig backStep = Config.Parent.Steps[backStepName];
            AtawDebug.AssertArgumentNull(backStep, string.Format(ObjectUtil.SysCulture,
                   "指定的人工步骤{0} 不存在", backStepName), this);
            return StepUtil.BackStep(Workflow, backStep);
        }
    }
}
