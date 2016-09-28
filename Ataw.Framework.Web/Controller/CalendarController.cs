using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ataw.Framework.Core;
using System.Data;
using System.Data.SqlClient;

namespace Ataw.Framework.Web
{
    public class CalendarController : AtawBaseController
    {

        private IUnitOfData UnitOfData
        {
            get
            {
                return AtawAppContext.Current.UnitOfData;
            }
        }

        public string GetCalendarMessage(string startTime, string endTime)
        {
            string sql = "select FID AS FID , USERID AS USERID , USERNAME AS USERNAME , USERID AS USERAVATAR , ACTIVITYITEMTYPE AS ACTIVITYITEMTYPE , " +
                         "ACTIVITYITEMACTION AS ACTIVITYITEMACTION , CREATE_TIME AS CREATE_TIME , SACT_TITLE AS SACT_TITLE , SACT_SUBCONTENT AS SACT_SUBCONTENT ," +
                          " ACTIVITYFROM AS ACTIVITYFROM , SOURCEID AS SOURCEID , REFERENCEID AS REFERENCEID , REFERENCENAME AS REFERENCENAME , PRIVACYSTATUS AS PRIVACYSTATUS ," +
                          " OWNERID AS OWNERID  from SNS_ACTIVITIES WHERE USERID='{0}' AND CREATE_TIME>='{1}' AND CREATE_TIME<='{2}'".AkFormat(AtawAppContext.Current.UserId, startTime, endTime);
            var dt = UnitOfData.QueryDataSet(sql).Tables[0];
            List<Object> result = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                switch (row["ACTIVITYITEMTYPE"].Value<string>())
                {
                    case "Signin":
                        result.Add(new { id = row["FID"].Value<string>(), title = ExtractTheTitle(row["SACT_SUBCONTENT"].Value<string>()), start = row["CREATE_TIME"].Value<string>(), end = "", url = "", type = row["ACTIVITYITEMTYPE"].Value<string>() });
                        break;
                    case "MicroBlog":
                        if (row["ACTIVITYITEMACTION"] != null && row["ACTIVITYITEMACTION"].Value<string>() == "Comment")
                        {
                            result.Add(new { id = row["FID"].Value<string>(), title = ExtractTheTitle(row["SACT_TITLE"].Value<string>()), start = row["CREATE_TIME"].Value<string>(), end = "", url = "", type = row["ACTIVITYITEMTYPE"].Value<string>() });
                        }
                        else if (row["ACTIVITYITEMACTION"].Value<string>() == "Create")
                        {
                            result.Add(new { id = row["FID"].Value<string>(), title = ExtractTheTitle(row["SACT_SUBCONTENT"].Value<string>()), start = row["CREATE_TIME"].Value<string>(), end = "", url = "", type = row["ACTIVITYITEMTYPE"].Value<string>() });
                        }
                        else { }
                        break;
                    case "MyWork":
                        if (row["SACT_TITLE"] == null || row["SACT_TITLE"].Value<string>() == null)
                        {
                            result.Add(new { id = row["FID"].Value<string>(), title = ExtractTheTitle(row["SACT_SUBCONTENT"].Value<string>()), start = row["CREATE_TIME"].Value<string>(), end = "", url = "", type = row["ACTIVITYITEMTYPE"].Value<string>() });
                        }
                        else
                        {
                            result.Add(new { id = row["FID"].Value<string>(), title = ExtractTheTitle(row["SACT_TITLE"].Value<string>()), start = row["CREATE_TIME"].Value<string>(), end = "", url = "", type = row["ACTIVITYITEMTYPE"].Value<string>() });
                        }
                        break;
                    case "WorkFlow":
                        if (row["SACT_TITLE"] == null || row["SACT_TITLE"].Value<string>() == null)
                        {
                            result.Add(new { id = row["FID"].Value<string>(), title = ExtractTheTitle(row["SACT_SUBCONTENT"].Value<string>()), start = row["CREATE_TIME"].Value<string>(), end = "", url = "", type = row["ACTIVITYITEMTYPE"].Value<string>() });
                        }
                        else
                        {
                            result.Add(new { id = row["FID"].Value<string>(), title = ExtractTheTitle(row["SACT_TITLE"].Value<string>()), start = row["CREATE_TIME"].Value<string>(), end = "", url = "", type = row["ACTIVITYITEMTYPE"].Value<string>() });
                        }
                        break;
                    default:
                        string strTitle = "";
                        try
                        {
                            strTitle += ExtractTheTitle(row["SACT_SUBCONTENT"].Value<string>());
                        }
                        catch (Exception e) { }
                        try
                        {
                            strTitle += ExtractTheTitle(row["SACT_TITLE"].Value<string>());
                        }
                        catch (Exception e1) { }
                        result.Add(new { id = row["FID"].Value<string>(), title = strTitle, start = row["CREATE_TIME"].Value<string>(), end = "", url = "", type = row["ACTIVITYITEMTYPE"].Value<string>() });
                        break;

                }
            }
            //待办事项
            sql = @"SELECT FID, CONTENT,BEGIN_TIME,END_TIME,ALLDAY FROM SNS_TODO_ITEMS WHERE CREATE_ID='{0}' AND CREATE_TIME >='{1}' AND CREATE_TIME<='{2}' AND (ISDELETE=0 OR ISDELETE IS NULL) 
                    ORDER BY CREATE_TIME ASC".AkFormat(AtawAppContext.Current.UserId, startTime, endTime);
            dt = UnitOfData.QueryDataSet(sql).Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                var allDay = row["ALLDAY"].Value<bool>();
                string title = row["CONTENT"].Value<string>();
                result.Add(new
                {
                    id = row["FID"].Value<string>(),
                    title = title,
                    start = row["BEGIN_TIME"].Value<string>(),
                    end = row["END_TIME"].Value<string>(),
                    url = "",
                    type = ActivityItemType.ToDoItems.ToString(),
                    allDay = allDay 
                });
            }
            return ReturnJson(result);
        }

        public string ExtractTheTitle(string title)
        {
            var htmlStr = "";
            var star = 0;
            var stop = 0;
            while (title.IndexOf("<") != -1)
            {
                star = title.IndexOf("<");
                stop = title.IndexOf(">") != -1 ? title.IndexOf(">") + 1 : 0;
                htmlStr = title.Substring(star, (stop - star));
                title = title.Replace(htmlStr, "");
            }
            return title;
        }

        /// <summary>
        /// 删除日历中待办事项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string DeleteToDoItem(string key)
        {
            string deleteSql = "UPDATE SNS_TODO_ITEMS SET ISDELETE=1 WHERE FID=@FID";
            UnitOfData.QueryObject(deleteSql, new SqlParameter("@FID", key));
            return ReturnJson("1");
        }

        /// <summary>
        /// 更新拖动后的待办事项
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="dayDiff"></param>
        /// <param name="minDiff"></param>
        /// <param name="allDay"></param>
        /// <returns></returns>
        public string UpdateToDoItem(string fid, string dayDiff, string minDiff, bool allDay)
        {
            string selectSql = "SELECT BEGIN_TIME,END_TIME FROM SNS_TODO_ITEMS WHERE FID=@FID";
            var dt = UnitOfData.QueryDataSet(selectSql, new SqlParameter("@FID", fid)).Tables[0];
            string updateSql = "";
            List<SqlParameter> paramList = new List<SqlParameter>();
            if (allDay)
            {
                if (!dt.Rows[0]["END_TIME"].ToString().IsEmpty())
                {
                    updateSql = "UPDATE SNS_TODO_ITEMS SET BEGIN_TIME=dateAdd(day,@DayDiff,BEGIN_TIME),END_TIME=dateAdd(day,@DayDiff,END_TIME),ALLDAY=1 WHERE FID=@FID";

                }
                else
                {
                    updateSql = "UPDATE SNS_TODO_ITEMS SET BEGIN_TIME=dateAdd(day,@DayDiff,BEGIN_TIME),ALLDAY=1 WHERE FID=@FID";
                }
                paramList.Add(new SqlParameter("@DayDiff", dayDiff.Value<int>()));
                paramList.Add(new SqlParameter("@FID", fid));
            }
            else
            {
                int minuteDiff = dayDiff.Value<int>() * 24 * 60 + minDiff.Value<int>();
                if (!dt.Rows[0]["END_TIME"].ToString().IsEmpty())
                {
                    updateSql = "UPDATE SNS_TODO_ITEMS SET BEGIN_TIME=dateAdd(minute,@MinuteDiff,BEGIN_TIME),END_TIME=dateAdd(minute,@MinuteDiff,END_TIME),ALLDAY=0 WHERE FID=@FID";

                }
                else
                {
                    updateSql = "UPDATE SNS_TODO_ITEMS SET BEGIN_TIME=dateAdd(minute,@MinuteDiff,BEGIN_TIME),ALLDAY=0 WHERE FID=@FID";
                }
                paramList.Add(new SqlParameter("@MinuteDiff", minuteDiff));
                paramList.Add(new SqlParameter("@FID", fid));
            }
            UnitOfData.RegisterSqlCommand(updateSql, paramList.ToArray());
            UnitOfData.Submit();
            return ReturnJson("1");
        }
    }
}
