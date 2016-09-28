using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class ColumnRightConfig 
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlArrayItem("Column")]
        public List<OverrideColumnConfig> Override { get; set; }

        [XmlArrayItem("Column")]
        public List<ColumnConfig> Add { get; set; }

        [XmlArrayItem("Column")]
        public List<DeleteColumnConfig> Delete { get; set; }

    }
}
