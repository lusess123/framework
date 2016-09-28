using System.Xml.Serialization;

namespace WorkFlowDesigner.Models
{
    public class ControllAction
    {
        [XmlAttribute("order")]
        public int Order { get; set; }
        [XmlAttribute("showkind")]
        public int ShowKind { get; set; }
        [XmlAttribute("title")]
        public string Title { get; set; }
        [XmlAttribute("areaname")]
        public string AreaName { get; set; }
        [XmlAttribute("controllname")]
        public string ControllName { get; set; }
        [XmlAttribute("actionname")]
        public string ActionName { get; set; }
    }
}