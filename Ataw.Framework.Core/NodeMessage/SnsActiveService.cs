using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Ataw.Framework.Core
{
    [CodePlug("SnsActiveService", BaseClass = typeof(ISnsActive),
   CreateDate = "2013-08-30", Author = "zyk", Description = "消息推送服务")]
    public class SnsActiveService : ISnsActive
    {
        private IUnitOfData fUnitOfData;
        private bool isSysUnitOfData;

        private SingleDbAccess DbAccess;

        public SnsActiveService()
            : this(AtawAppContext.Current.UnitOfData)
        {
            isSysUnitOfData = true;
        }

        public SnsActiveService(IUnitOfData unitOfData)
        {
            SetUnitOfData(unitOfData);
            DbAccess = new SingleDbAccess();
            DbAccess.TableName = "SNS_ACTIVITIES";
            DbAccess.SelectFields = new string[] {
                "FID","USERID","USERNAME","ACTIVITYITEMTYPE","ACTIVITYITEMACTION","SOURCEID",
                "SACT_TITLE","SACT_SUBCONTENT","PRIVACYSTATUS","CREATE_TIME","UPDATE_TIME","ACTIVITYSTATUS","REFERENCEID","REFERENCENAME"
             };
        }

        public void SetUnitOfData(IUnitOfData unitOfData)
        {
            fUnitOfData = unitOfData;
            isSysUnitOfData = false;
        }

        public IEnumerable<ActiveModel> GetAllActivePageByUserID(Pagination page, ActiveModel whereBean)
        {
            DbAccess.Page = page;
            return null;
        }

        public IEnumerable<ActiveModel> GetComingActiveByUserID(string userId)
        {
            // DbAccess.Where += " AND ( USERID = @USERID OR ) "

            return null;
        }

        private ActiveModel MapingRow(DataRow row)
        {
            ActiveModel bean = new ActiveModel();
            bean.FID = row["FID"].ToString();
            bean.CreateTime = row["CREATE_TIME"].Value<DateTime>();
            bean.ItemAction = row["ACTIVITYITEMACTION"].Value<string>();
            bean.ItemType = row["ACTIVITYITEMTYPE"].Value<string>();
            bean.PrivacyStatus = row["PRIVACYSTATUS"].ToString();
            bean.SourceId = row["SOURCEID"].ToString();
            bean.SubContent = row["SACT_SUBCONTENT"].ToString();
            bean.Title = row["SACT_TITLE"].ToString();
            bean.UserId = row["USERID"].ToString();
            bean.UserName = row["USERNAME"].ToString();
            bean.ActivityStatus = row["ACTIVITYSTATUS"].Value<int>();
            bean.ReferenceID = row["REFERENCEID"].ToString();
            bean.ReferenceName = row["REFERENCENAME"].ToString();
            return bean;
        }

        private void CreateBaseWhere()
        {
            DbAccess.Where += " AND  ((ISDELETE IS NULL) OR (ISDELETE = 0)) ";
        }


        public void InsertActive(ActiveModel bean)
        {
            // throw new NotImplementedException();
            string _sql = "INSERT INTO SNS_ACTIVITIES( FID ,USERID ,USERNAME,ACTIVITYITEMTYPE," +
                 " ACTIVITYITEMACTION,SOURCEID,PRIVACYSTATUS ,CREATE_ID,REFERENCEID,REFERENCENAME " +
                " ,CREATE_TIME,UPDATE_ID ,UPDATE_TIME,FControlUnitID,ISDELETE  ,SACT_TITLE ,SACT_SUBCONTENT,ACTIVITYFROM,OWNERID,ACTIVITYSTATUS)" +
                               " VALUES ( @FID ,@USERID ,@USERNAME,@ACTIVITYITEMTYPE," +
                 " @ACTIVITYITEMACTION,@SOURCEID,@PRIVACYSTATUS ,@CREATE_ID,@REFERENCEID,@REFERENCENAME " +
             " ,@CREATE_TIME,@UPDATE_ID ,@UPDATE_TIME,@FControlUnitID,@ISDELETE  ,@SACT_TITLE ,@SACT_SUBCONTENT,@ACTIVITYFROM,@OWNERID,@ACTIVITYSTATUS)";

            List<SqlParameter> pams = new List<SqlParameter>();
            pams.Add(new SqlParameter("@FID", SqlDbType.VarChar) { SqlValue = fUnitOfData.GetUniId() });
            pams.Add(new SqlParameter("@USERID", SqlDbType.VarChar) { SqlValue = bean.UserId });
            pams.Add(new SqlParameter("@USERNAME", SqlDbType.VarChar) { SqlValue = bean.UserName });
            pams.Add(new SqlParameter("@ACTIVITYITEMTYPE", SqlDbType.NVarChar) { Value = bean.ItemType });
            pams.Add(new SqlParameter("@PRIVACYSTATUS", SqlDbType.NVarChar) { SqlValue = bean.PrivacyStatus });
            pams.Add(new SqlParameter("@ACTIVITYITEMACTION", SqlDbType.NVarChar) { SqlValue = bean.ItemAction });
            pams.Add(new SqlParameter("@CREATE_ID", SqlDbType.NVarChar) { SqlValue = bean.UserId.IsEmpty() ? AtawAppContext.Current.UserId : bean.UserId});
            pams.Add(new SqlParameter("@CREATE_TIME", SqlDbType.DateTime) { SqlValue = fUnitOfData.Now });
            pams.Add(new SqlParameter("@UPDATE_ID", SqlDbType.NVarChar) { SqlValue = bean.FID });
            pams.Add(new SqlParameter("@UPDATE_TIME", SqlDbType.NVarChar) { SqlValue = fUnitOfData.Now });
            pams.Add(new SqlParameter("@FControlUnitID", SqlDbType.NVarChar) { SqlValue = bean.FControlUnitID.IsEmpty() ? AtawAppContext.Current.FControlUnitID : bean.FControlUnitID });
            pams.Add(new SqlParameter("@ISDELETE", SqlDbType.Bit) { SqlValue = 0 });

            pams.Add(new SqlParameter("@SOURCEID", SqlDbType.NVarChar) { SqlValue = bean.SourceId });
            pams.Add(new SqlParameter("@SACT_TITLE", SqlDbType.NVarChar) { SqlValue = bean.Title });
            pams.Add(new SqlParameter("@SACT_SUBCONTENT", SqlDbType.NVarChar) { SqlValue = bean.SubContent });
            pams.Add(new SqlParameter("@ACTIVITYFROM", SqlDbType.NVarChar) { SqlValue = bean.ActivityFrom });
            pams.Add(new SqlParameter("@OWNERID", SqlDbType.NVarChar) { SqlValue = bean.OwnerID });

            pams.Add(new SqlParameter("@ACTIVITYSTATUS", SqlDbType.Int) { SqlValue = 0 });
            pams.Add(new SqlParameter("@REFERENCEID", SqlDbType.NVarChar) { SqlValue = bean.ReferenceID });
            pams.Add(new SqlParameter("@REFERENCENAME", SqlDbType.NVarChar) { SqlValue = bean.ReferenceName });

            fUnitOfData.RegisterSqlCommand(_sql, pams.ToArray());

        }
    }
}
