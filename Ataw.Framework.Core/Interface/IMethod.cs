using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core
{
  public  interface IMethod
    {
      NameValueCollection Params { get; set; }

      int Order { get; set; }
      void Before();
      void Exe();
      void After(int resInt,IDictionary<string ,object> resultObj);

      object ResultObj { get; set; }

      
    }
}
