
using System.Collections.Generic;
using System.Data;
namespace Ataw.Framework.Core
{
    public class ObjectData : IObjectData
    {
        //public ObjectData()
        //{
        //    ModefyColumns = new HashSet<string>();
        //}

        public HashSet<string> MODEFY_COLUMNS
        {
            get;
            set;
        }
        public DataRow Row
        {
            get;
            set;
        }

        public string BUTTON_RIGHT
        {
            get;
            set;
        }


    }

    public static class ObjectDataExtension
    {

        public static void SetDataRowValue(this ObjectData od, string colName, object val)
        {
            if (od.MODEFY_COLUMNS !=null &&!od.MODEFY_COLUMNS.Contains(colName))
            {
                od.MODEFY_COLUMNS.Add(colName);
            }

            if (!od.Row.Table.Columns.Contains(colName))
            {
                od.Row.Table.Columns.Add(colName);
            }
            od.Row[colName] = val;
        }
        public static object GetDataRowValue(this ObjectData od, string colName)
        {
            if (od.MODEFY_COLUMNS.Contains(colName))
            {
                if (od.Row.Table.Columns.Contains(colName))
                {
                    return od.Row[colName];
                }
            }
            return null;
        }
        public static string CreateInsertSql(this ObjectData od, string tableName, string keyName)
        {
            string cols = string.Join(",", od.MODEFY_COLUMNS);
            string sql = "INSERT INTO {0} ({1}) VALUES ({2})";
            List<string> vals = new List<string>();
            foreach (string col in od.MODEFY_COLUMNS)
            {
                string val = od.Row[col].ToString();
                if (col == keyName)
                    val =
                val = "'" + val + "'";
                vals.Add(val);
            }

            sql = string.Format(ObjectUtil.SysCulture, sql, tableName, cols, string.Join(",", vals));
            return sql;
        }

        public static string CreateUpdateSql(this ObjectData od, string tableName, string keyName)
        {
            string cols = string.Join(",", od.MODEFY_COLUMNS);
            string sql = "UPDATE {0} SET {1} WHERE {2} = '{3}' ";
            List<string> vals = new List<string>();
            string key = "";
            foreach (string col in od.MODEFY_COLUMNS)
            {
                string val = od.Row[col].ToString();
                if (col != keyName)
                {

                    val = col + "  = '" + val + "'";
                    vals.Add(val);
                }
                else
                    key = val;
            }

            sql = string.Format(ObjectUtil.SysCulture, sql, tableName, string.Join(",", vals), keyName, key);
            return sql;
        }
    }


    public interface IObjectData
    {
        HashSet<string> MODEFY_COLUMNS { get; set; }
        string BUTTON_RIGHT { get; set; }

    }

}
