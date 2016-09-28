using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class OverrideColumnConfig : ColumnConfig
    {
        [XmlAttribute("Name")]
        public string BaseColumnName { get; set; }
    }
}
