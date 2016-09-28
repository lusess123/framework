using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Ataw.Framework.Core;
using Ataw.Framework.Web;
using Ataw.Workflow.Core;
using Ataw.Workflow.Core.DataAccess;
using Ataw.Workflow.Web.Bussiness;

namespace Ataw.Workflow.Web
{
    [AtawRightFilter]
    public class MyWorkController : AtawBaseController
    {
        //
        // GET: /MyWork/
        private MyWork mywork = new MyWork();
        public ActionResult Index()
        {
            WorkflowDbContext context = new WorkflowDbContext(PlugAreaRegistration.CONN);
            string sql = string.Format(ObjectUtil.SysCulture,
                @" UPDATE WF_WORKFLOW_INST SET WI_STATUS = {0}, WI_RECEIVE_ID = {1}, "
                + "WI_RECEIVE_DATE = {2}  WHERE WI_STEP_TYPE = {3} AND WI_STATUS = {4} "
                + "AND WI_RECEIVE_COUNT = {5} AND WI_RECEIVE_LIST LIKE {6}",
                context.GetSqlParamName("NEWWI_STATUS"), context.GetSqlParamName("WI_RECEIVE_ID"),
                context.GetSqlParamName("WI_RECEIVE_DATE"), context.GetSqlParamName("WI_STEP_TYPE"),
                context.GetSqlParamName("WI_STATUS"), context.GetSqlParamName("WI_RECEIVE_COUNT"),
                context.GetSqlParamName("WI_RECEIVE_LIST"));

            // s dblist = new DbParameterList();
            List<SqlParameter> dblist = new List<SqlParameter>();

            dblist.Add(new SqlParameter("@NEWWI_STATUS", (int)StepState.OpenNotProcess) { DbType = DbType.Int32, Value = (int)StepState.OpenNotProcess });
            dblist.Add(new SqlParameter("@WI_RECEIVE_ID", GlobalVariable.UserId.ToString()) { DbType = DbType.String });
            dblist.Add(new SqlParameter("@WI_RECEIVE_DATE", DateTime.Now) { DbType = DbType.DateTime });
            dblist.Add(new SqlParameter("@WI_STEP_TYPE", (int)StepType.Manual) { DbType = DbType.Int32 });
            dblist.Add(new SqlParameter("@WI_STATUS", (int)StepState.NotReceive) { DbType = DbType.Int32, Value = (int)StepState.NotReceive });
            dblist.Add(new SqlParameter("@WI_RECEIVE_COUNT", 1) { DbType = DbType.Int32 });
            dblist.Add(new SqlParameter("@WI_RECEIVE_LIST", QuoteIdList.GetQuoteId(GlobalVariable.UserId.ToString())) { DbType = DbType.String });

            int res = context.ExecuteSqlCommand(sql, dblist.ToArray());
            return View();
        }
        public string InitData(string name, string begin, string end)
        {
            var myWorkFlowInstTrees = mywork.QueryData(name, begin, end);
            return FastJson(new { total = myWorkFlowInstTrees.Count(), rows = myWorkFlowInstTrees });
        }
        public ActionResult ProcessDetail(string id)
        {
            return View(mywork.ProcessDetail(id));
        }

        public string WorkflowProcess()
        {
            return mywork.WorkflowProcess(PostDataSet.Tables["PARAM"].Rows[0]["WID"].ToString(), PostDataSet);
        }
    }
}
