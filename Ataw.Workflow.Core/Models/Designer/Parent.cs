using System.Xml.Serialization;

namespace WorkFlowDesigner.Models
{
    public class Parent
    {
        [XmlText]
        public string Value { get; set; }
    }
}