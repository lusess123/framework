using System.Data;
using System.Linq;

namespace Ataw.Framework.Core
{
    public class AtawBaseMultiOptionCreator : AtawDecodeOptionCreator
    {

        protected override void PreDecode(string tableName, string codeTableName, string colName, DataSet ds)
        {
            string insertTableName = tableName + "_INSERT";
            string codeColumn = colName + "_CODEINDEX";
            string text = "";
            if (!ds.Tables.Contains(codeTableName))
            {
                AddCodeTable(codeTableName);
            }
            switch (PageStyle)
            {
                case PageStyle.Insert:
                    if (ds.Tables.Contains(insertTableName))
                    {
                        //新增时，该多选控件存在默认值时，才需要新增索引列
                        if (!ds.Tables[insertTableName].Columns.Contains(codeColumn) &&
                            ds.Tables[insertTableName].Columns.Contains(colName))
                        {
                            ds.Tables[insertTableName].Columns.Add(codeColumn, typeof(int[]));
                        }
                        if (ds.Tables[insertTableName].Columns.Contains(codeColumn))
                            ds.Tables[insertTableName].Rows[0][codeColumn] = Decode(this.Config.DefaultValueStr, codeTableName, out text);
                    }
                    break;
                case PageStyle.List:
                case PageStyle.Detail:
                //if (ds.Tables.Contains(formTableName))
                //{
                //    foreach (DataRow row in ds.Tables[formTableName].Rows)
                //    {
                //        string val = row[colName].ToString();
                //        Decode(val, codeTableName, out text);
                //        //if (!text.IsEmpty())
                //        //{
                //        //    //if (ds.Tables[formTableName].Columns[colName].DataType != typeof(string))
                //        //    //{
                //        //    //    ds.Tables[formTableName].Columns[colName].DataType = typeof(string);
                //        //    //}
                //        //    row[colName] = text;
                //        //}
                //    }
                //}
                //break;
                case PageStyle.Update:
                    if (ds.Tables.Contains(tableName) && ds.Tables[tableName].Columns.Contains(colName))
                    {
                        if (!ds.Tables[tableName].Columns.Contains(codeColumn))
                            ds.Tables[tableName].Columns.Add(codeColumn, typeof(int[]));
                        foreach (DataRow row in ds.Tables[tableName].Rows)
                        {
                            string val = row[colName].ToString();
                            row[codeColumn] = Decode(val, codeTableName, out text);
                        }
                    }
                    break;
            }
        }

        private void AddCodeTable(string tableName)
        {
            var newTable = new DataTable(tableName);
            newTable.Columns.Add("CODE_VALUE");
            newTable.Columns.Add("CODE_TEXT");
            newTable.Columns.Add("CODE_INDEX", typeof(int));
            newTable.PrimaryKey = new DataColumn[] { newTable.Columns["CODE_VALUE"] };
            this.PageView.Data.Tables.Add(newTable);
            var codeTable = tableName.SingletonByPage<CodeTable<CodeDataModel>>();
            var codeDataList = codeTable.FillAllData(null);
            int i = 0;
            var _list = codeDataList.ToList();
            _list.ForEach(a =>
            {
                var row = newTable.NewRow();
                row.BeginEdit();
                row["CODE_VALUE"] = a.CODE_VALUE;
                row["CODE_TEXT"] = a.CODE_TEXT;
                row["CODE_INDEX"] = i++;
                row.EndEdit();
                newTable.Rows.Add(row);
            });
        }

        /// <summary>
        ///多选控件(包括单选)CodeTable解码,返回CodeValue在Codetable中的索引值数组
        /// </summary>
        /// <param name="val">需解码的value值</param>
        /// <param name="tableName">codeTable表名</param>
        /// <returns></returns>
        protected int[] Decode(string val, string tableName, out string text)
        {
            text = "";
            if (!val.IsEmpty())
            {
                var valArr = val.Split(',');
                var result = new int[valArr.Length];
                int i = 0;
                var dataText = "";
                valArr.ToList().ForEach(a =>
                    {
                        var dt = this.PageView.Data.Tables[tableName];
                        if (dt != null && dt.Rows.Count > 0 && dt.PrimaryKey.Count() > 0)
                        {
                            var dr = dt.Rows.Find(a.Replace("\"", ""));//这里需要把引号替换掉，是因为复选框保存的时候每个值都加了引号
                            if (dr != null)
                            {
                                result[i++] = dr["CODE_INDEX"].Value<int>();
                                dataText = dataText + dr["CODE_TEXT"].ToString() + ",";
                            }
                            else
                                result[i++] = -1;
                        }
                    });
                if (!dataText.IsEmpty())
                    text = dataText.Remove(dataText.Length - 1);
                return result;
            }
            return new int[] { -1 };
        }
    }
}
