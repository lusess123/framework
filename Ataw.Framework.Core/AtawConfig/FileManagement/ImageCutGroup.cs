using System.Collections.Generic;
using System.Xml.Serialization;
namespace Ataw.Framework.Core
{
    public class ImageCutGroup
    {

        public ImageCutGroup()
        {
            ImageCuts = new List<ImageCut>();
        }

        [XmlAttribute]
        public string Name
        {
            get;
            set;
        }
        [XmlArrayItem("ImageCut")]
        public List<ImageCut> ImageCuts
        {
            get;
            set;
        }
    }
}
