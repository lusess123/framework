using System.Xml.Serialization;
namespace Ataw.Framework.Core
{
    public class ImageCut
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public int ImageSizeWidth { get; set; }
        [XmlAttribute]
        public int ImageSizeHeight { get; set; }
        [XmlAttribute]
        public int Quality { get; set; }

        public ImageCut() {
            Quality = 100;
        }

        public string GetNewFileTitle(string oldTitle)
        {
            return "{0}_{1}-{2}".AkFormat(oldTitle, ImageSizeWidth, ImageSizeHeight);
        }

        public static string GetNewFileTitle(string oldTitle, int width, int height)
        {
            return "{0}_{1}-{2}".AkFormat(oldTitle, width, height);
        }
    }
}
