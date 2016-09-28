using System.Xml.Serialization;

namespace WorkFlowDesigner.Models
{
    public class BaseIntElement
    {
        [XmlAttribute("type")]
        public string Type { get; set; }
        [XmlText]
        public int Value { get; set; }
    }
}