using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core.Model.Log
{
   public class LogInfo
    {
       public LogInfo()
       {
           this.TimePointList = new List<TimePoint>();
           this.DllInfoList = new List<DllInfo>();
       }
       public List<TimePoint> TimePointList { get; set; }
       public List<DllInfo> DllInfoList { get; set; }
    }
}
