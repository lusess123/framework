using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
   public interface IFunFetch
    {
       void Init(string regName,string rightKey,string param1,string param2,string param3);
       string RegName { get; set; }
       string RightKey { get; set; }
       string Param1 { get; set; }
       string Param2 { get; set; }
       string Param3 { get; set; }
       int Fetch();
    }
}
