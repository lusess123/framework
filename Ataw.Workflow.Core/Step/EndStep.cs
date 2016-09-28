using System;
using System.Collections.Generic;
using System.Linq;
using Ataw.Framework.Core;
using Ataw.Framework.Web;
using Ataw.Workflow.Core.DataAccess;
namespace Ataw.Workflow.Core
{
    public class EndStep : Step
    {
        public EndStep(Workflow workflow, StepConfig config)
            : base(workflow, config)
        {
        }

        protected override bool Execute()
        {
            WorkflowDbContext dbContext = Source as WorkflowDbContext;
            EndStepConfig config = Config as EndStepConfig;
            AutoProcessor processor = null;
            WorkflowContent content = WorkflowInstUtil.CreateContent(WorkflowRow);
            if (!string.IsNullOrEmpty(config.PlugRegName))
            {
                try
                {
                    processor = AtawIocContext.Current.FetchInstance<AutoProcessor>(config.PlugRegName);
                    processor.Config = config;
                    processor.Source = Source;
                    processor.Content = content;
                    processor.Execute(WorkflowRow);
                }
                catch (Exception ex)
                {
                    string mesg = string.Format(ObjectUtil.SysCulture,
              "程序出现异常,异常信息是:{0}{1}{0}堆栈信息是：{0}{2}",
                Environment.NewLine, ex.Message, ex.StackTrace);
                    AtawTrace.WriteFile(LogType.WorkFlowError, mesg); ;

                   // AtawTrace.WriteFile();
                    throw new PlugInException(config, config.Error, ex);
                }
            }
            FinishType finishType = FinishType.Normal;
            if (config.EnableModify)
            {
                finishType = FinishType.ModifiedNormal;
            }
            //-----
            ////新增实例历史
            // "WI_ID", 
            //"WI_WD_NAME", "WI_CONTENT_XML", "WI_NAME", "WI_CREATE_DATE", "WI_CREATE_USER",
            //"WI_REF_LIST", "WI_IS_TIMEOUT","WI_TIMEOUT_DATE", "WI_NEXT_EXE_DATE", "WI_PRIORITY" ,
            //"WI_STATUS", "WI_ERROR_TYPE","WI_RETRY_TIMES","WI_MAX_RETRY_TIMES","WI_WE_ID",
            //"WI_PARENT_ID","WI_PC_FLAG"
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

            content.EndMainRowStatus(Source as WorkflowDbContext, finishType);
            ////主表转移先不做
            ////--提交
            dbContext.WF_WORKFLOW_INST.Remove(WorkflowRow);
            Source.Submit();
            return false;
        }

        protected override void Send(StepConfig nextStep)
        {
            AtawDebug.ThrowAtawException(string.Format(ObjectUtil.SysCulture,
                "工作流{1}的结束步骤{0}没有Send操作", Config.Parent.Name, Config.Name), this);
        }

        public override State GetState(StepState state)
        {
            switch (state)
            {
                case StepState.NotReceive:
                    return EndNRState.Instance;
                case StepState.ReceiveNotOpen:
                    return EndRNOState.Instance;
                case StepState.OpenNotProcess:
                    return EndONPState.Instance;
                case StepState.ProcessNotSend:
                    return EndPNSState.Instance;
                case StepState.Mistake:
                    return EndMState.Instance;
                default:
                    AtawDebug.ThrowImpossibleCode(this);
                    return null;
            }
        }
    }
}
