using System;
using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;
using Ataw.Framework.Web;
using System.Collections.Generic;
using System.Linq;
namespace Ataw.Workflow.Core
{
    public static class StepUtil
    {

        //public static void SetWorkflowByStep(IWorkflowSource source, WF_WORKFLOW_INST workflowRow,
        //   StepConfig nextStep)
        //{
        //    workflowRow.WI_LAST_STEP = workflowRow.WI_CURRENT_STEP;
        //    workflowRow.WI_LAST_STEP_NAME = workflowRow.WI_CURRENT_STEP_NAME;

        //    WorkflowInstResolver.SetWorkflowByStep(workflowRow, nextStep);
        //    nextStep.Prepare(workflowRow, source);
        //}

        public static void SetWorkflowByStep(WF_WORKFLOW_INST row, string currName, string currDisplayName,
            DateTime createDateTime, int stepType, int stepStatus)
        {
            row.WI_CURRENT_STEP = currName;
            row.WI_CURRENT_STEP_NAME = currDisplayName;
            row.WI_CURRENT_CREATE_DATE = createDateTime;
            row.WI_STEP_TYPE = stepType;
            row.WI_STATUS = stepStatus;
        }


        public static void SendStep(Workflow workflow, StepConfig nextStep, IUnitOfData source)
        {
            DateTime now = DateTime.Now;
            //  = AtawIocContext.Current.FetchInstance<IUnitOfData>();
            WorkflowDbContext dbContext = source as WorkflowDbContext;
            WF_WORKFLOW_INST workflowRow = workflow.WorkflowRow;
            WorkflowContent content = WorkflowInstUtil.CreateContent(workflowRow);
            using (dbContext)
            {
                //拷贝新步骤
                WF_STEP_INST stepRow = new WF_STEP_INST();
                (source as WorkflowDbContext).WF_STEP_INST.Add(stepRow);
                CopyWorkflowToStep(dbContext, workflowRow, stepRow, FlowAction.Flow);
                //人工步骤的处理
                bool isManual = workflow.CurrentStep.Config.StepType == StepType.Manual;
                if (isManual)
                {
                    CopyManualInfo(workflow.WorkflowRow, stepRow);
                }
                //更新工作流实例
                workflowRow.WI_INDEX = workflowRow.WI_INDEX.Value<int>() + 1;
                if (isManual)
                {
                    workflowRow.WI_LAST_MANUAL = workflowRow.WI_CURRENT_STEP;
                    workflowRow.WI_LAST_MANUAL_NAME = workflowRow.WI_CURRENT_STEP_NAME;
                    //更新参与人列表
                    string refIds = workflowRow.WI_REF_LIST;
                    QuoteIdList ulRef = QuoteIdList.LoadFromString(refIds);
                    string receiveId = workflowRow.WI_RECEIVE_ID;
                    string sendId = workflowRow.WI_SEND_ID;
                    string processId = workflowRow.WI_PROCESS_ID;
                    ulRef.Add(receiveId);
                    ulRef.Add(sendId);
                    ulRef.Add(processId);
                    int outInt;
                    workflowRow.WI_REF_LIST = ulRef.ToString(out outInt);
                    //接收人  处理人  重新置为空
                    workflowRow.WI_RECEIVE_ID = "";
                    workflowRow.WI_LAST_PROCESS_ID = workflowRow.WI_PROCESS_ID;
                    workflowRow.WI_PROCESS_ID = "";
                    workflowRow.WI_RECEIVE_LIST = "";
                    //清空超时和提醒标识
                    if (workflowRow.WI_IS_TIMEOUT.Value<bool>() == true)
                    {
                        workflowRow.WI_IS_TIMEOUT = false;
                    }
                    //清空错误处理信息 WI_ERROR_TYPE WI_MAX_RETRY_TIMES WI_RETRY_TIMES  WI_NEXT_EXE_DATE
                    WorkflowInstUtil.ClearError(workflowRow);
               }
                //更新主表信息
                SetWorkflowByStep(source, workflowRow, nextStep);
                content.SetMainRowStatus(source, nextStep);
                dbContext.Submit();
            }
            // IUnitOfData dbContext = AtawIocContext.Current.FetchInstance<IUnitOfData>();
            //ATAW_WORKFLOWContext context = dbContext as ATAW_WORKFLOWContext;
            //拷贝新步骤
            //WF_STEP_INST stepRow = new WF_STEP_INST();



        }

