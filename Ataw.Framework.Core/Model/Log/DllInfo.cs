using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core.Model.Log
{
  public  class DllInfo
    {
      public string Name { get; set; }
      public string Error { get; set; }
      public List<ClassInfo> ClassInfoList { get; set; }
    }
}
