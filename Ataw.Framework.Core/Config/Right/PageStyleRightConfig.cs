using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class PageStyleRightConfig
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public PageStyle PageStyle { get; set; }
    }
}