        //public static void FinishStep(Workflow workflow, WorkflowContent content, EndStepConfig config,
        //  IUnitOfData source, BaseProcessor processor)
        //{
        //    FinishType finishType = FinishType.Normal;
        //    if (config.EnableModify)
        //    {
        //        finishType = FinishType.ModifiedNormal;
        //    }
        //    FinishData result = FinishStep(workflow, finishType,
        //        workflow.WorkflowRow["WI_PROCESS_ID"], content);

        //    source.Submit();
        //}


        public static void SetWorkflowByStep(IUnitOfData source, WF_WORKFLOW_INST workflowRow,
            StepConfig nextStep)
        {
            workflowRow.WI_LAST_STEP = workflowRow.WI_CURRENT_STEP;
            workflowRow.WI_LAST_STEP_NAME = workflowRow.WI_CURRENT_STEP_NAME;

            WorkflowInstUtil.SetWorkflowByStep(workflowRow, nextStep);
            nextStep.Prepare(workflowRow, source);
        }


        public static void CopyWorkflowToStep(WorkflowDbContext wfContext, WF_WORKFLOW_INST workflowRow,
           WF_STEP_INST stepRow, FlowAction flowAction)
        {
            // stepRow.SI_INDEX = workflowRow.WI_INDEX;
            stepRow.SI_INDEX = workflowRow.WI_INDEX;
            //步骤标识 0 -- 有效初始值  1 -- 无效的
            stepRow.SI_VALID_FLAG = 0;
            stepRow.SI_VALID_FLAG = 0;
            //流转方式
            stepRow.SI_FLOW_TYPE = (int?)flowAction;
            ////步骤基本信息
            stepRow.SI_ID = wfContext.GetUniId();
            stepRow.SI_WI_ID = workflowRow.WI_ID;
            ////上一步骤信息
            stepRow.SI_LAST_STEP = workflowRow.WI_LAST_STEP;
            stepRow.SI_LAST_STEP_NAME = workflowRow.WI_LAST_STEP_NAME;
            stepRow.SI_LAST_MANUAL = workflowRow.WI_LAST_MANUAL;
            stepRow.SI_LAST_MANUAL_NAME = workflowRow.WI_LAST_MANUAL_NAME;
            ////当前步骤
            stepRow.SI_CURRENT_STEP = workflowRow.WI_CURRENT_STEP;
            stepRow.SI_CURRENT_STEP_NAME = workflowRow.WI_CURRENT_STEP_NAME;
            stepRow.SI_STEP_TYPE = workflowRow.WI_STEP_TYPE;
            stepRow.SI_PRIORITY = workflowRow.WI_PRIORITY;
            stepRow.SI_STATUS = workflowRow.WI_STATUS ?? 0;
            ////当前步骤 时间信息：开始时间 结束时间 步骤用时
            stepRow.SI_START_DATE = workflowRow.WI_CURRENT_CREATE_DATE;
            stepRow.SI_END_DATE = wfContext.Now;
            stepRow.SI_TIME_SPAN = (double)((wfContext.Now - stepRow.SI_START_DATE.Value<DateTime>()).Ticks)
              / TimeSpan.TicksPerDay;
            ////发送时间
            stepRow.SI_SEND_DATE = workflowRow.WI_SEND_DATE;
        }

