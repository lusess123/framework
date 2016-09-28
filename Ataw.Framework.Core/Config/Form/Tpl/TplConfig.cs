using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
   public class TplConfig
    {
       [XmlAttribute]
       public string Name { get; set; }
       public string Item { get; set; }
       public string Header { get; set; }
       public string Footer { get; set; }
       public string Container { get; set; }
       [XmlAttribute]
       public PageStyle PageStyle { get; set; }
    }
}
