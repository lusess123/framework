using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data.Entity.ModelConfiguration;
using System.Linq.Expressions;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Ataw.Framework.Core
{
    public class AtawDbContext : DbContext, IUnitOfData
    {
        private DateTime fNow;
        private bool fIsSubmit;

        public string RegName
        {
            get;
            set;
        }

        public string NameOrConnectionString { get; set; }

        #region 构造函数
        public AtawDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            NameOrConnectionString = nameOrConnectionString;
            fUnitSign = nameOrConnectionString;
            ApplyFun = new List<Func<SqlTransaction, int>>();
        }

        #endregion

        public virtual int ExecuteSqlCommand(string sql, params SqlParameter[] param)
        {
            return Database.ExecuteSqlCommand(sql, param);
        }

        #region 获取guid

        public string GetUniId()
        {
            return GetUniId("");
        }

        public string GetUniId(string tableName)
        {
            string result = "";
            SqlParameter pTableName = new SqlParameter("tabName", SqlDbType.VarChar, 50) { Value = tableName };
            SqlParameter pMaxID = new SqlParameter("MaxID", SqlDbType.VarChar, 50)
            {
                Direction = ParameterDirection.Output,
                Value = result
            };

            ExecuteSqlCommand(" EXECUTE GetMaxFID @tabName,  @MaxID  OUTPUT", pTableName, pMaxID);
            return pMaxID.Value.ToString();
        }
        public string AddError(string module, string mesg, string strack)
        {
            string result = "";
            SqlParameter moduleP = new SqlParameter("MODULE", SqlDbType.NVarChar, 50) { Value = module };
            SqlParameter mesgP = new SqlParameter("MESG", SqlDbType.NVarChar) { Value = mesg };
            SqlParameter strackP = new SqlParameter("STRACK", SqlDbType.NText) { Value = strack };
            SqlParameter fidP = new SqlParameter("FID", SqlDbType.NVarChar, 50)
            {
                Direction = ParameterDirection.Output,
                Value = result
            };

            ExecuteSqlCommand(" EXECUTE ATAW_SP_ADD_ERROR @MODULE,@MESG,@STRACK,@FID  OUTPUT", moduleP,
                mesgP, strackP, fidP);
            return fidP.Value.ToString();
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ObjectUtil.DisposeListObject(AtawDbContextList);
                ApplyFun = null;
            }
            base.Dispose(disposing);

        }

        #endregion

        #region 获取事务时间
        public DateTime Now
        {
            get
            {
                if (fNow.Value<DateTime>() == default(DateTime))
                {
                    fNow = DateTime.Now;
                }
                return fNow;
            }
        }
        #endregion

        #region 事务注册
        public List<SqlTrans> SqlTransList = new List<SqlTrans>();
        public List<IUnitOfData> AtawDbContextList = new List<IUnitOfData>();

        public int RegisterSqlCommand(string sql, params SqlParameter[] param)
        {
            SqlTrans trans = new SqlTrans()
            {
                sql = sql,
                param = param,
                CommandType = CommandType.Text
            };
            SqlTransList.Add(trans);
            return param == null ? 0 :param.Count();
        }
        #endregion

        public string GetSqlParamName(string param)
        {
            return "@" + param;
        }

        public int Submit()
        {
            CheckSubmit();
            return ADOSubmit();
        }

        public int EFSubmit()
        {
           // CheckSubmit();
            return SaveChanges();
        }

        private void CheckSubmit()
        {
            if (!fIsSubmit)
            {
                fIsSubmit = true;
            }
            else
            {
                throw new AtawException("注意 DbContext 只能被提交一次", this);
            }
        }

        public List<Func<SqlTransaction, int>> ApplyFun
        {
            get;
            set;
        }

        private void TransSubmit(SqlConnection conn, SqlTransaction sqlTran)
        {
        }

        public StringBuilder LogCommand(StringBuilder sb)
        {
            sb.AppendLine("_一个子工作单元");
            bool _islog = LogSaveAtawChanges();
            if (_islog || !IsChildUnit)
            {
                if ((CommandItems != null && CommandItems.Count > 0))
                {
                    CommandItems.CommandToString(sb);
                }
                if (SqlTransList != null && SqlTransList.Count > 0)
                {
                    SqlTransList.SqlTransToString(sb);
                }
            }
            
            return sb;
        }

        // public void 
        private bool IsSaveAtawChanges = false;

        private bool LogSaveAtawChanges()
        {
            try
            {
                if (!IsSaveAtawChanges)
                {
                    IsSaveAtawChanges = true;
                    SaveAtawChanges();
                }
                else
                    return false;//已经被记录过了
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                //string exx = string.Join("|",
                //    dbEx.EntityValidationErrors.Select(
                //    a => string.Join("-", a.ValidationErrors.Select(b => b.ErrorMessage),"-实体:",a.Entry.Entity.ToString(),"-状态",a.Entry.State.ToString())
                //    ),Environment.NewLine);
                //exx = exx + AtawAppContext.Current.FastJson.ToJSON(dbEx.EntityValidationErrors);
              string  exx = this.logDbEx(dbEx);
                AtawTrace.WriteFile(LogType.EfErrot, exx);
                throw dbEx;
            }
           
            return true;
        }


        private string logDbEx(DbEntityValidationException dbEx)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("以下是EF提交的异常信息：");
            sb.AppendLine("<ValidationErrors>");
            var _list = dbEx.EntityValidationErrors;
            foreach (DbEntityValidationResult _error in _list)
            {
                sb.AppendLine("<ValidationError>");
                sb.AppendLine("<Entry>{0}</Entry>".AkFormat(_error.Entry.Entity.ToJson()));
                sb.AppendLine("<State>{0}</State>".AkFormat(_error.Entry.State.ToString()));
                sb.AppendLine("<Errors>");
                foreach (var _er in _error.ValidationErrors) {
                    sb.AppendLine("<Error>");
                    sb.AppendLine("<Name>{0}<Name>".AkFormat(_er.PropertyName));
                    sb.AppendLine("<Mesg>{0}<Mesg>".AkFormat(_er.ErrorMessage));
                    sb.AppendLine("<Error>");
                }
                
                sb.AppendLine("</Errors>");
                sb.AppendLine("<ValidationError>");
            }
            sb.AppendLine("</ValidationErrors>");

            return sb.ToString();
        }

        public bool IsChildUnit
        {
            get;
            set;
        }

        public int ADOSubmit()
        {

            bool _haslog = LogSaveAtawChanges();//是否需要记录日志
            var con = Database.Connection as SqlConnection;
            //check
            int result = 0;

            if ((CommandItems != null && CommandItems.Count > 0) || (SqlTransList.IsNotEmpty()) || AtawDbContextList.IsNotEmpty() )
            {
                StringBuilder sb = new StringBuilder();
                if (_haslog)
                {
                   
                    sb.Append(con.ConnectionString);
                    sb.AppendLine();
                    LogCommand(sb);
                    foreach (IUnitOfData unit in AtawDbContextList)
                    {
                        unit.IsChildUnit = true;
                        unit.LogCommand(sb);
                    }
                    if (AtawAppContext.Current.Logger != null)
                        AtawAppContext.Current.Logger.Debug(sb.ToString());
                    AtawTrace.WriteFile(LogType.SubmitSql, sb.ToString());
                }
                using (con)
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    var trans = con.BeginTransaction();
                   
                    using (trans)
                    {
                        try
                        {

                            if (SqlTransList.Count > 0)
                            {
                                foreach (var sql in SqlTransList)
                                {
                                    result = result + SqlHelper.ExecuteNonQuery(trans, sql.CommandType,
                                        sql.sql, sql.param);
                                }
                            }
                            foreach (var com in CommandItems)
                            {
                                result = result + SqlHelper.ExecuteNonQuery(trans, com.CommandType,
                                    com.CommandText, com.Parameters.ToArray());
                            }

                            foreach (var context in AtawDbContextList)
                            {
                                result = result + context.Submit();
                            }
                            if (ApplyFun != null && ApplyFun.Count > 0)
                            {
                                foreach (var fun in ApplyFun)
                                {
                                    result = result + fun(null);
                                }
                            }
                            //if (!IsChildUnit)
                            //{
                            //    AtawTrace.WriteFile(LogType.SubmitSql, sb.ToString());
                            //}
                            trans.Commit();
                            
                        }
                        //catch (System.Data.Common.DbException wex)
                        //{
                        //    trans.Rollback();
                        //    throw wex;
                        //}
                        catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                        {
                            string exx = string.Join("|",
                                dbEx.EntityValidationErrors.Select(
                                a => string.Join("-", a.ValidationErrors.Select(b => b.ErrorMessage))
                                ));
                            AtawTrace.WriteFile(LogType.EfErrot, exx);
                            trans.Rollback();
                            throw dbEx;
                        }
                        catch (Exception ex)
                        {
                            AtawTrace.WriteFile(LogType.SqlSubmitError, sb.ToString());
                            trans.Rollback();
                            throw ex;

                        }
                        finally {
                          
                            
                        }
                       
                        //catch (SqlException sqlEx)
                        //{
                        //    trans.Rollback();
                        //    throw sqlEx;
                        //}
                    }
                }
            }
            
            return result;
        }

        public int EfSubmit()
        {
            try
            {
                //throw new NotImplementedException();
                //Submit();
                if (SqlTransList.Count > 0 || AtawDbContextList.Count > 0)
                {
                    //int result = 0;
                    //foreach (var sql in SqlTransList)
                    //{
                    //    result = result + ExecuteSqlCommand(sql.sql, sql.param);
                    //}
                    //result = result + SaveChanges();
                    //return result;
                    TransactionOptions option = new TransactionOptions();
                    option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, option))
                    {
                        // Database.Connection.Open();
                        int result = 0;

                        foreach (var sql in SqlTransList)
                        {
                            result = result + ExecuteSqlCommand(sql.sql, sql.param);
                        }

                        foreach (var context in AtawDbContextList)
                        {
                            result = result + context.Submit();
                        }
                        result = result + SaveChanges();
                        ts.Complete();
                        return result;
                    }
                    //var conn = Database.Connection;
                    //using (conn)
                    //{
                    //    int result = 0;
                    //    conn.Open();
                    //    var ts = Database.Connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    //    using (ts)
                    //    {
                    //        result = result + SaveChanges();
                    //        foreach (var sql in SqlTransList)
                    //        {
                    //            //SqlCommand cmd = new SqlCommand(sql.sql, conn,); ;
                    //            DbCommand command = conn.CreateCommand();
                    //            command.CommandText = sql.sql;
                    //            // command.Transaction = ts;
                    //            command.Parameters.AddRange(sql.param);
                    //            result = result + command.ExecuteNonQuery();
                    //        }

                    //        return result;
                    //    }
                    //}

                    // return 0;
                }
                else
                {
                    var res = SaveChanges();
                    return res;
                }
                //return 
            }
            catch (DbEntityValidationException entityEx)
            {
                // entityEx.EntityValidationErrors.SelectMany(a=>a.ValidationErrors.

                string exx = string.Join("|",
                     entityEx.EntityValidationErrors.Select(
                     a => string.Join("-", a.ValidationErrors.Select(b => b.ErrorMessage))
                     ));

                throw new Exception(exx);
            }
            catch (Exception ex)
            {
                throw new AtawException("EF 提交异常", ex, this);
            }
        }

        #region 存储过程
        #region 设置参数
        public SqlParameter setOutputParameter(string paramName, string value, int size)
        {
            SqlParameter param = new SqlParameter();
            param.Direction = ParameterDirection.Output;
            param.ParameterName = paramName;
            param.SqlDbType = SqlDbType.VarChar;
            if (size > 0) { param.Size = size; }
            if (!string.IsNullOrEmpty(value))
                param.Value = value;
            return param;
        }
        public SqlParameter SetParameter(string paramName, string val, int size)
        {
            SqlParameter param = new SqlParameter();
            param.Direction = ParameterDirection.Input;
            param.ParameterName = paramName;
            param.SqlDbType = SqlDbType.VarChar;
            param.Size = size;
            param.Value = val;
            return param;
        }
        public SqlParameter SetParameter(string paramName, bool val)
        {
            SqlParameter param = new SqlParameter();
            param.Direction = ParameterDirection.Input;
            param.ParameterName = paramName;
            param.SqlDbType = SqlDbType.TinyInt;
            param.Value = val ? 1 : 0;

            return param;
        }
        public SqlParameter SetParameter(string paramName, DateTime _val)
        {
            SqlParameter param = new SqlParameter();
            param.Direction = ParameterDirection.Input;
            param.ParameterName = paramName;
            param.SqlDbType = SqlDbType.DateTime;
            param.Value = _val;
            return param;
        }
        public SqlParameter SetParameter(string paramName, int val)
        {
            SqlParameter param = new SqlParameter();
            param.Direction = ParameterDirection.Input;
            param.ParameterName = paramName;
            param.SqlDbType = SqlDbType.Int;
            param.Value = val;
            return param;
        }
        public SqlParameter SetParameter(string paramName, decimal val)
        {
            SqlParameter param = new SqlParameter();
            param.Direction = ParameterDirection.Input;
            param.ParameterName = paramName;
            param.SqlDbType = SqlDbType.Decimal;
            param.Precision = 18;
            param.Scale = 4;
            param.Value = val;
            return param;
        }
        #endregion

        public virtual int ExecuteStored(string storedName, params SqlParameter[] param)
        {
            return ExecuteSqlCommand(" EXECUTE " + storedName, param);
        }
        public int RegisterStored(string storedName, params SqlParameter[] param)
        {
            SqlTrans trans = new SqlTrans()
            {
                sql = storedName,
                param = param,
                CommandType = CommandType.StoredProcedure
            };
            SqlTransList.Add(trans);
            return param.Count();
        }

        public string ExecuteStoredRtn(string storedName, params SqlParameter[] param)
        {
            //string result = "";
            //SqlParameter pTableName = new SqlParameter("tabName", SqlDbType.VarChar, 50) { Value = tableName };
            //SqlParameter pMaxID = new SqlParameter("MaxID", SqlDbType.VarChar, 50)
            //{
            //    Direction = ParameterDirection.Output,
            //    Value = result
            //};

            //ExecuteSqlCommand(" EXECUTE " + storedName, param);
            //return pMaxID.Value.ToString();
            return "";
        }

        #endregion
        public object QueryObject(string sqlString, params SqlParameter[] cmdParms)

        {
            
            SqlCommand cmd = new SqlCommand();
            var conn = this.Database.Connection as SqlConnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn as SqlConnection;
            cmd.CommandText = sqlString;

            cmd.CommandType = CommandType.Text;



            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }


            StringBuilder sb = new StringBuilder();
            DBUtil.DbCommandToString(cmd, sb);
            AtawTrace.WriteFile(LogType.QueryObject, sb.ToString());

            var res = cmd.ExecuteScalar();
            if (conn != null)
                conn.Close();
            return res;
        }
        public DataSet QueryDataSet(string sqlString, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            var conn = this.Database.Connection as SqlConnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn as SqlConnection;
            cmd.CommandText = sqlString;

            cmd.CommandType = CommandType.Text;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
            StringBuilder sb = new StringBuilder();
            DBUtil.DbCommandToString(cmd, sb);
            AtawTrace.WriteFile(LogType.PageQuerySql, sb.ToString());
             DataSet ds = new DataSet();
             try
             {
                 using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                 {


                     da.Fill(ds);
                     cmd.Parameters.Clear();


                 }
             }
             catch (Exception ex)
             {
                 AtawTrace.WriteFile(LogType.SqlQueryError, sb.ToString());
                 throw ex;
             }
             finally
             {
                 if (conn != null)
                     conn.Close();
             }

                return ds;
            

        }
        #region 获取参数
        public SqlParameter CreateParameter(string pamName, DbType dbType, object value)
        {
            return new SqlParameter("@" + pamName, dbType) { Value = value };
        }
        #endregion


        public void addUnitOfData(IUnitOfData unitOfData)
        {
            //throw new NotImplementedException();
            this.AtawDbContextList.Add(unitOfData);
        }

        private string fUnitSign;

        public string UnitSign
        {
            get
            {
                return fUnitSign;
            }
        }

        protected void ModelAdd<TEntityType>(DbModelBuilder modelBuilder)where TEntityType : class
        {
            modelBuilder.Configurations.Add(new EntityTypeConfiguration<TEntityType>());
        }
        protected void ModelKeyAdd<TEntityType, TKey>(DbModelBuilder modelBuilder, Expression<Func<TEntityType, TKey>> keyExpression) where TEntityType : class
        {
            modelBuilder.Configurations.Add(
                new EntityTypeConfiguration<TEntityType>()
                .HasKey(keyExpression));
        }

        protected void ModelKeyAddType(DbModelBuilder modelBuilder,Type eType) 
        {
            var parameter = Build(eType);
            Func<EntityTypeConfiguration<object>, ConfigurationRegistrar> func = modelBuilder.Configurations.Add<object>;
            var method = func.Method.GetGenericMethodDefinition().MakeGenericMethod(eType);
            method.Invoke(modelBuilder.Configurations, new object[] { parameter });

        }


        public static object Build(Type innerType)
        {
            var type = typeof(EntityTypeConfiguration<>);
            type = type.MakeGenericType(innerType);
            var result = Activator.CreateInstance(type);
            var parameter = Expression.Parameter(innerType, "foo");
            var lambda = Expression.Lambda(Expression.Property(parameter, "FID"), new[] { parameter });
            var method = type.GetMethod("HasKey").MakeGenericMethod(innerType.GetProperty("FID").PropertyType);
            return method.Invoke(result, new object[] { lambda });
        }



        public List<IUnitOfData> UnitOfDataList
        {
            get
            {
               return this.AtawDbContextList;
            }
            set
            {
                this.AtawDbContextList = value;
            }
        }
    }
}
