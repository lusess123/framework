
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
namespace Ataw.Framework.Core
{
    public interface IUnitOfData : IDisposable, IRegName
    {
        // string RegName { get;  }
        int RegisterSqlCommand(string sql, params SqlParameter[] param);
        int RegisterStored(string storedName, params SqlParameter[] param);

        object QueryObject(string sqlString, params SqlParameter[] cmdParms);
        DataSet QueryDataSet(string sqlString, params SqlParameter[] cmdParms);

        int Submit();
        int EFSubmit();
        void addUnitOfData(IUnitOfData unitOfData);

        List<IUnitOfData> UnitOfDataList { get; set; }
        StringBuilder LogCommand(StringBuilder sb);
        DateTime Now { get; }

        string UnitSign { get; }
        string GetUniId();
        string GetSqlParamName(string param);
        int ExecuteSqlCommand(string sql, params SqlParameter[] param);
         int ExecuteStored(string storedName, params SqlParameter[] param);
        string ExecuteStoredRtn(string storedName, params SqlParameter[] param);

        bool IsChildUnit { get; set; }
        //
        List<Func<SqlTransaction, int>> ApplyFun { get; set; }
    }
}