        public static void CopyWorkflowToStepHis(WorkflowDbContext wfContext, WF_WORKFLOW_INST workflowRow,
        WF_STEP_INST_HIS stepRow, FlowAction flowAction)
        {
            // stepRow.SI_INDEX = workflowRow.WI_INDEX;
            stepRow.SI_INDEX = workflowRow.WI_INDEX;
            //步骤标识 0 -- 有效初始值  1 -- 无效的
            stepRow.SI_VALID_FLAG = 0;
            stepRow.SI_VALID_FLAG = 0;
            //流转方式
            stepRow.SI_FLOW_TYPE = (int?)flowAction;
            ////步骤基本信息
            stepRow.SI_ID = wfContext.GetUniId();
            stepRow.SI_WI_ID = workflowRow.WI_ID;
            ////上一步骤信息
            stepRow.SI_LAST_STEP = workflowRow.WI_LAST_STEP;
            stepRow.SI_LAST_STEP_NAME = workflowRow.WI_LAST_STEP_NAME;
            stepRow.SI_LAST_MANUAL = workflowRow.WI_LAST_MANUAL;
            stepRow.SI_LAST_MANUAL_NAME = workflowRow.WI_LAST_MANUAL_NAME;
            ////当前步骤
            stepRow.SI_CURRENT_STEP = workflowRow.WI_CURRENT_STEP;
            stepRow.SI_CURRENT_STEP_NAME = workflowRow.WI_CURRENT_STEP_NAME;
            stepRow.SI_STEP_TYPE = workflowRow.WI_STEP_TYPE;
            stepRow.SI_PRIORITY = workflowRow.WI_PRIORITY;
            stepRow.SI_STATUS = workflowRow.WI_STATUS ?? 0;
            ////当前步骤 时间信息：开始时间 结束时间 步骤用时
            stepRow.SI_START_DATE = workflowRow.WI_CURRENT_CREATE_DATE;
            stepRow.SI_END_DATE = wfContext.Now;
            stepRow.SI_TIME_SPAN = (double)((wfContext.Now - stepRow.SI_START_DATE.Value<DateTime>()).Ticks)
              / TimeSpan.TicksPerDay;
            ////发送时间
            stepRow.SI_SEND_DATE = workflowRow.WI_SEND_DATE;
        }

