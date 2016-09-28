using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;
namespace Ataw.Workflow.Core
{
    [Serializable]
    public class WorkflowContent : XmlConfigBase
    {
        public WorkflowContent()
        {
            Tables = new RegNameList<WorkflowModel>();
        }

        [XmlArrayItem("Table")]
        public RegNameList<WorkflowModel> Tables { get; set; }

        public string MainTableName { get; set; }
        public string Prefix { get; set; }
        public string FID { get; set; }

        public DataSet AppendToDataSet(DataSet ds)
        {
            foreach (WorkflowModel model in Tables)
            {
                string tableName = model.RegName + "_SEARCH";
                DataTable tb = null;
                if (!ds.Tables.Contains(tableName))
                {
                    tb = new DataTable(tableName);
                    ds.Tables.Add(tb);
                }
                tb = ds.Tables[tableName];

                foreach (KeyValueItem kvt in model.InputParams)
                {
                    if (!tb.Columns.Contains(kvt.Key))
                    {
                        tb.Columns.Add(kvt.Key, typeof(string));
                    }
                    DataRow row = null;
                    if (tb.Rows.Count > 0)
                    {
                        row = tb.Rows[0];
                    }
                    else
                    {
                        row = tb.NewRow();
                        tb.Rows.Add(row);
                    }
                    row[kvt.Key] = kvt.Value;

                }
            }
            return ds;

        }

        public void SetAllMainRow(IUnitOfData context, StepConfig stepConfig, object workflowId)
        {
            WorkflowDbContext wfContent = context as WorkflowDbContext;
            string sql = "UPDATE {0} SET  " +
            " {1}_WF_ID = @WF_ID ," +
            " {1}_IS_APPLY_WF = 1 ," +
            " {1}_WF_IS_END = 0 ,  " +
            " {1}_WF_STATUS = @WF_STATUS ,  " +
            " {1}_STEP_NAME = @STEP_NAME ,  " +
            " {1}_WF_TIME = @WF_TIME " +
            " WHERE FID = @FID";
            sql = string.Format(sql, MainTableName, Prefix);

            wfContent.RegisterSqlCommand(sql,
                new SqlParameter[] {
                    new SqlParameter("@WF_ID",workflowId){  DbType = DbType.String},
                    new SqlParameter("@FID",FID){  DbType = DbType.String},
                    new SqlParameter("@WF_STATUS",stepConfig.DisplayName
                        ){  DbType = DbType.String},
                    new SqlParameter("@STEP_NAME",stepConfig.Name){  DbType = DbType.String},
                     new SqlParameter("@WF_TIME",wfContent.Now){  DbType = DbType.DateTime}
                }
                );
        }

        public void SetMainRowCreateID(IUnitOfData context, string userID)
        {
            WorkflowDbContext wfContent = context as WorkflowDbContext;
            string sql = "UPDATE {0} SET  " +
            " CREATE_ID = @USERID" +
            " WHERE FID = @FID";
            sql = string.Format(sql, MainTableName);

            wfContent.RegisterSqlCommand(sql,
                new SqlParameter[] {
                     new SqlParameter("@USERID",userID){  DbType = DbType.String},
                     new SqlParameter("@FID",FID){  DbType = DbType.String}
                }
                );
        }
        public void SetMainRowStatus(IUnitOfData context, StepConfig stepConfig)
        {
            WorkflowDbContext wfContent = context as WorkflowDbContext;
            string sql = "UPDATE {0} SET  " +
            " {1}_WF_STATUS = @WF_STATUS ,  " +
            " {1}_STEP_NAME = @STEP_NAME ,  " +
             " {1}_WF_TIME = @WF_TIME " +
            " WHERE FID = @FID";
            sql = string.Format(sql, MainTableName, Prefix);

            wfContent.RegisterSqlCommand(sql,
                new SqlParameter[] {
                    new SqlParameter("@WF_STATUS",stepConfig.DisplayName){
                         DbType = DbType.String
                    },
                    new SqlParameter("@STEP_NAME",stepConfig.Name){
                         DbType = DbType.String
                    },
                     new SqlParameter("@WF_TIME",wfContent.Now){  DbType = DbType.DateTime},
                     new SqlParameter("@FID",FID){  DbType = DbType.String},
                }
                );
        }

        public void EndMainRowStatus(WorkflowDbContext context, FinishType finishType)
        {
            string sql = "UPDATE {0} SET  " +
            " {2}  " +
            " {1}_WF_STATUS = @WF_STATUS , " +
            " {1}_WF_IS_END = @WF_IS_END , " +
              " {1}_WF_TIME = @WF_TIME " +
            " WHERE FID = @FID";
            string statusSql = Prefix + "_STEP_NAME = @STEP_NAME ,";
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter statusPar = new SqlParameter("@WF_IS_END", (int)finishType);
            SqlParameter fidParm = new SqlParameter("@FID", FID) { DbType = DbType.String };
            SqlParameter wftimeParm = new SqlParameter("@WF_TIME", context.Now) { DbType = DbType.DateTime };
            list.Add(statusPar);
            list.Add(fidParm);
            list.Add(wftimeParm);
            string sql0 = string.Format(sql, MainTableName, Prefix, "");

            switch (finishType)
            {
                case FinishType.ReturnBegin:
                    break;
                case FinishType.Abort:
                    //fMainRow[prefix + "WF_STATUS"] = "终止";
                    list.Add(new SqlParameter("@WF_STATUS", "终止"));
                    //fMainRow[prefix + "STEP_NAME"] = "__abort";
                    list.Add(new SqlParameter("@STEP_NAME", "__abort"));
                    sql0 = string.Format(sql, MainTableName, Prefix, statusSql);
                    break;
                case FinishType.OverTryTimes:
                    //fMainRow[prefix + "WF_STATUS"] = "重试错误终止";
                    list.Add(new SqlParameter("@WF_STATUS", "重试错误终止"));
                    break;
                case FinishType.Error:
                    // fMainRow[prefix + "WF_STATUS"] = "错误终止";
                    list.Add(new SqlParameter("@WF_STATUS", "错误终止"));
                    break;
                default:
                    list.Add(new SqlParameter("@WF_STATUS", "结束"));
                    break;
            }
            context.RegisterSqlCommand(sql0, list.ToArray());
        }


    }
}
