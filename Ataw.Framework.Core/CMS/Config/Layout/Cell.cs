using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ataw.Framework.Core.CMS
{
    public class Cell
    {
        [XmlAttribute]
        public int SpanLength { get; set; }
        [XmlAttribute]
        public int OffsetLength { get; set; }
        [XmlArrayItem(Type = typeof(ListItemControl), ElementName = "ListItem")]
        public List<BaseControl> Controls { get; set; }
        [XmlElement("Row")]
        public List<Row> Rows { get; set; }
    }
}
