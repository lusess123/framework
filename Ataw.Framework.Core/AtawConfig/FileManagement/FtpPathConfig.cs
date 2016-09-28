using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class FtpPathConfig
    {
        [XmlAttribute]
        public string Value { get; set; }
    }
}
