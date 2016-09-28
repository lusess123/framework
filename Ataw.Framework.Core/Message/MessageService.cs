using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ataw.Framework.Core;
using System.Data;
using System.Data.SqlClient;

namespace Ataw.Framework.Core
{
    [CodePlug("MessageService", BaseClass = typeof(IMessage),
   CreateDate = "2014-03-25", Author = "sj", Description = "消息推送服务")]
    public class MessageService : IMessage
    {
        private IUnitOfData fUnitOfData;

        public MessageService()
        {
            fUnitOfData = AtawAppContext.Current.UnitOfData;
        }

        private MessageModel MapingRow(DataRow row)
        {
            MessageModel bean = new MessageModel();
            if (row.Table.Columns.Contains("FID"))
                bean.FID = row["FID"].ToString();
            if (row.Table.Columns.Contains("TENANT_ID"))
                bean.TenantID = row["TENANT_ID"].ToString();
            if (row.Table.Columns.Contains("APPLICATION_ID"))
                bean.ApplicationID = row["APPLICATION_ID"].ToString();
            if (row.Table.Columns.Contains("MODULE_ID"))
                bean.ModuleID = row["MODULE_ID"].ToString();
            if (row.Table.Columns.Contains("SOURCEID"))
                bean.SourceID = row["SOURCEID"].ToString();
            if (row.Table.Columns.Contains("ORGANIZATION_ID"))
                bean.OrganizationID = row["ORGANIZATION_ID"].ToString();
            if (row.Table.Columns.Contains("SEND_ID"))
                bean.SendID = row["SEND_ID"].ToString();
            if (row.Table.Columns.Contains("RECEIVE_ID"))
                bean.ReceiveID = row["RECEIVE_ID"].ToString();
            if (row.Table.Columns.Contains("BODY"))
                bean.Body = row["BODY"].ToString();
            if (row.Table.Columns.Contains("FID"))
                bean.Category = row["CATEGORY"].ToString();
            if (row.Table.Columns.Contains("PRIVACY"))
                bean.Privacy = row["PRIVACY"].ToString();
            if (row.Table.Columns.Contains("TYPE"))
                bean.Type = row["TYPE"].ToString();
            if (row.Table.Columns.Contains("PRIORITY"))
                bean.Priority = row["PRIORITY"].ToString();
            if (row.Table.Columns.Contains("ISHANDLED"))
                bean.IsHandled = row["ISHANDLED"].Value<bool>();
            if (row.Table.Columns.Contains("CREATE_TIME"))
                bean.CreateTime = row["CREATE_TIME"].Value<DateTime>();

            return bean;
        }


        public void InsertMessage(MessageModel bean)
        {
            // throw new NotImplementedException();
            string _sql = "INSERT INTO ATAW_MESSAGES(FID,TENANT_ID,APPLICATION_ID,ORGANIZATION_ID,MODULE_ID," +
                 "SOURCE_ID,SEND_ID,RECEIVE_ID,BODY,TYPE,CATEGORY,PRIVACY,ISHANDLED,CREATE_ID,CREATE_TIME" +
                 " VALUES (@FID,@TENANT_ID,@APPLICATION_ID,@ORGANIZATION_ID," +
                 "@MODULE_ID,@SOURCE_ID,@SEND_ID,@RECEIVE_ID,@BODY,@TYPE,@CATEGORY,@PRIVACY,@ISHANDLED,@CREATE_ID,@CREATE_TIME)";

            List<SqlParameter> pams = new List<SqlParameter>();
            pams.Add(new SqlParameter("@FID", SqlDbType.NVarChar) { SqlValue = fUnitOfData.GetUniId() });
            pams.Add(new SqlParameter("@TENANT_ID", SqlDbType.NVarChar) { SqlValue = bean.TenantID });
            pams.Add(new SqlParameter("@APPLICATION_ID", SqlDbType.NVarChar) { SqlValue = bean.ApplicationID });
            pams.Add(new SqlParameter("@ORGANIZATION_ID", SqlDbType.NVarChar) { Value = bean.OrganizationID });
            pams.Add(new SqlParameter("@MODULE_ID", SqlDbType.NVarChar) { SqlValue = bean.ModuleID });
            pams.Add(new SqlParameter("@SOURCE_ID", SqlDbType.NVarChar) { SqlValue = bean.SourceID });
            pams.Add(new SqlParameter("@SEND_ID", SqlDbType.NVarChar) { SqlValue = bean.SendID });
            pams.Add(new SqlParameter("@RECEIVE_ID", SqlDbType.NVarChar) { SqlValue = bean.ReceiveID });
            pams.Add(new SqlParameter("@BODY", SqlDbType.NVarChar) { SqlValue = bean.Body });
            pams.Add(new SqlParameter("@TYPE", SqlDbType.NVarChar) { SqlValue = bean.Type });
            pams.Add(new SqlParameter("@CATEGORY", SqlDbType.NVarChar) { SqlValue = bean.Category });
            pams.Add(new SqlParameter("@PRIVACY", SqlDbType.NVarChar) { SqlValue = bean.Privacy });
            pams.Add(new SqlParameter("@ISHANDLED", SqlDbType.NVarChar) { SqlValue = bean.IsHandled });
            pams.Add(new SqlParameter("@CREATE_ID", SqlDbType.NVarChar) { SqlValue = AtawAppContext.Current.UserId });
            pams.Add(new SqlParameter("@CREATE_TIME", SqlDbType.DateTime) { SqlValue = fUnitOfData.Now });

            fUnitOfData.RegisterSqlCommand(_sql, pams.ToArray());
        }

