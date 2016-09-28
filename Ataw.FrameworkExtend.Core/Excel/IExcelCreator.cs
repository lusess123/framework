using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.FrameworkExtend.Core
{
    public interface IExcelCreator {
        List<ColumnItem> ColumnList { get; set; }
        DataTable Table { get; set; }

        string TableName { get; set; }

        string FileName { get; set; }

    }

    public class ColumnItem
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public ExcelColumnType ExcelColumnType { get; set; }
    }

    public enum ExcelColumnType
    {
        Text = 0 ,
        Image = 1

    }
    
    
}
