using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq;

namespace Ataw.Framework.Core
{
    public  abstract class RssDataSource : BaseDataTableSource
    {
        public abstract string RssUrl
        {
            get;
            set;
        }

        public string RowName
        {
            get;
            set;
        }

        private void CreateColumns()
        {
 
        }

        protected override void InitializeDataSet()
        {
           // base.InitializeDataSet();
            DataSet ds = new DataSet();
            DataTable tb = new DataTable(RegName);
            foreach (var col in DataFormConfig.Columns)
            {
                tb.Columns.Add(col.Name);
            }
          //  tb.co
            ds.Tables.Add(tb);
            DataRow dr = tb.NewRow();

            var rssXDoc = XDocument.Load("http://feed.cnblogs.com/blog/sitehome/rss");
            //rssXDoc.Nodes().Select(
            //    a => { 
            //        a.E
            //    }
            //    );

        }
    }
}
