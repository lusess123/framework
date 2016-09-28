using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ataw.Framework.Core.CMS
{
    public class Row
    {
        [XmlElement("Cell")]
        public List<Cell> Cells { get; set; }
    }
}
