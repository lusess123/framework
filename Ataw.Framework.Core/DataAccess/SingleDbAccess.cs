using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace Ataw.Framework.Core
{

   public class SingleDbAccess
    {
       public const string PAGE_SQL = "select {0} from ( "
                                    + "select row_number() over(order by {1} ) rn,{0} from {2} WHERE 1=1 {3})"
                                    + "tb where rn >(@PageNo)*@pageSize and rn <=(@PageNo + 1)*@pageSize";

       public const string COUNT_SQL = "SELECT COUNT(*) FROM {0} WHERE 1=1 {1}";

       public string TableName { get; set; }
       public IEnumerable<string> SelectFields { get; set; }
       public List<SqlParameter> Params { get; set; }
       /// <summary>
       /// 类似于 USERID，=，USERID
       /// </summary>
      // public List<Tuple<string, string, string>> WhereSqlTupleList { get; set; }
       public string Where { get; set; }
       public Pagination Page {  get; set; }

       public string Sql { get; private set; }
       public string CountSql { get; private set; }
       public int Top { get; set; }

       public SingleDbAccess()
       {
           Params = new List<SqlParameter>();
       }

       public void BuilderSql()
       {
           InternalBuilderSql();
       }

       protected  virtual  void  InternalBuilderSql()
       {
           if (Top == 0)
           {
               Page.PageSize = 100;
           }
           if (Page == null)
           {
               Page = new Pagination();
               if (Page.PageSize == 0)
               {
                   Page.PageSize = Top;
               }
               Page.PageIndex = 0;
              
               Page.SortName = "UPDATE_TIME";
           }

           //Sql = "SELECT {0} FROM {1} WHERE 1=1 AND ( {2} )".AkFormat(string);
          string _selelct =  string.Join(",", SelectFields);
          string _sortName = Page.SortName.IsEmpty() ? " UPDATE_TIME " : Page.SortName;
          string _order = " {0} {1} ".AkFormat(_sortName,Page.IsASC ? " ASC ":" DESC ");

          Sql =  PAGE_SQL.AkFormat(_selelct, _order, TableName, Where);
          CountSql = COUNT_SQL.AkFormat(TableName,Where);
         
       }

       public   DataSet Query(IUnitOfData unitOfData)
       {
           int count = unitOfData.QueryObject(CountSql, Params.ToArray()).Value<int>() ;
           Page.TotalCount = count;
           Params.Add(new SqlParameter("@PageNo", SqlDbType.Int) { SqlValue = Page.PageIndex });
           Params.Add(new SqlParameter("@pageSize", SqlDbType.Int) { SqlValue = Page.PageSize });
          return unitOfData.QueryDataSet(Sql, Params.ToArray()); 
       }
    }
}
