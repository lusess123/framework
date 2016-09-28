
using System;
using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace Ataw.Workflow.Core
{
    public static class WorkflowInstUtil
    {
        public static WorkflowContent CreateContent(WF_WORKFLOW_INST row)
        {
            string contentXml = row.WI_CONTENT_XML;
            AtawDebug.AssertNotNullOrEmpty(contentXml, string.Format(ObjectUtil.SysCulture,
                "ID为{0}，模式为{1}的工作流没有设置ContentXml", row.WI_ID, row.WI_WD_NAME), null);

            WorkflowContent content = XmlUtil.ReadFromString<WorkflowContent>(contentXml);
            return content;
        }

        public static void ManualSendWorkflow(WF_WORKFLOW_INST row, object userId, BaseProcessor processor)
        {
            // 处理人，处理时间，状态改为PNS
            row.WI_PROCESS_ID = userId.ToString();

            row.WI_PROCESS_DATE = DateTime.Now;
            row.WI_STATUS = (int)StepState.ProcessNotSend;
            processor.SaveContent(row);
        }

        public static void SetWorkflowByStep(WF_WORKFLOW_INST row, string currName, string currDisplayName,
            DateTime createDateTime, int stepType, int stepStatus)
        {
            row.WI_CURRENT_STEP = currName;
            row.WI_CURRENT_STEP_NAME = currDisplayName;
            row.WI_CURRENT_CREATE_DATE = createDateTime;
            row.WI_STEP_TYPE = stepType;
            row.WI_STATUS = stepStatus;
        }

        public static void SetWorkflowByStep(WF_WORKFLOW_INST row, string currName, string currDisplayName,
            DateTime createDateTime, int stepType, int stepStatus, DateTime sendTime)
        {
            row.WI_SEND_DATE = sendTime;
            SetWorkflowByStep(row, currName, currDisplayName, createDateTime, stepType, stepStatus);
        }

        public static void SetWorkflowByStep(WF_WORKFLOW_INST row, StepConfig stepConfig)
        {
            DateTime now = DateTime.Now;
            SetWorkflowByStep(row, stepConfig.Name, stepConfig.DisplayName, now,
                (int)stepConfig.StepType, (int)StepState.NotReceive, now);
        }

        public static void SetWorkflowByStep(DateTime now, WF_WORKFLOW_INST row, StepConfig stepConfig)
        {
            SetWorkflowByStep(row, stepConfig.Name, stepConfig.DisplayName, now,
                (int)stepConfig.StepType, (int)StepState.NotReceive, now);
        }

        //public static void SetInstInfo(DataRow row, DataRow mainRow, DbContext context, string infoFieldName, string columnName)
        //{
        //    int sharpP = columnName.IndexOf("#", StringComparison.CurrentCulture);
        //    if (sharpP > -1)
        //    {
        //        string fieldName = columnName.Substring(0, sharpP);
        //        string easySearchName = columnName.Substring(sharpP + 1);
        //        EasySearch search = PlugInFactoryManager.CreateInstance<EasySearch>("EasySearch", easySearchName);
        //        string infoValue = search.Decode(context, mainRow[fieldName].ToString());
        //        row[infoFieldName] = infoValue;
        //    }
        //    else
        //    {
        //        row[infoFieldName] = mainRow[columnName];
        //    }
        //}

        public static void SaveError(WF_WORKFLOW_INST workflowRow, WorkflowException wfException)
        {
            ErrorConfig error = wfException.ErrorConfig;
            if (workflowRow.WI_STATUS.Value<StepState>() != StepState.Mistake)
            {
                workflowRow.WI_STATUS = StepState.Mistake.Value<int>();
                workflowRow.WI_ERROR_TYPE = (int)wfException.Reason;
                workflowRow.WI_MAX_RETRY_TIMES = error.RetryTimes;
            }
            workflowRow.WI_RETRY_TIMES = workflowRow.WI_RETRY_TIMES.Value<int>() + 1;
            workflowRow.WI_NEXT_EXE_DATE = DateTime.Now.Add(error.Interval);
        }

        public static void ClearError(WF_WORKFLOW_INST workflowRow)
        {
            //清空错误处理信息 WI_ERROR_TYPE WI_MAX_RETRY_TIMES WI_RETRY_TIMES  WI_NEXT_EXE_DATE
            workflowRow.WI_ERROR_TYPE = null;
            workflowRow.WI_MAX_RETRY_TIMES = null;
            workflowRow.WI_RETRY_TIMES = null;
            workflowRow.WI_NEXT_EXE_DATE = null;
        }
        // public bool IsSelfReceiveManualStep(string id)
        //{
        //    var workflow = Workflow.Core.Workflow.CreateWorkflow(context, id);
        //    var res =  workflow.IsUserStep(UserId);
        //    IsFinish = workflow.IsFinish;
        //    return res;
        //    //if (workflow.CurrentStep.Config.StepType == StepType.Manual && workflow.WorkflowRow.WI_RECEIVE_ID == UserId)
        //    //{
        //    //    return true;
        //    //}
        //    //return false;
        //}
       
    }
}
