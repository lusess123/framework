using System.Xml.Serialization;

namespace WorkFlowDesigner.Models
{
    public class Line
    {
        [XmlAttribute("id")]
        public string ID { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("plugname")]
        public string PlugName { get; set; }
        [XmlText]
        public string Text { get; set; }
    }
}