using System.Xml.Serialization;

namespace WorkFlowDesigner.Models
{
    public class BaseStringElement
    {
        [XmlAttribute("type")]
        public string Type { get; set; }
        [XmlText]
        public string Value { get; set; }
    }
}