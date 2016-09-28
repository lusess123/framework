using Aspose.Cells;
using Ataw.Framework.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.FrameworkExtend.Core
{
      [CodePlug("ExcelMethod", BaseClass = typeof(IMethod),
   CreateDate = "2016-01-15", Author = "zhengyk", Description = "")]
    public class ExcelMethod : BaseSingleMethod
    {
        public override void Exe()
        {
            //-------------
            AtawAppContext.Current.SetItem("querystring",this.Params);
            //throw new NotImplementedException();
            string _regname = this.Params["excel"];
            IExcelCreator _excel =   _regname.CodePlugIn<IExcelCreator>();
            Workbook wb = new Workbook();
            Worksheet ws = wb.Worksheets[0];
            ws.Cells.InsertColumn(0);

            _excel.ColumnList = _excel.ColumnList.OrderBy(a=>a.Order).ToList();


            for (int i = 0; i < _excel.ColumnList.Count; i++) {
                var _item = _excel.ColumnList[i];
                ws.Cells[0, i].PutValue(_item.Title);
            }

              
            for (int i = 0; i < _excel.Table.Rows.Count; i++)
            {
                DataRow row = _excel.Table.Rows[i];
                for (int j = 0; j < _excel.ColumnList.Count; j++)
                {
                    var _item = _excel.ColumnList[j];
                    ws.Cells[(i + 1), j].PutValue(row[_item.Name].ToString());
                }
               
            }
            ws.AutoFitColumns();
            DataTableToExcel.WorkbookToExcel(wb, _excel.FileName + DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + ".xls");
           
            //_excel.

        }

    }
}
