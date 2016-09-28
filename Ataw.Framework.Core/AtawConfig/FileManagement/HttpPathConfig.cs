using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class HttpPathConfig
    {
        [XmlAttribute]
        public string Value { get; set; }
    }
}
