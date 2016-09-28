
using System.Xml.Serialization;
namespace Ataw.Framework.Core
{
    public class FileUploadConfig
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public int MaxSize { get; set; }

        [XmlAttribute]
        public string Extensions { get; set; }

        [XmlAttribute]
        public string FilePath { get; set; }

        [XmlAttribute]
        public int ImageSizeWidth { get; set; }

        [XmlAttribute]
        public int ImageSizeHeight { get; set; }

        [XmlAttribute]
        public string ImageCutGroupName { get; set; }
       
    }
}
