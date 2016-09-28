using Ataw.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.FrameworkExtend.Core.Report
{
     [CodePlug("ColumnMethod", BaseClass = typeof(IMethod),
CreateDate = "2016-01-15", Author = "zhengyk", Description = "")]
    public class ColumnMethod : BaseSingleMethod
    {

        public override void Exe()
        {
           // throw new NotImplementedException();
            string xml = this.Params["xml"];
            PageViewUtil util = new PageViewUtil();
            var _pageView = util.getPageView(xml,"List");

            var __apcv = _pageView as AtawListPageConfigView;
            string _formName = __apcv.ListFormName;
            string _tableName = __apcv.Forms[_formName].TableName;

            var table = _pageView.Data.Tables[_tableName];
           // this.createFillDataSet(__apcv, __apcv.ListFormName);
            var columnList = new List<IColumnInfo>();
            __apcv.Forms[_formName].Columns.ForEach((col) =>
            {
                if (col.ControlType != ControlType.Hidden)
                {
                    columnList.Add(new ColumnInfo()
                    {
                        Name = col.Name,
                        DisplayName = col.DisplayName
                    });
                }

            });
            //_pageView.
            this.ResultObj = columnList;
        }
    }
}
