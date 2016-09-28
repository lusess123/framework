using System.Collections.Generic;
using System.Xml.Serialization;

namespace WorkFlowDesigner.Models
{
    public class Process
    {
        [XmlElement("uioperationconfig")]
        public UIOperationConfig UIOperationConfig { get; set; }
        [XmlArrayItem("nonuioperationconfig")]
        [XmlArray("nonuioperationconfigs")]
        public List<NonUIOperationConfig> NonUIOperationConfigs { get; set; }
    }
}