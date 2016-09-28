using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class ButtonRightConfig
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string ButtonName { get; set; }
    }
}
