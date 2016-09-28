using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
   [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
   public  class EnumCodePlugAttribute : CodePlugAttribute
    {
       public EnumCodePlugAttribute(string regName)
           : base(regName)
       { 
       }
    }
}
