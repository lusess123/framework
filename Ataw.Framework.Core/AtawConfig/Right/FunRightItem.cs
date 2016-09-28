using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    public class FunRightItem : IRegName
    {
       public string Menu { get; set; }
       public string Name { get; set; }
       public bool IsAllow { get; set; }
       public string RegName 
       { 
           get {
               return string.Format(ObjectUtil.SysCulture , "{0}.{1}",Menu,Name);
           } 
       }
    }
}
