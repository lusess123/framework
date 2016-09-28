using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
   [Serializable]
    public class PostModel : FileXmlConfigBase
    {
         public string ActionName { get; set; } 
         public string Url { get; set; }
         public string JsonTxt { get; set; }
    }
}
