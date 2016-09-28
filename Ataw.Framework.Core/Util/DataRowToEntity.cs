using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;

namespace Ataw.Framework.Core
{
    public static class DataRowToEntity
    {

        /// <summary>
        /// 将DataRow的值赋值到实体对象中.
        /// </summary>
        /// <typeparam name="T">实体对象烈性</typeparam>
        /// <param name="row">DataRow对象</param>
        /// <returns>赋值后的实体对象</returns>
        public static T ToEntity<T>(this DataRow row) where T : new()
        {
            if (row == null)
            {
                return default(T);
            }
            T result = new T();

            Type type = result.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (DataColumn column in row.Table.Columns)
            {
                var tmpPropQuery = from item in properties
                                   where item.Name == column.ColumnName
                                   select item;
                if (tmpPropQuery.Count() > 0)
                {
                    PropertyInfo tmpProp = tmpPropQuery.FirstOrDefault();
                    if (tmpProp != null && tmpProp.CanWrite)
                    {
                        object o = row[column.ColumnName] != DBNull.Value && row[column.ColumnName] != null ?
                            row[column.ColumnName] : null;
                        
                        Type tt = tmpProp.PropertyType;
                        object[] paras = { Convert.ChangeType(o, tt) };
                        tmpProp.ReflectedType.InvokeMember(tmpProp.Name, BindingFlags.SetProperty, null, result, paras);
                    }
                }
            }

            return result;
        }
    }
}
