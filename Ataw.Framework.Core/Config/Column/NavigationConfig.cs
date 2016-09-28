using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class NavigationConfig
    {
        [XmlAttribute]
        public bool IsAvailable { get; set; }
        [XmlAttribute]
        public bool IsRefrech { get; set; }
        [XmlAttribute]
        public bool IsExpand { get; set; }
        public ControlType ControlType { get; set; }
        public string RegName { get; set; }

    }
}
