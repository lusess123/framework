using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ataw.Workflow.Core;
using Ataw.Workflow.Core.DataAccess;
using Ataw.Framework.Core;
using Ataw.Framework.Web;
using System.Data.SqlClient;

namespace Ataw.Workflow.Core
{
      [CodePlug(ApproveUIProcess.REG_NAME, BaseClass = typeof(UIProcessor)
           , Tags = new PlugInTag[] { PlugInTag.Code, PlugInTag.Workflow },
        CreateDate = "2012-10-4", Author = "zhengyk", Description = "审批操作按钮")]
    public class ApproveUIProcess : UIProcessor
    {
        public const string REG_NAME = "ApproveUIProcess";


        protected virtual void SetApproveYes(bool isYes, WorkflowDbContext db)
        { 
        }
        public override   void Execute(WF_WORKFLOW_INST workflowRow)
        {
            // throw new NotImplementedException();
            if (UIData != null && UIData.Tables["WF_APPROVE_HISTORY"] != null)
            {
                var approveRow = UIData.Tables["WF_APPROVE_HISTORY"].Rows[0];

                var yes = approveRow["AH_APPROVE"].ToString();
               
                var note = approveRow["AH_NOTE"].ToString();

                WorkflowDbContext db = Source as WorkflowDbContext;
                SetApproveYes(yes.Value<bool>(),db);
                string sql = "INSERT INTO [WF_APPROVE_HISTORY] " +
                 "([AH_ID] " +
                 ",[AH_WORKFLOW_ID] " +
                 ",[AH_STEP_NAME] " +
                 ",[AH_STEP_DISPLAY_NAME] " +
                 ",[AH_OPERATOR] " +
                " ,[AH_APPROVE] " +
                " ,[AH_NOTE] " +
                " ,[AH_CREATE_ID] " +
                " ,[AH_CREATE_DATE]) " +
           "VALUES(@AH_ID,@AH_WORKFLOW_ID,@AH_STEP_NAME,@AH_STEP_DISPLAY_NAME,@AH_OPERATOR,@AH_APPROVE,@AH_NOTE,@AH_CREATE_ID,@AH_CREATE_DATE) "; 
                // "('" + db.GetUniId() + "' " +
                // ",'" + workflowRow.WI_ID + "' " +
                //" ,'" + workflowRow.WI_CURRENT_STEP + "' " +
                // ",'" + workflowRow.WI_CURRENT_STEP_NAME + "' " +
                //" ,'" + GlobalVariable.UserId.ToString() + "' " +
                //" ," + yes +
                //" ,'" + note + "' " +
                // " ,'" + GlobalVariable.UserId.ToString() + "' " +
                //" ,'" + db.Now.ToString() + "') ";

                List<SqlParameter> dblist = new List<SqlParameter>();
              
                dblist.Add(new SqlParameter("@AH_ID",db.GetUniId()));
                dblist.Add(new SqlParameter("@AH_WORKFLOW_ID", workflowRow.WI_ID));
                dblist.Add(new SqlParameter("@AH_STEP_NAME", workflowRow.WI_CURRENT_STEP));
                dblist.Add(new SqlParameter("@AH_STEP_DISPLAY_NAME",workflowRow.WI_CURRENT_STEP_NAME) );
                dblist.Add(new SqlParameter("@AH_OPERATOR",GlobalVariable.UserId.ToString()) );
                dblist.Add(new SqlParameter("@AH_APPROVE",yes));
                dblist.Add(new SqlParameter("@AH_NOTE",note));
                dblist.Add(new SqlParameter("@AH_CREATE_ID",GlobalVariable.UserId.ToString()) );
                dblist.Add(new SqlParameter("@AH_CREATE_DATE",db.Now.ToString()) );
                db.RegisterSqlCommand(sql, dblist.ToArray());

                workflowRow.WI_CUSTOM_DATA = yes;
            }

        }
    }
}
