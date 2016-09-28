using Ataw.Framework.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.FrameworkExtend.Core.Excel
{
     [CodePlug("default", BaseClass = typeof(IExcelCreator),
   CreateDate = "2016-01-15", Author = "zhengyk", Description = "")]
    public class DefaultExcelCreator : IExcelCreator
    {

        public DataTable Table
        {
            get;
            set;
        }

        public string TableName
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public DefaultExcelCreator()
        {
            ColumnList = new List<ColumnItem>();
            ColumnList.Add(new ColumnItem() { 
             Name = "FID",
             Title = "编号",
             Order = 0
            });

            ColumnList.Add(new ColumnItem()
            {
                Name = "NAME",
                Title = "名称",
                Order = 1

            });

           
            Table = new DataTable("default");
            Table.Columns.Add("FID");
            Table.Columns.Add("NAME");

            for (int i = 0; i < 100; i++)
            {
                DataRow row = Table.NewRow();
                row["FID"] = i.ToString();
                row["NAME"] = "name" + i.ToString();
                Table.Rows.Add(row);
            }

            FileName = "默认测试";

        }




        public List<ColumnItem> ColumnList
        {
            get;
            set;
        }
    }
}
