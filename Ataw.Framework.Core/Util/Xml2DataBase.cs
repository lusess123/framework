using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
namespace Ataw.Framework.Core
{
    public class Xml2DataBase
    {

        private List<string> tableNames;
        private List<SqlLogMessage> logMessages;
        public bool IsMigrations { get; private set; }

        public Xml2DataBase()
        {
            logMessages = new List<SqlLogMessage>();
            tableNames = new List<string>();
            string sql = "SELECT name FROM SYSOBJECTS WHERE XTYPE='U'";
            DataSet dataSet = AtawAppContext.Current.UnitOfData.QueryDataSet(sql);
            logMessages.Add(new SqlLogMessage { SqlStr = sql });
            DataRowCollection dataRows = dataSet.Tables[0].Rows;
            for (int i = 0; i < dataRows.Count; i++)
            {
                tableNames.Add(dataRows[i][0].ToString().Trim().ToUpper());
            }
        }
        private List<string> GetTableFields(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("tableName不能为空");
            }
            List<string> tableFields = new List<string>();
            string sql = "SELECT Name FROM SysColumns WHERE id=Object_Id(@tableName)";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@tableName",tableName)
            };
            DataSet dataSet = AtawAppContext.Current.UnitOfData.QueryDataSet(sql, sqlParameters);
            logMessages.Add(new SqlLogMessage { SqlStr = sql, Parameters = sqlParameters });
            DataRowCollection dataRows = dataSet.Tables[0].Rows;
            for (int i = 0; i < dataRows.Count; i++)
            {
                tableFields.Add(dataRows[i][0].ToString().Trim().ToUpper());
            }
            return tableFields;
        }

        private bool HaveTable(string tableName)
        {
            return tableNames.FirstOrDefault<string>(n => n == tableName.Trim().ToUpper()) != default(string);
        }

        public void Migrations(DataFormConfig dataFormConfig)
        {
            dataFormConfig.AddBaseColumns("BaseForm.xml");
            if (dataFormConfig == null) { return; }
            if (dataFormConfig.TableName.IsEmpty()) dataFormConfig.TableName = dataFormConfig.Name;
            if (dataFormConfig.TableName.ToUpper().IndexOf("VIEW") == 0) { return; }//若是试图，不执行任何操作
            if (HaveTable(dataFormConfig.TableName))
            {
                List<string> tableFields = GetTableFields(dataFormConfig.TableName);
                string sql = string.Empty;
                string commentSql = string.Empty;
                //SqlParameter[] sqlParameters;
                foreach (var column in dataFormConfig.Columns)
                {
                    if (column.Kind == ColumnKind.Data && column.SourceName.IsEmpty())
                    {
                        if (tableFields.FirstOrDefault<string>(n => n == column.Name.Trim().ToUpper()) == default(string))
                        {

                            sql = string.Format(ObjectUtil.SysCulture, "{0} ALTER TABLE [{1}] Add [{2}] {3}", sql, dataFormConfig.TableName, column.Name, ConvertToDataBaseType(column));
                            if (column.IsKey)
                            {
                                sql = sql + " PRIMARY KEY";
                            }
                            commentSql = string.Format(ObjectUtil.SysCulture, " {0} EXECUTE sp_addextendedproperty N'MS_Description', '{1}', N'user', N'dbo', N'table', N'{2}', N'column', N'{3}'", commentSql, column.DisplayName, dataFormConfig.TableName, column.Name);
                        }
                        //sqlParameters = new SqlParameter[]
                        //{
                        //    new SqlParameter("@tableName",dataFormConfig.TableName),
                        //    new SqlParameter("@fieldName",column.Name),
                        //    new SqlParameter("@fieldType",ConvertToDataBaseType(column.ControlType,column.Length))
                        //};
                    }
                }
                if (!string.IsNullOrEmpty(sql))
                {
                    AtawAppContext.Current.UnitOfData.RegisterSqlCommand(sql);
                    AtawAppContext.Current.UnitOfData.RegisterSqlCommand(commentSql);
                    this.IsMigrations = true;
                    logMessages.Add(new SqlLogMessage { SqlStr = sql });
                }
            }
            else
            {
                string commentSql = string.Empty;
                string sql = "CREATE TABLE [" + dataFormConfig.TableName + "] (";
                foreach (var column in dataFormConfig.Columns)
                {
                    if (column.Kind == ColumnKind.Data)
                    {
                        if (column.IsKey)
                        {
                            sql = string.Format(ObjectUtil.SysCulture, "{0} [{1}] {2} PRIMARY KEY,", sql, column.Name, ConvertToDataBaseType(column));
                        }
                        else
                        {
                            sql = string.Format(ObjectUtil.SysCulture, "{0} [{1}] {2},", sql, column.Name, ConvertToDataBaseType(column));
                        }
                        commentSql = string.Format(ObjectUtil.SysCulture, " {0} EXECUTE sp_addextendedproperty N'MS_Description', '{1}', N'user', N'dbo', N'table', N'{2}', N'column', N'{3}'", commentSql, column.DisplayName, dataFormConfig.TableName, column.Name);
                    }
                }
                sql = sql.Substring(0, sql.Length - 1) + ")";
                AtawAppContext.Current.UnitOfData.RegisterSqlCommand(sql);
                AtawAppContext.Current.UnitOfData.RegisterSqlCommand(commentSql);
                this.IsMigrations = true;
                logMessages.Add(new SqlLogMessage { SqlStr = sql });
            }
            //AtawAppContext.Current.UnitOfData.Submit();
        }
        public void Migrations(string fileName)
        {
            Migrations(ReadConfig(fileName));
        }
        public string GetLogMessage()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(AtawAppContext.Current.DefaultConnString);
            if (logMessages.Count > 0)
            {
                foreach (var logMessage in logMessages)
                {
                    sb.Append(Environment.NewLine);
                    sb.Append(logMessage.ToString());
                }
                return sb.ToString();
            }
            return string.Empty;
        }
        private DataFormConfig ReadConfig(string fileName)
        {
            string fpath = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH,
                "form", fileName);

            return XmlUtil.ReadFromFile<DataFormConfig>(fpath);
        }
        private string ConvertToDataBaseType(ColumnConfig column)
        {
            int length = column.Length == 0 ? 200 : column.Length;
            if (column.DataType != default(XmlDataType))
            {
                switch (column.DataType)
                {
                    case XmlDataType.String:
                        return "NVARCHAR(" + length + ")";
                    case XmlDataType.Binary:
                        return "VARBINARY(" + length + ")";
                    case XmlDataType.Bit:
                        return "BIT";
                    case XmlDataType.Byte:
                        return "IMAGE";
                    case XmlDataType.Date:
                        return "DATETIME";
                    case XmlDataType.DateTime:
                        return "DATETIME";
                    case XmlDataType.Decimal:
                        return "DECIMAL";
                    case XmlDataType.Double:
                        return "FLOAT";
                    case XmlDataType.Int:
                        return "INT";
                    case XmlDataType.Long:
                        return "BIGINT";
                    case XmlDataType.Money:
                        return "numeric(18,4)";
                    case XmlDataType.Short:
                        return "SMALLINT";
                    case XmlDataType.Text:
                        return "NTEXT";
                    default:
                        return "VARCHAR(200)";
                }
            }
            else if (column.ControlType != ControlType.None)
            {
                ControlType controlType = column.ControlType;
                if (controlType == ControlType.Radio || controlType == ControlType.Combo || controlType == ControlType.CheckBox)
                {
                    return "INT";
                }
                if (controlType == ControlType.Date || controlType == ControlType.DateTime || controlType == ControlType.DetailDate)
                {
                    return "DATETIME";
                }
                if (controlType == ControlType.TextArea || controlType == ControlType.MultiSelector || controlType == ControlType.FormMultiSelector)
                {
                    return "NTEXT";
                }
                if (controlType == ControlType.SingleFileUpload || controlType == ControlType.SingleImageUpload
                    || controlType == ControlType.MultiFileUpload || controlType == ControlType.MultiImageUpload || controlType == ControlType.ImageDetail)
                {
                    return "NTEXT";
                }
                else
                {
                    return "VARCHAR(" + length + ")";
                }
            }
            return "VARCHAR(" + length + ")";
        }


    }
    public class SqlLogMessage
    {
        public string ConnectionString { get; set; }
        public string SqlStr { get; set; }
        public SqlParameter[] Parameters { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append(SqlStr);
            sb.Append(Environment.NewLine);
            if (Parameters != null)
            {
                foreach (var par in Parameters)
                {
                    if (par != null && par.ParameterName != null && par.Value != null)
                        sb.AppendFormat("名称:{0} 类型：{1} 长度：{2} 值：{3}",
                            par.ParameterName,
                            par.SqlDbType.ToString(),
                            par.Size, par.Value.ToString());

                    sb.Append(Environment.NewLine);
                }
            }
            return sb.ToString();
        }
    }
}
