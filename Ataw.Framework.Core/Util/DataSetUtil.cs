using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;

namespace Ataw.Framework.Core
{
    public static class DataSetUtil
    {

        public static SqlParameter SqlPar(this string name, object val, DbType? sqlType = null, int? size = null)
        {
           var res =   new SqlParameter("@" + name, val);
           if (sqlType != null)
           {
               res.DbType = (DbType)sqlType;
           }
           if (size != null)
           {
               res.Size = (int)size;
           }
           return res;
        }

        public static DataRow GetSingleRow(this DataSet ds)
        {
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return ds.Tables[0].Rows[0];
                    }
                }
            }
            return null;
        }

        public static DataSet SetDelDataSet(string formName,IEnumerable<string> ids)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable(formName + "_OPERATION");
            dt.Columns.Add("KeyValue");
            dt.Columns.Add("OperationName");

            ds.Tables.Add(dt);
            foreach (string str in ids)
            {
                DataRow row = dt.NewRow();
                row.BeginEdit();
                row["KeyValue"] = str;
                row["OperationName"] = "Delete";
                row.EndEdit();
                dt.Rows.Add(row);
            }
            return ds;
        }

        public static List<ObjectData> FillModel(DataTable dt, Type objType, HashSet<string> xmlColumns)
        {

            //List l = new List();

            List<ObjectData> l = new List<ObjectData>();
            bool isIObjectData = false;
            //var _objectData = model as ObjectData;
            //if (_objectData != null)
            //{
            //    isIObjectData = true;
            //    // var g = Attribute.GetCustomAttribute(model.GetType(), typeof(DataAccessAttribute)) as DataAccessAttribute;
            //    // g.ModefyColumns
            //}


            if (dt.Columns[0].ColumnName == "rowId")
            {
                dt.Columns.Remove("rowId");
            }



            foreach (DataRow dr in dt.Rows)
            {

                //isIObjectData
                Object model = Activator.CreateInstance(objType);
                var _objectData = model as ObjectData;
                if (_objectData != null)
                {
                    isIObjectData = true;
                    _objectData.MODEFY_COLUMNS = new HashSet<string>();
                    _objectData.Row = dr;
                }

                foreach (DataColumn dc in dr.Table.Columns)
                {

                    PropertyInfo pi = model.GetType().GetProperty(dc.ColumnName);
                    if (pi != null)
                    {
                        if (dr[dc.ColumnName] != DBNull.Value)
                        {
                            var ff = pi.ReflectedType;
                            var _obj = XmlUtil.GetValue(dr, pi.PropertyType, dr[dc.ColumnName].ToString());
                            pi.SetValue(model, _obj, null);
                            if (isIObjectData)
                            {
                                // Attribute.GetCustomAttribute(type, typeof(CodePlugAttribute)) as CodePlugAttribute
                                _objectData.MODEFY_COLUMNS.Add(dc.ColumnName);

                            }

                        }
                        else
                        {
                            pi.SetValue(model, null, null);
                        }
                    }
                    else
                    {
                        if (xmlColumns != null)
                        {
                            if (xmlColumns.Contains(dc.ColumnName) && dr[dc.ColumnName] != DBNull.Value)
                            {
                                _objectData.MODEFY_COLUMNS.Add(dc.ColumnName);
                            }
                        }
                    }

                }
                l.Add(_objectData);
            }

            return l;
        }

        public static IList<T> FillModel<T>(DataTable dt)
        {

            List<T> l = new List<T>();
            T model = default(T);

            bool isIObjectData = false;
            //var _objectData = model as ObjectData;
            //if (_objectData != null)
            //{
            //    isIObjectData = true;
            //    // var g = Attribute.GetCustomAttribute(model.GetType(), typeof(DataAccessAttribute)) as DataAccessAttribute;
            //    // g.ModefyColumns
            //}


            if (dt.Columns[0].ColumnName == "rowId")
            {
                dt.Columns.Remove("rowId");
            }



            foreach (DataRow dr in dt.Rows)
            {

                //isIObjectData
                model = Activator.CreateInstance<T>();
                var _objectData = model as ObjectData;
                if (_objectData != null)
                {
                    isIObjectData = true;
                    _objectData.MODEFY_COLUMNS = new HashSet<string>();
                }

                foreach (DataColumn dc in dr.Table.Columns)
                {

                    PropertyInfo pi = model.GetType().GetProperty(dc.ColumnName);
                    if (dr[dc.ColumnName] != DBNull.Value)
                    {
                        var ff = pi.ReflectedType;
                        var _obj = XmlUtil.GetValue(dr, pi.PropertyType, dr[dc.ColumnName].ToString());
                        pi.SetValue(model, _obj, null);
                        if (isIObjectData)
                        {
                            // Attribute.GetCustomAttribute(type, typeof(CodePlugAttribute)) as CodePlugAttribute
                            _objectData.MODEFY_COLUMNS.Add(dc.ColumnName);
                        }

                    }
                    else
                    {
                        pi.SetValue(model, null, null);
                    }

                }
                l.Add(model);
            }

            return l;
        }

        public static IList<T> FillModel<T>(DataSet ds)
        {

            return FillModel<T>(ds.Tables[0]);

        }

        /// <summary> 
        /// 将实体类转换成DataTable 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="i_objlist"></param> 
        /// <returns></returns> 
        public static DataTable Fill<T>(IList<T> objlist)
        {
            if (objlist == null || objlist.Count <= 0)
            {
                return null;
            }
            DataTable dt = new DataTable(typeof(T).Name);
            DataColumn column;
            DataRow row;

            System.Reflection.PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(BindingFlags.Public);

            foreach (T t in objlist)
            {
                if (t == null)
                {
                    continue;
                }

                row = dt.NewRow();

                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    System.Reflection.PropertyInfo pi = myPropertyInfo[i];

                    string name = pi.Name;

                    if (dt.Columns[name] == null)
                    {
                        column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                    }

                    row[name] = pi.GetValue(t, null);
                }

                dt.Rows.Add(row);
            }
            return dt;
        }


        /// <summary> 
        /// 将实体类转换成DataTable 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="i_objlist"></param> 
        /// <returns></returns> 
        public static DataTable ToDataTable(IList objlist, Type objType)
        {
            if (objlist == null || objlist.Count <= 0)
            {
                return null;
            }
            DataTable dt = new DataTable(objType.Name);
            DataColumn column;
            DataRow row;

            System.Reflection.PropertyInfo[] myPropertyInfo = objType.GetProperties();

            foreach (var t in objlist)
            {
                if (t == null)
                {
                    continue;
                }

                row = dt.NewRow();

                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    System.Reflection.PropertyInfo pi = myPropertyInfo[i];

                    string name = pi.Name;

                    if (dt.Columns[name] == null)
                    {

                        Type type = pi.PropertyType;
                        if (IsNullableType(type))
                            column = new DataColumn(name, type.GetGenericArguments()[0]);
                        else
                            column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                    }

                    row[name] = pi.GetValue(t, null) ?? DBNull.Value;
                }

                dt.Rows.Add(row);
            }
            return dt;
        }

        private static bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && theType.
              GetGenericTypeDefinition().Equals
              (typeof(Nullable<>)));
        }

        public static IEnumerable<TSource> SourceRowWhere<TSource, TResult>(this IEnumerable<TSource> source, DataRow row, string proName,
            Expression<Func<TSource, TResult>> selector, SysDataType sysDataType = SysDataType.None)
        {
            source = source.ToList();
            //datarow 里面的优先级最高
            //然后是 sysDataType
            //然后才是 实体类
            if (row.Table.Columns.Contains(proName))
            {

                //if (sysDataType == SysDataType.None)
                //{
                //    sysDataType = DataTypeUtil.ConvertTypeToSysDataType(typeof(TResult));
                //}
                switch (sysDataType)
                {
                    case SysDataType.None:
                        string postValue = row[proName].ToString();
                        if (postValue.IsEmpty())
                            return source;
                        string[] postValues = postValue.Split(',');
                        return source.Where(a =>
                        {
                            string beanValue = selector.Compile()(a).Value<string>() ?? "";
                            //if()
                            var _count = postValues.Where(b => beanValue.Equals(b)).Count();
                            return _count > 0;
                        }
                            );
                       // break;
                    case SysDataType.String:
                        string postValue2 = row[proName].ToString();
                        if (postValue2.IsEmpty())
                            return source;
                        string[] postValues2 =  postValue2.Split(',') ;
                        return source.Where(a =>
                        {
                            string beanValue = selector.Compile()(a).Value<string>() ?? "";
                            //if()
                           var _count =  postValues2.Where(b => beanValue.Contains(b)).Count();
                           return _count >  0 ;
                        }
                            );
                  //  break;
                    case SysDataType.Date:
                    case SysDataType.DateTime:
                        DateTime postTime = row[proName].Value<DateTime>();
                        if (postTime == default(DateTime))
                            return source;
                        if (proName.EndsWith("_END"))
                        {

                            return source.Where(a =>
                            {
                                DateTime beanTime = selector.Compile()(a).Value<DateTime>();
                                return beanTime < postTime.AddDays(1);
                            }
                                );
                        }
                        else
                        {
                            return source.Where(a =>
                            {
                                DateTime beanTime = selector.Compile()(a).Value<DateTime>();
                                return beanTime > postTime;
                            }
                               );
                        }
                    default:
                        int postInt = row[proName].Value<int>();
                        // TResult _where = selector.Compile();
                        if (postInt == 0)
                            return source;
                        return source.Where(a =>
                        {
                            int beanInt = selector.Compile()(a).Value<int>();
                            return postInt == beanInt;
                        }
                            );
                }
            }
            return source;
        }

        /// <summary>
        /// 拷贝一条DataRow，使用每个Column的Index进行拷贝，适用于表结构完全相同的DataRow
        /// </summary>
        /// <param name="src">源DataRow</param>
        /// <param name="dst">目标DataRow</param>
        /// <param name="start">起始的Column位置</param>
        /// <param name="end">终止的Column位置</param>
        /// <example>例如：
        /// <code>
        /// DataSetCopyUtil.CopyRow(rowSource, rowDest, 0, 5);
        /// </code>
        /// rowSource与rowDest的结构完全一致，将rowSource的第一个字段到第6个字段的内容依次拷贝到rowDest中
        /// </example>
        public static void CopyRow(DataRow src, DataRow dst, int start, int end)
        {
            dst.BeginEdit();
            try
            {
                for (int i = start; i <= end; ++i)
                    dst[i] = src[i];
            }
            finally
            {
                dst.EndEdit();
            }
        }

        /// <summary>
        /// 拷贝一条DataRow，使用每个Column的Index进行拷贝，适用于表结构完全相同的DataRow
        /// </summary>
        /// <param name="src">源行</param>
        /// <param name="dst">目的行</param>
        /// <example>例如：
        /// <code>
        /// DataSetCopyUtil.CopyRow(rowSource, rowDest);
        /// </code>
        /// rowSource与rowDest的结构完全一致，将rowSource的的所有字段内容依次拷贝到rowDest中
        /// </example>
        public static void CopyRow(DataRow src, DataRow dst)
        {
            CopyRow(src, dst, 0, src.Table.Columns.Count - 1);
        }

        /// <summary>
        /// 将DataRow src中指定的字段拷贝到DataRow dst中指定的字段，
        /// 源行与目标行的字段名可以不相同，单类型必须向下兼容		
        /// </summary>
        /// <param name="src">源DataRow</param>
        /// <param name="dst">目标DataRow</param>
        /// <param name="srcNames">源字段名序列</param>
        /// <param name="dstNames">目标字段名序列</param>
        /// <example>例如：
        /// <code>
        /// DataSetUtil.CopyRow(account, lead, new string[] {"ACC_NAME", "ACC_COMPANY"}, 
        ///     new string[] {"LD_NAME", "LD_COMPANY"});
        /// </code>		
        /// </example>
        public static void CopyRow(DataRow src, DataRow dst, string[] srcNames, string[] dstNames)
        {
            dst.BeginEdit();
            try
            {
                for (int i = 0; i < dstNames.Length; ++i)
                    dst[dstNames[i]] = src[srcNames[i]];
            }
            finally
            {
                dst.EndEdit();
            }
        }

        /// <summary>
        /// 拷贝一条DataRow，使用每个Column的Name进行拷贝，适用于字段名完全相同，但字段顺序不一致的表
        /// </summary>
        /// <param name="src">源DataRow</param>
        /// <param name="dst">目标DataRow</param>
        /// <param name="start">起始的Column位置</param>
        /// <param name="end">终止的Column位置</param>
        public static void CopyRowByName(DataRow src, DataRow dst, int start, int end)
        {
            DataColumnCollection cols = src.Table.Columns;
            dst.BeginEdit();
            try
            {
                for (int i = start; i <= end; ++i)
                {
                    DataColumn col = cols[i];
                    dst[col.ColumnName] = src[col];
                }
            }
            finally
            {
                dst.EndEdit();
            }
        }

        /// <summary>
        /// 拷贝一条DataRow，使用每个Column的Name进行拷贝，适用于字段名完全相同，但字段顺序不一致的表
        /// </summary>
        /// <param name="src">源DataRow</param>
        /// <param name="dst">目标DataRow</param>
        public static void CopyRowByName(DataRow src, DataRow dst)
        {
            CopyRowByName(src, dst, 0, dst.Table.Columns.Count - 1);
        }

        /// <summary>
        /// 拷贝DataTable，使用CopyRow方法复制每个Row
        /// </summary>
        /// <param name="src">源DataTable</param>
        /// <param name="dst">目标DataTable</param>
        public static void CopyDataTable(DataTable src, DataTable dst)
        {
            DataSetUtil.EmptyDataTable(dst);
            AppendDataTable(src, dst);
        }

        /// <summary>
        /// 向与源表结构完全相同的目的表拷贝数据
        /// </summary>
        /// <param name="src">源表</param>
        /// <param name="dst">目的表</param>
        public static void AppendDataTable(DataTable src, DataTable dst)
        {
            foreach (DataRow row in src.Rows)
            {
                DataRow newRow = dst.NewRow();
                CopyRow(row, newRow);
                dst.Rows.Add(newRow);
            }
        }

        /// <summary>
        /// 拷贝DataTable，使用CopyRowByName方法复制每个Row
        /// </summary>
        /// <param name="src">源DataTable</param>
        /// <param name="dst">目标DataTable</param>
        public static void CopyDataTableByName(DataTable src, DataTable dst)
        {
            DataSetUtil.EmptyDataTable(dst);
            AppendDataTableByName(src, dst);
        }

        /// <summary>
        /// 从源表向目的表拷贝数据，源表和目的表的字段名都一致，但顺序可能不一样
        /// </summary>
        /// <param name="src">源表</param>
        /// <param name="dst">目的表</param>
        public static void AppendDataTableByName(DataTable src, DataTable dst)
        {
            foreach (DataRow row in src.Rows)
            {
                DataRow newRow = dst.NewRow();
                CopyRowByName(row, newRow);
                dst.Rows.Add(newRow);
            }
        }

        /// <summary>
        /// 只拷贝表的部分字段到另外一个表，注意，这里要求两个表的字段名完全一样。拷贝前，先清空dst表
        /// </summary>
        /// <param name="src">源DataTable</param>
        /// <param name="dst">目标DataTable</param>
        /// <param name="fieldNames">需要拷贝的字段名称</param>
        public static void CopyPartDataTable(DataTable src, DataTable dst, params string[] fieldNames)
        {
            DataSetUtil.EmptyDataTable(dst);
            foreach (DataRow row in src.Rows)
            {
                DataRow newRow = dst.NewRow();
                newRow.BeginEdit();
                try
                {
                    foreach (string fieldName in fieldNames)
                        newRow[fieldName] = row[fieldName];
                }
                finally
                {
                    newRow.EndEdit();
                }
                dst.Rows.Add(newRow);
            }
        }

        /// <summary>
        /// 只拷贝表的部分字段到另外一个表，注意拷贝前，先清空dst表		
        /// </summary>
        /// <param name="src">源DataTable</param>
        /// <param name="dst">目标DataTable</param>
        /// <param name="srcNames">需要拷贝的字段名称</param>
        /// <param name="dstNames">目标表对应的字段名称</param>
        /// <example> 例如：
        /// <code>
        /// DataSetUtil.CopyPartDataTable(account, lead, new string[] {"ACC_NAME", "ACC_COMPANY"}, 
        ///     new string[] {"LD_NAME", "LD_COMPANY"});
        /// </code>
        /// </example>
        public static void CopyPartDataTable(DataTable src, DataTable dst, string[] srcNames, string[] dstNames)
        {
            DataSetUtil.EmptyDataTable(dst);
            foreach (DataRow row in src.Rows)
            {
                DataRow newRow = dst.NewRow();
                CopyRow(row, newRow, srcNames, dstNames);
                dst.Rows.Add(newRow);
            }
        }

        /// <summary>
        /// 清空DataTable的内容，使用Delete方法
        /// </summary>
        /// <param name="table">需要清空的表</param>
        public static void EmptyDataTable(DataTable table)
        {
            foreach (DataRow row in table.Rows)
                row.Delete();
        }

        public static void MegerDataTable(DataTable from, DataTable to, string keyColName)
        {
            foreach (DataColumn col in from.Columns)
            {
                string colName = col.ColumnName;
                if (!(to.Columns.Contains(colName)))
                {
                    to.Columns.Add(colName, col.DataType);
                }
            }

            foreach (DataRow row in from.Rows)
            {
                string key = row[keyColName].Value<string>();

                DataRow toRow = null;

                foreach (DataRow _row in to.Rows)
                {
                    if (_row[keyColName].Value<string>() == key && !key.IsEmpty())
                    {
                        toRow = _row;
                        break;
                    }
                }

                if (toRow == null)
                {
                    toRow = to.NewRow();
                    to.Rows.Add(toRow);
                }
                toRow.BeginEdit();
                foreach (DataColumn col in from.Columns)
                {
                    string colName = col.ColumnName;
                    toRow[colName] = row[colName];
                }
                toRow.EndEdit();
            }



        }

        private static IEnumerable<string> GetColumns(this DataTable dt)
        {
            List<string> strs = new List<string>();
            foreach (DataColumn col in dt.Columns)
            {
                strs.Add(col.ColumnName);
            }
            return strs;
        }
    }
}