        public IEnumerable<ToDoItem> GetToDoWork()
        {
            string sql = @"SELECT FID,APPLICATION_ID,BODY,TYPE,CATEGORY,RECEIVE_ID,SOURCE_ID,PRIVACY,CREATE_TIME FROM ATAW_MESSAGES 
                           WHERE TYPE=@TYPE AND RECEIVE_ID=@USERID AND (ISHANDLED!=1 OR ISHANDLED IS NULL) ORDER BY CREATE_TIME DESC";
            var paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("@TYPE", MessageType.ToDo.ToString()));
            paras.Add(new SqlParameter("@USERID", AtawAppContext.Current.UserId));
            var dt = fUnitOfData.QueryDataSet(sql, paras.ToArray()).Tables[0];
            var result = new List<ToDoItem>();
            var delRows = new List<DataRow>();
            if (dt.Rows.Count > 0)
            {
                result = GetToDoItems(dt.Select().ToList());
            }
            return result;

        }

        public List<ToDoItem> GetToDoItems(List<DataRow> source)
        {
            var result = new List<ToDoItem>();
            var list = source.GroupBy(x => x["CATEGORY"].ToString());
            list.ToList().ForEach(x =>
            {
                var todoItem = new ToDoItem();
                todoItem.CategoryName = x.Key;//x.Key.ToEnum<MessageType>().GetDescription();
                todoItem.ItemList = new List<MessageModel>();
                x.ToList().ForEach(k =>
                {
                    todoItem.ItemList.Add(new MessageModel { Body = k["BODY"].ToString(), CreateTime = k["CREATE_TIME"].Value<DateTime>() });
                });
                result.Add(todoItem);
            });
            return result;
        }



        public List<ToDoItem> GetToDoItemsGroupByApp(List<DataRow> source)
        {
            var result = new List<ToDoItem>();

            var list = source.GroupBy(x => x["APPLICATION_ID"].ToString());
            var temp = new List<List<DataRow>>();
            list.ToList().ForEach(x =>
            {
                var todoItem = new ToDoItem();
                todoItem.CategoryName = x.Key.ToEnum<MessageType>().GetDescription();
                result.Add(todoItem);
                temp.Add(x.ToList());
                todoItem.SubItemList = new List<ToDoItem>();
            });
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i].GroupBy(x => x["TYPE"].ToString()).ToList().ForEach(x =>
                {
                    var todoSubItem = new ToDoItem();
                    todoSubItem.CategoryName = x.Key;
                    todoSubItem.ItemList = new List<MessageModel>();
                    result[i].SubItemList.Add(todoSubItem);
                    x.ToList().ForEach(k =>
                    {
                        todoSubItem.ItemList.Add(new MessageModel { Body = k["BODY"].ToString(), CreateTime = k["CREATE_TIME"].Value<DateTime>() });
                    });
                });

            }
            return result;
        }

        public void UpdateMessageStatus(string sourceID)
        {
            string sql = "UPDATE ATAW_MESSAGES SET ISHANDLED=1 WHERR SOURCE_ID=@SOURCEID";
            var paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("@SOURCEID", sourceID));
            fUnitOfData.ExecuteSqlCommand(sql, paras.ToArray());
        }

        public List<DataRow> GetUnHandledMessges()
        {
            string sql = @"SELECT FID,TYPE,RECEIVE_ID,SOURCE_ID,PRIVACY,CREATE_TIME FROM ATAW_MESSAGES 
                           WHERE (ISHANDLED!=1 OR ISHANDLED IS NULL) AND RECEIVE_ID=@USERID ORDER BY CREATE_TIME DESC";
            var dt = fUnitOfData.QueryDataSet(sql, new SqlParameter("@USERID", AtawAppContext.Current.UserId)).Tables[0];
            return dt.Select().ToList();
        }

        public List<DataRow> GetNewMessages(string latestTime)
        {
            string sql = @"SELECT FID,TYPE,RECEIVE_ID,SOURCE_ID,PRIVACY,CREATE_TIME FROM ATAW_MESSAGES 
                           WHERE {0} ORDER BY CREATE_TIME DESC";
            string filter = "(ISHANDLED!=1 OR ISHANDLED IS NULL) AND RECEIVE_ID=@USERID";
            var paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("@USERID", AtawAppContext.Current.UserId));
            if (!latestTime.IsEmpty())
            {
                filter = "{0} AND CREATE_TIME>@DATETIME".AkFormat(filter);
                paras.Add(new SqlParameter("DATETIME", latestTime.Value<DateTime>().AddSeconds(1).ToString()));

            }
            var dt = fUnitOfData.QueryDataSet(sql.AkFormat(filter), paras.ToArray()).Tables[0];
            return dt.Select().ToList();
        }
    }
}