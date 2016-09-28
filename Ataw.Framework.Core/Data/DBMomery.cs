using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core.Data
{
   public abstract class DBMomery:IMomery
    {

       private string PAGE_SQL;
       public DBMomery() { 
        
   }

       protected abstract string TXTFileName
       {
           get;
       }
       protected abstract string TableName
       {
           get;
       }
       protected abstract IUnitOfData DBContext
       {
           get;
       }
       protected virtual string AndFilterSql
       {
           get {
               return "  AND 1=1";
           }
       }

       public IEnumerable<string> BeginSearch(string text)
       {
           string sql = "SELECT {1}  FROM {0} WHERE {1} LIKE @txt  {2} ".AkFormat(TableName, TXTFileName, AndFilterSql);
           SqlParameter sql1 = new SqlParameter("@txt",text+"%");
          // SqlParameter sql2 = new SqlParameter("@sign",SignName);
           List<string> res = new List<string>();
           DataSet ds = DBContext.QueryDataSet(sql, sql1);
           foreach (DataRow row in ds.Tables[0].Rows)
           {
               string _txt = row[TXTFileName].ToString();
               res.Add(_txt);
           }
           return res;

       }

       public IEnumerable<string> Search(string text, int pageNo, int pageSize, ref int pageCount)
       {
           PAGE_SQL = ("select * from ( "
                                + "select row_number() over(order by CREATE_TIME DESC ) rn,{1} from {0} WHERE 1=1 AND {1} LIKE @txt {2} )"
                                + "tb where rn >(@PageNo)*@pageSize and rn <=(@PageNo + 1)*@pageSize").AkFormat(TableName, TXTFileName, AndFilterSql);
           string countSql = "SELECT COUNT({1}) FROM {0} WHERE 1=1 {2}  ".AkFormat(TableName,TXTFileName,AndFilterSql);
           // SqlParameter sql2 = new SqlParameter("@sign",SignName);
            pageCount = DBContext.QueryObject(countSql).Value<int>();

            
           List<SqlParameter> paraList = new List<SqlParameter>();
           paraList.Add(new SqlParameter("@txt", SqlDbType.Text) { SqlValue = "%{0}%".AkFormat(text) });
           paraList.Add(new SqlParameter("@PageNo", SqlDbType.Int) { SqlValue = pageNo });
           paraList.Add(new SqlParameter("@pageSize", SqlDbType.Int) { SqlValue = pageSize });
          // SqlParameter sql3 = new SqlParameter("@sign", SignName);
         //  paraList.Add(sql3);
        //   PAGE_SQL = PAGE_SQL.AkFormat();
            var ds  = DBContext.QueryDataSet(PAGE_SQL, paraList.ToArray());
            List<string> res = new List<string>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string _txt = row[TXTFileName].ToString();
                res.Add(_txt);
            }
            return res;
       }

       public int AddText(string text, IUnitOfData unit)
       {
           string countSql = "SELECT COUNT({1}) FROM {0} WHERE  1=1  and  {1}  = @txt {2}".AkFormat(TableName,TXTFileName,AndFilterSql);

           int count = DBContext.QueryObject(countSql, new SqlParameter("@txt", text)).Value<int>();
           if (count == 0)
           {
              //// DBContext.QueryObject("");
              // string sql = "INSERT {0} (FID ,{1},CREATE_TIME) VALUES (@fid ,@txt,getdate()) ".AkFormat(TableName,TXTFileName);
              // SqlParameter sql1 = new SqlParameter("@txt", text);
              //// SqlParameter sql2 = new SqlParameter("@sign", SignName);
              // SqlParameter sql3 = new SqlParameter("@fid", DBContext.GetUniId());
              // return unit.RegisterSqlCommand(sql, sql1, sql3);
               InsertSql(text,unit);
           }
           return 0;

       }

       public abstract void InsertSql(string text ,IUnitOfData unit);




       public int RemoveText(string text, IUnitOfData unit)
       {
           string sql = "DELETE FROM {0}  WHERE {1} = @txt  {2} ".AkFormat(TableName,TXTFileName,AndFilterSql);
           SqlParameter sql1 = new SqlParameter("@txt", text);
          // SqlParameter sql2 = new SqlParameter("@sign", SignName);

           return unit.RegisterSqlCommand(sql, sql1);
       }
    }
}
