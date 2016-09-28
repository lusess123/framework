using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Ataw.Framework.Core;
using Ataw.Workflow.Core;

namespace WorkFlowDesigner.Models
{
    [Serializable]
    [XmlRoot("root")]
    public sealed class Root : FileXmlConfigBase
    {
        /// <summary>
        /// 不允许被外部调用
        /// </summary>
        public Root()
        {
            Nodes = new List<Node>();
        }
        [XmlAttribute("id")]
        public string ID { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("displayname")]
        public string DisplayName { get; set; }
        [XmlAttribute("priority")]
        public string Priority { get; set; }
        [XmlAttribute("issave")]
        public string IsSave { get; set; }
        [XmlAttribute("description")]
        public string Description { get; set; }
        [XmlAttribute("contentchoice")]
        public string ContentChoice { get; set; }
        [XmlAttribute("manualpagexml")]
        public string ManualPageXml { get; set; }
        [XmlArrayItem("controllaction")]
        [XmlArray("controllactions")]
        public List<ControllAction> ControllActions { get; set; }
        [XmlElement("node")]
        public List<Node> Nodes { get; set; }
        [XmlArrayItem("line")]
        [XmlArray("linelist")]
        public List<Line> LineList { get; set; }


        public WorkflowConfig ConvertToWfConfig()
        {
            return null;
        }
    }
}