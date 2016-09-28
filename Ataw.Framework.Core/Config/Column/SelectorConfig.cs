
using System.Xml.Serialization;
namespace Ataw.Framework.Core
{
    public class SelectorConfig
    {
        //public string RegName { get; set; }
        public string DataText { get; set; }
        [XmlAttribute]
        public bool Descendant { get; set; }

        public string ModuleXml { get; set; }
    }
}
