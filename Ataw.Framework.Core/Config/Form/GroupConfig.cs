using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class GroupConfig
    {
        public string IsDisabled { get; set; }

        [XmlIgnore]
        public PageStyle ShowPage { get; set; }

        [XmlElement("ShowPage")]
        public string InternalShowPage { get; set; }
    }
}
