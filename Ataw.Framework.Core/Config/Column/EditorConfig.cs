using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class EditorConfig
    {
        public EditorConfig()
        {
            IsAll = false;
            IsHaveElementPath = false;
            Width = 0;
            Height = 0;
        }

        [XmlAttribute]
        public bool IsAll { get; set; }
        [XmlAttribute]
        public bool IsHaveElementPath { get; set; }
        [XmlAttribute]
        public int Width { get; set; }
        [XmlAttribute]
        public int Height { get; set; }
    }
}
