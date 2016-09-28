using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class ScriptConfig
    {
        [XmlAttribute("Style")]
        public string InternalStyle { get; set; }

        [XmlIgnore]
        public PageStyle Style { get; set; }

        //[XmlText]
        //public string Content { get; set; }
        public string Path { get; set; }

        public string Function { get; set; }

    }
}
