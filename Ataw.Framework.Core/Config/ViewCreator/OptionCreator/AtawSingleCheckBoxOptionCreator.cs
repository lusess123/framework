
using System.Data;
namespace Ataw.Framework.Core
{
    [CodePlug("SingleCheckBox", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-12-24", Author = "sj", Description = "SingleCheckBox控件参数创建插件")]
    public class AtawSingleCheckBoxOptionCreator : AtawBaseOptionCreator
    {
        private SingleCheckBoxOptions fSingleCheckBoxOptions;

        public AtawSingleCheckBoxOptionCreator()
        {
            fSingleCheckBoxOptions = new SingleCheckBoxOptions();
            BaseOptions = fSingleCheckBoxOptions;
        }

        public override BaseOptions Create()
        {
            var options = base.Create();
            string colName = this.Config.Name;
            string tableName = this.FormView.TableName;
            var ds = this.PageView.Data;
            switch (PageStyle)
            {
                case PageStyle.Insert:
                    break;
                case PageStyle.List:
                case PageStyle.Detail:
                    if (ds.Tables.Contains(tableName) && ds.Tables[tableName].Columns.Contains(colName))
                    {
                        DataTable dtClone = ds.Tables[tableName].Clone();
                        dtClone.Columns[colName].DataType = typeof(string);
                        //复制数据到克隆的datatable里
                        try
                        {
                            foreach (DataRow dr in ds.Tables[tableName].Rows)
                            {
                                dtClone.ImportRow(dr);
                            }
                        }
                        catch { }
                        ds.Tables.Remove(tableName);
                        ds.Tables.Add(dtClone);
                        foreach (DataRow row in ds.Tables[tableName].Rows)
                        {
                            if (row[colName].ToString() == "")
                            {
                                row[colName] = "×";
                            }
                            else
                            {

                                if (row[colName].Value<bool>() || row[colName].ToString() == "1")
                                    row[colName] = "√";
                                else
                                    row[colName] = "×";
                            }
                        }
                    }
                    break;
                case PageStyle.Update:
                    break;
            }
            return options;
        }
    }
}
