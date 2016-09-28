using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Ataw.Framework.Core;
using Ataw.Framework.Web;
using Ataw.Workflow.Core.DataAccess;

namespace Ataw.Workflow.Core
{
    [CodePlug(REG_NAME, BaseClass = typeof(INotifyAction)
     , Tags = new PlugInTag[] { PlugInTag.Code, PlugInTag.Workflow },
  CreateDate = "2012-11-15", Author = "ace", Description = "发送消息")]
    public class NotifyAction : INotifyAction
    {
        private const string REG_NAME = "NotifyAction";
      //  private IMessagesBuilder messagesBuilder = GlobalVariable.AppContext.AtawMessagesBuilder.Value;
        private string BaseUrl = "/WorkFlow/MyWork/ProcessDetail?id=";
        public string Title = "流程处理";
        //public abstract string SourceModuleID { get; }
        public void DoAction(string userId, WF_WORKFLOW_INST mainRow, WorkflowContent content, IUnitOfData source)
        {
            IMessagesBuilder messagesBuilder = GlobalVariable.AppContext.AtawMessagesBuilder.Value;
            //Thread th = new Thread();
            string messageBody = string.Format("{0}-{1}-需要你处理", mainRow.WI_NAME, mainRow.WI_CURRENT_STEP_NAME);
            string url = string.Format("{0}{1}", BaseUrl, mainRow.WI_ID);
            messagesBuilder.InsertMessage(Title, messageBody, url, 0, userId, MessageTypeEnum.WorkFlow, "", GlobalVariable.FControlUnitID, GlobalVariable.UserId.ToString());
        }
        public void DoPush(IEnumerable<string> userIds, NodeRequest nodeRequest, WF_WORKFLOW_INST mainRow, IUnitOfData source)
        {
            if (userIds == null || userIds.Count() == 0) return;
            var workflowSource = source as WorkflowDbContext;
            if (workflowSource == null) return;
            var workflowDefName = workflowSource.WF_WORKFLOW_DEF.FirstOrDefault(m => m.WD_SHORT_NAME == mainRow.WI_WD_NAME).WD_NAME;
            foreach (var userId in userIds)
            {
                var selectsql = string.Format("SELECT NickName FROM Ataw_Users WHERE UserID=@USERID");
                var param = new SqlParameter("@USERID", userId);
                string nickName = source.QueryObject(selectsql, param).ToString();
                SnsActiveService sns = new SnsActiveService();
                sns.SetUnitOfData(source);
                string title = string.Format(ObjectUtil.SysCulture, "待处理流程到达:"
                    + "<a class='ACT-A-HREF' href='$$module/workflow/workflowDef$Detail${0}'>{1}</a>"
                    + "<br/>"
                    + "<a class='ACT-A-HREF' href='$workflow$inst$${2}'>{3}-{4}</a>", Convert.ToBase64String(Encoding.Default.GetBytes("{\"keys\":\"" + mainRow.WI_WD_NAME + "\"}")), workflowDefName, mainRow.WI_ID, mainRow.WI_NAME, mainRow.WI_CURRENT_STEP_NAME);
                sns.InsertActive(new ActiveModel()
                {
                    ItemType = ActivityItemType.MyWork.ToString(),
                    PrivacyStatus = PrivacyType.ToMyself.ToString(),
                    SourceId = mainRow.WI_ID,
                    SubContent = title,
                    UserId = userId,
                    UserName = nickName
                });
            }

            source.ApplyFun.Add((tran) =>
            {
                NodeServerPusher.Notify(userIds, null);
                return 0;
            });
        }
        // private void 





        public void ManualCompletePush(string manualUserId, NodeRequest nodeRequest, WF_WORKFLOW_INST mainRow, IUnitOfData source)
        {
           // throw new NotImplementedException();
        }
    }
}
