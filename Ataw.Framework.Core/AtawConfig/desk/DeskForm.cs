using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
   public class DeskForm
    {
       public string FormName { get; set; }

       public string FormDisplayName { get; set; }

       public bool IsDisplay { get; set; }

       public int Order { get; set; }

       public bool IsAuthorized { get; set; }
    }
}
