using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Ataw.Framework.Core
{
    public class SqlCmdData
    {
        public int ParamIndex { get; set; }
        public List<SqlParameter> ParamList { get; set; }
        public string SqlTxt { get; set; }
        public DataSet QueryDataSet(IUnitOfData unitData)
        {
            //if (ParamList != null && ParamList.Count > 0)
            //{
            return unitData.QueryDataSet(SqlTxt, ParamList.ToArray());
            //}
            //return null;
        }

        public object QueryObject(IUnitOfData unitData)
        {
            if (ParamList != null && ParamList.Count > 0)
            {
                return unitData.QueryObject(SqlTxt, ParamList.ToArray());
            }
            return null;
        }
    }

    public static class SqlUtil
    {
        public static SqlCmdData ByAnd(this SqlCmdData sql, string keyColumn, List<string> valList)
        {
            AtawDebug.AssertArgument(valList != null && valList.Count > 0, "valList", "条件查询不能为空", sql);
            StringBuilder resSb = new StringBuilder(sql.SqlTxt);
            int i = sql.ParamIndex;
            foreach (string val in valList)
            {
                i++;
                resSb.Append("  AND ");
                resSb.Append(keyColumn);
                resSb.Append("  =  @p");
                resSb.Append(i.ToString());
                sql.ParamList.Add(new SqlParameter("@p" + i.ToString(), val) { DbType = DbType.String });

            }
            sql.SqlTxt = resSb.ToString();
            sql.ParamIndex = i;

            return sql;
        }

        public static SqlCmdData SqlByUnionAll(string select, string keyColumn, List<string> valList)
        {
            List<SqlParameter> sqlList = new List<SqlParameter>();
            StringBuilder resSb = null;
            int i = 0;
            foreach (string val in valList)
            {
                StringBuilder sb = new StringBuilder(select);
                if (resSb == null)
                {
                    resSb = sb;
                }
                else
                {
                    resSb.Append(" UNION ALL ");
                    resSb.Append(sb);
                }


                resSb.Append("  AND  ");
                resSb.Append(keyColumn);
                resSb.Append("  =  @p");
                resSb.Append(i.ToString());
                sqlList.Add(new SqlParameter("@p" + i.ToString(), val) { DbType = DbType.String });
                i++;
            }

            return new SqlCmdData()
            {
                ParamList = sqlList,
                SqlTxt = resSb.ToString(),
                ParamIndex = i
            };
        }


        public static SqlCmdData SqlByNotAnd(string select, string keyColumn, List<string> valList)
        {
            List<SqlParameter> sqlList = new List<SqlParameter>();
            StringBuilder resSb = new StringBuilder(select);
            int i = 0;
            foreach (string val in valList)
            {
                resSb.Append(" AND ");
                resSb.Append(keyColumn);
                resSb.Append("  <> @p");
                resSb.Append(i);
                sqlList.Add(new SqlParameter("@p" + i.ToString(), val) { DbType = DbType.String });
                i++;
            }
            return new SqlCmdData()
                     {
                         ParamList = sqlList,
                         SqlTxt = resSb.ToString(),
                         ParamIndex = i
                     };

        }
    }
}
