using System.Xml.Serialization;
using System.Collections.Generic;
namespace Ataw.Framework.Core
{
    public class Business
    {
        [XmlAttribute("DisplayName")]
        public string DisplayName { get; set; }
        [XmlAttribute("IconClass")]
        public string IconClass { get; set; }
        [XmlAttribute("Href")]
        public string Href { get; set; }
        [XmlAttribute("RegName")]
        public string RegName { get; set; }
        [XmlAttribute("IsSpecial")]
        public string IsSpecial { get; set; }
		[XmlAttribute("Prompt")]
		public string Prompt { get; set; }
		[XmlAttribute("ValPrompt")]
		public string ValPrompt { get; set; }
        [XmlElement("Business")]
        public List<Business> Children { get; set; }
    }
}
