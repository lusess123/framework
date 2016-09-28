using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ataw.Framework.Core.CMS
{
    public class Layout : FileXmlConfigBase, IReadXmlCallback
    {
        [XmlElement("Row")]
        public List<Row> Rows { get; set; }
        [XmlAttribute]
        public LayoutStyle Style { get; set; }
        void IReadXmlCallback.OnReadXml()
        { }
    }
}
