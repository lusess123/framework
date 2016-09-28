
using System;
using System.Linq;
using Ataw.Framework.Core;
using Ataw.Framework.Web;
using Ataw.Workflow.Core.DataAccess;
namespace Ataw.Workflow.Core
{
    public sealed class Workflow : IDisposable
    {
        //private readonly IUnitOfData fSource;
        // public Type 

        //public 

        public WorkflowConfig Config { get; private set; }

        public Step CurrentStep { get; private set; }

        public State CurrentState { get; private set; }

        public WF_WORKFLOW_INST WorkflowRow { get; set; }

        public bool IsFinish { get; set; }

        public string WorkflowId { get; private set; }

        public IUnitOfData Source
        {
            get;
            set;
        }

        public bool IsManualStep
        {
            get
            {
                if (CurrentStep != null)
                    return CurrentStep.Config.StepType == StepType.Manual;
                return false;
            }
        }

        private Workflow(IUnitOfData workflowSource)
        {
            Source = workflowSource;
        }
        private Workflow(string workflowId, IUnitOfData workflowSource)
            : this(workflowSource)
        {
            //Source = workflowSource;
            WorkflowDbContext dbContext = workflowSource as WorkflowDbContext;
            WF_WORKFLOW_INST row = dbContext.WF_WORKFLOW_INST.FirstOrDefault(a => a.WI_ID == workflowId);
            AtawDebug.AssertNotNull(row, "该流程已处理或异常,请进历史页面查看", this);
            WorkflowId = row.WI_ID;
            WorkflowRow = row;
            Config = WorkflowConfig.GetByName(row.WI_WD_NAME, workflowSource);
            //WorkflowConfig.ConnString = 
            StepConfig stepConfig = Config.Steps[row.WI_CURRENT_STEP];
            if (stepConfig != null)
            {
                CurrentStep = stepConfig.CreateStep(this);
                CurrentState = CurrentStep.GetState(row.WI_STATUS.Value<StepState>());
            }
            else
            {
                throw new AtawException("不存在的步骤" + row.WI_CURRENT_STEP, this);
            }
        }

        public Workflow(IUnitOfData source, string name, RegNameList<KeyValueItem> parameter, string createUser,
           int? parentId)
            : this(source)
        {
            //  Source = source;
            WorkflowDbContext dbContext = source as WorkflowDbContext;
            Config = WorkflowConfig.GetByName(name, source);
            WorkflowRow = new WF_WORKFLOW_INST();
            //WorkflowRow.BeginEdit();
            WorkflowRow.WI_ID = dbContext.GetUniId();
            //WorkflowRow["WI_ID"] = id;
            WorkflowId = WorkflowRow.WI_ID;
            WorkflowRow.WI_WD_NAME = name;
            WorkflowRow.WI_CREATE_USER = createUser;
            // WorkflowRow["WI_RETRIEVABLE"] = Config.Retrievable;
            ////初始化参与人列表
            WorkflowRow.WI_REF_LIST = QuoteIdList.GetQuoteId(createUser);
            WorkflowRow.WI_CREATE_DATE = dbContext.Now;

            BeginStep step = Config.Steps.BeginStep.CreateStep(this) as BeginStep;
            step.Parameter = parameter;
            CurrentStep = step;
            CurrentState = BeginNRState.Instance;
        }



