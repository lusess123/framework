using System.Xml.Serialization;

namespace Ataw.Framework.Core 
{
    public class WorkflowDefBuilderConfig
    {
        [XmlAttribute]
        public string Type { get; set; }
        [XmlAttribute]
        public string MapTo { get; set; }
        public string RegName { get; set; }
    }
}
