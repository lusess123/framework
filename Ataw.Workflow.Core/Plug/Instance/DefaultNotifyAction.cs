using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Workflow.Core.Plug.Instance
{
      [CodePlug("DefaultNotifyAction", BaseClass = typeof(INotifyAction),
CreateDate = "2015-12-31", Author = "zhengyk", Description = "默认工作流触发接口")]
    public class DefaultNotifyAction : INotifyAction
    {
        public void DoAction(string userId, DataAccess.WF_WORKFLOW_INST mainRow, WorkflowContent content, IUnitOfData source)
        {
           // throw new NotImplementedException();
        }

        public void DoPush(IEnumerable<string> userIds, NodeRequest nodeRequest, DataAccess.WF_WORKFLOW_INST mainRow, IUnitOfData source)
        {
            //throw new NotImplementedException();
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
                    + "<a class='ACT-A-HREF' href='$windefault$module/workflow/workflowDef$Detail${0}'>{1}</a>"
                    + "<br/>"
                    + "<a class='ACT-A-HREF' href='$winworkflow$inst$${2}'>{3}-{4}</a>", Convert.ToBase64String(Encoding.Default.GetBytes("{\"keys\":\"" + mainRow.WI_WD_NAME + "\"}")), workflowDefName, mainRow.WI_ID, mainRow.WI_NAME, mainRow.WI_CURRENT_STEP_NAME);
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

        public void ManualCompletePush(string manualUserId, NodeRequest nodeRequest, DataAccess.WF_WORKFLOW_INST mainRow, IUnitOfData source)
        {
           // throw new NotImplementedException();
        }
    }
}