        public void Run()
        {
            bool continueRun;
            do
            {
                try
                {
                    if (!IsFinish)
                    {
                        continueRun = CurrentState.Execute(this);
                    }
                    else
                    {
                        continueRun = false;
                    }
                }
                catch (WorkflowException ex)
                {
                    SaveError(ex);
                    continueRun = SetWorkflowException(ex);
                    throw new AtawException("工作流异常", ex, this);
                }
                catch (Exception ex)
                {
                 string   mesg = string.Format(ObjectUtil.SysCulture,
                "程序出现异常,异常信息是:{0}{1}{0}堆栈信息是：{0}{2}",
                  Environment.NewLine, ex.Message, ex.StackTrace);
                 AtawTrace.WriteFile(LogType.WorkFlowError, mesg); ;
                    // throw ex;

                    //SaveError(ex);
                    continueRun = false;
                    IsFinish = true;
                    StepUtil.ErrorAbort(Source, WorkflowRow);
                    //StepUtil.ErrorAbort(this, FinishType.Error);
                    //  throw new AtawException("工作流异常", ex, this);
                    throw ex;
                }
            } while (continueRun);
        }
        private void SaveError(Exception ex)
        {
            string mesg = string.Format(ObjectUtil.SysCulture,
               "程序出现异常,异常信息是:{0}{1}{0}堆栈信息是：{0}{2}",
                 Environment.NewLine, ex.Message, ex.StackTrace);
            AtawTrace.WriteFile(LogType.WorkFlowError, mesg); ;
            //int errorId;
            //WorkflowException wfException = ex as WorkflowException;
            //if (wfException == null || wfException != null && wfException.InnerException != null)
            //    errorId = GetSaveError(ex);
            //else
            //    errorId = 0;
            //WorkflowRow.BeginEdit();
            //SetWorkflowRowByState();
            //if (errorId > 0)
            //{
            //    WorkflowRow["WI_WE_ID"] = errorId;
            //}
            //WorkflowRow.EndEdit();
        }
        private bool SetWorkflowException(WorkflowException wfException)
        {
            bool continueRun;
            switch (wfException.ErrorConfig.ProcessType)
            {
                //case ErrorProcessType.Abort:
                //    //WorkflowResolver.ta
                //    StepUtil.ErrorAbort(Source, WorkflowRow);
                //    continueRun = false;
                //    IsFinish = true;
                //    break;
                //case ErrorProcessType.Retry:
                //    if (WorkflowRow.WI_RETRY_TIMES.Value<int>() < wfException.ErrorConfig.RetryTimes)
                //    {
                //        //保存重试信息
                //        WorkflowInstUtil.SaveError(WorkflowRow, wfException);
                //        UpdateState(CurrentStep.GetState(StepState.Mistake));

                //        Source.Submit();
                //        continueRun = false;
                //    }
                //    else
                //    {
                //        CurrentStep.ClearDataSet();
                //        StepUtil.OverTryTimesAbort(Source, WorkflowRow);
                //        continueRun = false;
                //        IsFinish = true;
                //    }

                //    break;
                default:
                    continueRun = false;
                    break;
            }
            return continueRun;
        }
        public void UpdateState(State state)
        {
            AtawDebug.AssertArgumentNull(state, "state", this);

            CurrentState = state;
        }

        public void UpdateStep(Step step)
        {
            AtawDebug.AssertArgumentNull(step, "step", this);

            //if (CurrentStep != null)
            //    CurrentStep.Dispose();
            CurrentStep = step;
        }

        //移交
        public void Transfer(string toUserID, string note)
        {
            if (string.IsNullOrEmpty(toUserID)) return;
            var dbContent = Source as WorkflowDbContext;
            WorkflowContent workflowContent = WorkflowInstUtil.CreateContent(WorkflowRow);
            workflowContent.SetMainRowCreateID(Source, toUserID);
            WF_WORKFLOW_TRANSFER workflowTransfer = new WF_WORKFLOW_TRANSFER();
            workflowTransfer.WT_WI_ID = WorkflowRow.WI_ID;
            workflowTransfer.WT_WD_NAME = WorkflowRow.WI_WD_NAME;
            workflowTransfer.WT_FROM_USER = WorkflowRow.WI_CREATE_USER;
            workflowTransfer.WT_TITLE = "工作移交:" + WorkflowRow.WI_NAME;
            workflowTransfer.WT_NOTE = note;
            workflowTransfer.WT_TO_USER = toUserID;
            workflowTransfer.WT_CREATE_DATE = dbContent.Now;
            workflowTransfer.WT_CREATE_ID = GlobalVariable.UserId.ToString();
            workflowTransfer.WT_ID = dbContent.GetUniId();
            workflowTransfer.WT_IS_APPLY_WF = 1;
            workflowTransfer.WT_STEP_NAME = WorkflowRow.WI_CURRENT_STEP;
            dbContent.WF_WORKFLOW_TRANSFER.Add(workflowTransfer);

            WorkflowRow.WI_CREATE_USER = toUserID;
            dbContent.Submit();
        }
        public void Abort()
        {
            StepUtil.Abort(Source, WorkflowRow);
        }
        #region create
        //public static Workflow CreateWorkflow(IUnitOfData context, string name,
        //    RegNameList<KeyValueItem>  parameter, string createUser)
        //{
        //    return new Workflow(context, name, parameter, createUser);
        //}
        public static Workflow CreateWorkflow(IUnitOfData context, string name,
           RegNameList<KeyValueItem> parameter, string createUser, int? parentId)
        {
            return new Workflow(context, name, parameter, createUser, parentId);
        }

        public static Workflow CreateWorkflow(IUnitOfData source, string workflowId)
        {
            return new Workflow(workflowId, source);
        }

        #endregion

        public void Dispose()
        {
            ObjectUtil.DisposeObject(Source);
            GC.SuppressFinalize(this);
        }


        public bool IsUserStep(string userId)
        {
            Run();
            if (IsManualStep && !IsFinish)
            {
                ManualStep step = CurrentStep as ManualStep;
                return step.ProcessManualWorkflow(WorkflowRow, userId);
            }
            else
            {
 
            }
            return false;
        }
    }
}
