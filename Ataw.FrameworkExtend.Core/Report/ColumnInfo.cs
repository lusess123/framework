using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.FrameworkExtend.Core.Report
{
    public interface IColumnInfo
    {
        string DisplayName { get; set; }
        string Name { get; set; }

        int Order { get; set; }
    }
    public class ColumnInfo : IColumnInfo
    {

        public string DisplayName
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Order
        {
            get;
            set;
        }
    }
}
