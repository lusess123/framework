using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core.Data
{
    public abstract class CommonMomery : IMomery
    {
        public const string PAGE_SQL = "select * from ( "
                                  + "select row_number() over(order by CREATE_TIME DESC ) rn,TXT from ATAW_MOMERY WHERE 1=1 AND TXT LIKE @txt AND  SIGN = @sign )"
                                  + "tb where rn >(@PageNo)*@pageSize and rn <=(@PageNo + 1)*@pageSize";

        protected abstract string SignName
        { get; }

       
        protected abstract IUnitOfData DBContext
        {
            get;
        }
        protected abstract string FilterSql
        {
            get;
        }

        public IEnumerable<string> BeginSearch(string text)
        {
            string sql = "SELECT TXT  FROM ATAW_MOMERY WHERE TXT LIKE @txt and SIGN = @sign ";
            SqlParameter sql1 = new SqlParameter("@txt", text + "%");
            SqlParameter sql2 = new SqlParameter("@sign", SignName);
            List<string> res = new List<string>();
            DataSet ds = DBContext.QueryDataSet(sql, sql1, sql2);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string _txt = row["TXT"].ToString();
                res.Add(_txt);
            }
            return res;

        }

        public IEnumerable<string> Search(string text, int pageNo, int pageSize, ref int pageCount)
        {
            string countSql = "SELECT COUNT(TXT) FROM ATAW_MOMERY WHERE SIGN = @sign ";
            SqlParameter sql2 = new SqlParameter("@sign", SignName);
            pageCount = DBContext.QueryObject(countSql, sql2).Value<int>();


            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(new SqlParameter("@txt", SqlDbType.Text) { SqlValue = "%{0}%".AkFormat(text) });
            paraList.Add(new SqlParameter("@PageNo", SqlDbType.Int) { SqlValue = pageNo });
            paraList.Add(new SqlParameter("@pageSize", SqlDbType.Int) { SqlValue = pageSize });
            SqlParameter sql3 = new SqlParameter("@sign", SignName);
            paraList.Add(sql3);
            var ds = DBContext.QueryDataSet(PAGE_SQL, paraList.ToArray());
            List<string> res = new List<string>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string _txt = row["TXT"].ToString();
                res.Add(_txt);
            }
            return res;
        }

        public int AddText(string text,IUnitOfData unit)
        {
            string countSql = "SELECT COUNT(TXT) FROM ATAW_MOMERY WHERE SIGN = @sign  and TXT = @txt ";

            int count = DBContext.QueryObject(countSql, new SqlParameter("@sign", SignName), new SqlParameter("@txt", text)).Value<int>();
            if (count == 0)
            {
                // DBContext.QueryObject("");
                string sql = "INSERT ATAW_MOMERY (FID ,TXT,SIGN,CREATE_TIME) VALUES (@fid ,@txt,@sign,getdate()) ";
                SqlParameter sql1 = new SqlParameter("@txt", text);
                SqlParameter sql2 = new SqlParameter("@sign", SignName);
                SqlParameter sql3 = new SqlParameter("@fid", DBContext.GetUniId());
                return DBContext.ExecuteSqlCommand(sql, sql1, sql2, sql3);
            }
            return 0;

        }



        public int RemoveText(string text, IUnitOfData unit)
        {
            string sql = "DELETE FROM ATAW_MOMERY  WHERE TXT = @txt AND SIGN = @sign";
            SqlParameter sql1 = new SqlParameter("@txt", text);
            SqlParameter sql2 = new SqlParameter("@sign", SignName);

            return DBContext.ExecuteSqlCommand(sql, sql1, sql2);
        }
    }
}
