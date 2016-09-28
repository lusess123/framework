using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core 
{
    public class MessagesBuilderConfig
    {
        [XmlAttribute]
        public string Type { get; set; }
        [XmlAttribute]
        public string MapTo { get; set; }
        public string RegName { get; set; }
    }
}