        public static void CopyManualInfo(WF_WORKFLOW_INST workflowRow, WF_STEP_INST stepRow)
        {
            //当前步骤是否超时 超时时间 (超时信息)
            stepRow.SI_IS_TIMEOUT = workflowRow.WI_IS_TIMEOUT;
            stepRow.SI_TIMEOUT_DATE = workflowRow.WI_TIMEOUT_DATE;
            //接收 发送 处理人和时间（操作信息）
            stepRow.SI_RECEIVE_ID = workflowRow.WI_RECEIVE_ID;
            stepRow.SI_RECEIVE_DATE = workflowRow.WI_RECEIVE_DATE;

            stepRow.SI_SEND_ID = workflowRow.WI_SEND_ID;
            stepRow.SI_SEND_DATE = workflowRow.WI_SEND_DATE;

            stepRow.SI_PROCESS_ID = workflowRow.WI_PROCESS_ID;
            stepRow.SI_PROCESS_DATE = workflowRow.WI_PROCESS_DATE;
        }
        public static void ErrorAbort(IUnitOfData source, WF_WORKFLOW_INST WorkflowRow)
        {
            Abort(source, WorkflowRow, FinishType.Error);
        }
        public static void OverTryTimesAbort(IUnitOfData source, WF_WORKFLOW_INST WorkflowRow)
        {
            Abort(source, WorkflowRow, FinishType.OverTryTimes);
        }
        public static void Abort(IUnitOfData source, WF_WORKFLOW_INST WorkflowRow, FinishType finishType = FinishType.Abort)
        {
            WorkflowDbContext dbContext = source as WorkflowDbContext;
            WorkflowContent content = WorkflowInstUtil.CreateContent(WorkflowRow);
            WF_WORKFLOW_INST_HIS wfi_His = new WF_WORKFLOW_INST_HIS();
            //copy
            wfi_His.WI_ID = WorkflowRow.WI_ID;
            wfi_His.WI_WD_NAME = WorkflowRow.WI_WD_NAME;
            wfi_His.WI_CONTENT_XML = WorkflowRow.WI_CONTENT_XML;
            wfi_His.WI_NAME = WorkflowRow.WI_NAME;
            wfi_His.WI_CREATE_DATE = WorkflowRow.WI_CREATE_DATE;
            wfi_His.WI_CREATE_USER = WorkflowRow.WI_CREATE_USER;
            wfi_His.WI_REF_LIST = WorkflowRow.WI_REF_LIST;
            wfi_His.WI_IS_TIMEOUT = WorkflowRow.WI_IS_TIMEOUT;
            wfi_His.WI_TIMEOUT_DATE = WorkflowRow.WI_TIMEOUT_DATE;
            wfi_His.WI_NEXT_EXE_DATE = WorkflowRow.WI_NEXT_EXE_DATE;
            wfi_His.WI_PRIORITY = WorkflowRow.WI_PRIORITY;
            wfi_His.WI_STATUS = WorkflowRow.WI_STATUS;
            wfi_His.WI_ERROR_TYPE = WorkflowRow.WI_ERROR_TYPE;
            wfi_His.WI_RETRY_TIMES = WorkflowRow.WI_RETRY_TIMES;
            wfi_His.WI_MAX_RETRY_TIMES = WorkflowRow.WI_MAX_RETRY_TIMES;
            wfi_His.WI_WE_ID = WorkflowRow.WI_WE_ID;
            wfi_His.WI_PARENT_ID = WorkflowRow.WI_PARENT_ID;
            wfi_His.WI_PC_FLAG = WorkflowRow.WI_PC_FLAG;
            //赋值
            wfi_His.WI_END_DATE = dbContext.Now;
            wfi_His.WI_END_STATE = (int)finishType;
            if (!string.IsNullOrEmpty(WorkflowRow.WI_PROCESS_ID))
                wfi_His.WI_END_USER = WorkflowRow.WI_PROCESS_ID;
            else
                wfi_His.WI_END_USER = GlobalVariable.UserId.ToString();
            //附加
            dbContext.WF_WORKFLOW_INST_HIS.Add(wfi_His);
            ////copy步骤历史
            string wiId = WorkflowRow.WI_ID;
            List<WF_STEP_INST> stepList = dbContext.WF_STEP_INST.Where(a => a.SI_WI_ID == wiId).ToList();
            List<WF_STEP_INST_HIS> stepListHis = new List<WF_STEP_INST_HIS>();
            foreach (var step in stepList)
            {
                //------------
                WF_STEP_INST_HIS his = new WF_STEP_INST_HIS();
                his.SI_WI_ID = step.SI_WI_ID;
                his.SI_CURRENT_STEP = step.SI_CURRENT_STEP;
                his.SI_CURRENT_STEP_NAME = step.SI_CURRENT_STEP_NAME;
                his.SI_END_DATE = step.SI_END_DATE;

                his.SI_FLOW_TYPE = step.SI_FLOW_TYPE;
                his.SI_ID = step.SI_ID;
                his.SI_INDEX = step.SI_INDEX;
                his.SI_IS_TIMEOUT = step.SI_IS_TIMEOUT;
                his.SI_LAST_MANUAL = step.SI_LAST_MANUAL;
                his.SI_LAST_MANUAL_NAME = step.SI_LAST_MANUAL_NAME;
                his.SI_LAST_STEP = step.SI_LAST_STEP;
                his.SI_LAST_STEP_NAME = step.SI_LAST_STEP_NAME;
                his.SI_NOTE = step.SI_NOTE;
                his.SI_PRIORITY = step.SI_PRIORITY;
                his.SI_PROCESS_DATE = step.SI_PROCESS_DATE;
                his.SI_PROCESS_ID = step.SI_PROCESS_ID;
                his.SI_RECEIVE_DATE = step.SI_RECEIVE_DATE;
                his.SI_RECEIVE_ID = step.SI_RECEIVE_ID;
                his.SI_SEND_DATE = step.SI_SEND_DATE;
                his.SI_SEND_ID = step.SI_SEND_ID;
                his.SI_START_DATE = step.SI_START_DATE;
                his.SI_STATUS = step.SI_STATUS;
                his.SI_STEP_TYPE = step.SI_STEP_TYPE;
                his.SI_TIME_SPAN = step.SI_TIME_SPAN;
                his.SI_TIMEOUT_DATE = step.SI_TIMEOUT_DATE;

                dbContext.WF_STEP_INST.Remove(step);
                dbContext.WF_STEP_INST_HIS.Add(his);
            }
            //新增最后一个步骤到历史
            WF_STEP_INST_HIS his_step = new WF_STEP_INST_HIS();
            // dbContext.WF_STEP_INST_HIS.Add(his_step);
            StepUtil.CopyWorkflowToStepHis(dbContext, WorkflowRow, his_step, FlowAction.Flow);
            dbContext.WF_STEP_INST_HIS.Add(his_step);
            ////更新主表信息
            //删除视力表

            content.EndMainRowStatus(dbContext, finishType);
            ////主表转移先不做
            ////--提交
            dbContext.WF_WORKFLOW_INST.Remove(WorkflowRow);
            dbContext.Submit();
        }

