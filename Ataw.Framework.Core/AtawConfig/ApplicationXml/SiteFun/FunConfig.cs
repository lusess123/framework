using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
   public class FunConfig
    {

       public FunConfig()
       {
           Children = new List<FunConfig>();
       }
       [XmlElement("Fun")]
       public List<FunConfig> Children { get; set; }
       [XmlAttribute]
       public string Name { get; set; }
        [XmlAttribute]
       public string RightKey { get; set; }
        [XmlAttribute]
       public string Title { get; set; }
       public string RegName { get; set; }
       public string Param1 { get; set; }
       public string Param2 { get; set; }
       public string Param3 { get; set; }
    }
}
