using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    [XmlRoot("Database")]
    public class DatabaseConfigItem : IRegName
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public bool IsDefault { get; set; }
        public string ConnectionString { get; set; }

        [XmlIgnore]
        public string RegName
        {
            get { return Name; }
        }
    }
}