        public static bool BackStep(Workflow workflow, StepConfig backStep)
        {
            var workflowRow = workflow.WorkflowRow;
            var source = workflow.Source as WorkflowDbContext;

            var stepInst = GetBackStep(workflow, backStep);

            var newStepInst = new WF_STEP_INST();
            source.WF_STEP_INST.Add(newStepInst);
            CopyWorkflowToStep(source, workflow.WorkflowRow, newStepInst, FlowAction.Back);
            CopyManualInfo(workflow.WorkflowRow, newStepInst);
            //回退步骤 修改接收人列表 和 接收人个数
            //从步骤实例表中查询上次接收的人


            workflowRow.WI_INDEX = stepInst.SI_INDEX;
            workflowRow.WI_RECEIVE_ID = stepInst.SI_RECEIVE_ID;//上次接收的人
            workflowRow.WI_RECEIVE_LIST = QuoteIdList.GetQuoteId(stepInst.SI_RECEIVE_ID);
            workflowRow.WI_RECEIVE_COUNT = 1;
            workflowRow.WI_SEND_ID = GlobalVariable.UserId.ToString();
            workflowRow.WI_SEND_DATE = source.Now;

            workflowRow.WI_LAST_MANUAL = stepInst.SI_LAST_MANUAL;
            workflowRow.WI_LAST_MANUAL_NAME = stepInst.SI_LAST_MANUAL_NAME;
            workflowRow.WI_LAST_STEP = stepInst.SI_LAST_STEP;
            workflowRow.WI_LAST_STEP_NAME = stepInst.SI_LAST_STEP_NAME;
            workflowRow.WI_CURRENT_CREATE_DATE = DateTime.Now;
            // stepInst.SI_VALID_FLAG = 1;
            WorkflowInstUtil.ClearError(workflowRow);
            WorkflowInstUtil.SetWorkflowByStep(workflowRow, backStep);
            WorkflowContent workflowContent = WorkflowInstUtil.CreateContent(workflow.WorkflowRow);
            workflowContent.SetMainRowStatus(source, backStep);
            backStep.Prepare(workflowRow, source);
            source.Submit();
            return true;
        }
        private static WF_STEP_INST GetBackStep(Workflow workflow, StepConfig nextStep)
        {
            var context = workflow.Source as WorkflowDbContext;
            return context.WF_STEP_INST.Where(m => m.SI_WI_ID == workflow.WorkflowRow.WI_ID).OrderBy(m => m.SI_INDEX).FirstOrDefault(m => m.SI_CURRENT_STEP == nextStep.Name);
        }
    }
}
