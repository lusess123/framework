using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core.Report
{
   public interface IReportSource
    {
        IEnumerable DataSourceValue { get; }
        string RdlcFileName { get; }

        string SourceName { get; }

        IDetailReportSource[] DetailListReportSources { get; }
    }

   public interface IDetailReportSource
   {
       IEnumerable createReportSource(string fid);
       string RdlcFileName { get; }
       string SourceName { get; }

       
   }
}
