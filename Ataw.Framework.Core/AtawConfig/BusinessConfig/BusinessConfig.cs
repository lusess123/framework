using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class BusinessConfig : FileXmlConfigBase
    {
        [XmlElement("Business")]
        public List<Business> Roots { get; set; }
    }
}
