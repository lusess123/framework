using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class ExpandDetailConfig
    {
        [XmlAttribute]
        public ExpandDetailType Type { get; set; }
        [XmlText]
        public string Value { get; set; }
    }
}
