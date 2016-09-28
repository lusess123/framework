using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
   public class AtawSysMessageCommand
    {
       public string FID { get; set; }
       public DateTime CreateTime { get; set; }
       public string Data { get; set; }
       public string Sign { get; set; }
    }
}
