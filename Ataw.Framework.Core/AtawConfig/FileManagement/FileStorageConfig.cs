
using System.Xml.Serialization;
namespace Ataw.Framework.Core
{
    public class FileStorageConfig
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public int FileRootID { get; set; }

        [XmlAttribute]
        public string FileNameFormat { get; set; }

        [XmlAttribute]
        public string FilePathFormat { get; set; }
    }
}
