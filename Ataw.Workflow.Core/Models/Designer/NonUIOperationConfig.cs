using System.Xml.Serialization;

namespace WorkFlowDesigner.Models
{

    public class NonUIOperationConfig
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("displayname")]
        public string DisplayName { get; set; }
        [XmlAttribute("buttoncaption")]
        public string ButtonCaption { get; set; }
        [XmlAttribute("plugin")]
        public string Plugin { get; set; }
        [XmlAttribute("needprompt")]
        public string NeedPrompt { get; set; }
    }
}