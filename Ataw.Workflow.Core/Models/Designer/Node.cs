using System.Collections.Generic;
using System.Xml.Serialization;

namespace WorkFlowDesigner.Models
{
    public class Node
    {
        [XmlElement("objectindex")]
        public BaseIntElement ObjectIndex { get; set; }
        [XmlElement("id")]
        public BaseStringElement ID { get; set; }
        [XmlElement("typeindex")]
        public BaseIntElement TypeIndex { get; set; }
        [XmlElement("icon")]
        public BaseStringElement Icon { get; set; }
        [XmlElement("nodetype")]
        public BaseIntElement NodeType { get; set; }
        [XmlElement("text")]
        public BaseStringElement Text { get; set; }
        [XmlElement("nodetext")]
        public BaseStringElement NodeText { get; set; }
        [XmlElement("x")]
        public BaseIntElement X { get; set; }
        [XmlElement("y")]
        public BaseIntElement Y { get; set; }
        [XmlElement("width")]
        public BaseIntElement Width { get; set; }
        [XmlElement("height")]
        public BaseIntElement Height { get; set; }
        //[XmlElement("inputlist")]
        //public BaseElement InputList { get; set; }
        //[XmlElement("outputlist")]
        //public BaseElement OutputList { get; set; }
        [XmlElement("inputtype")]
        public BaseIntElement InputType { get; set; }
        [XmlElement("outputtype")]
        public BaseIntElement OutputType { get; set; }
        [XmlElement("zindex")]
        public BaseIntElement Zindex { get; set; }
        [XmlElement("name")]
        public BaseStringElement Name { get; set; }
        [XmlArrayItem("parentid")]
        [XmlArray("parentlist")]
        public List<Parent> ParentList { get; set; }
        [XmlArrayItem("childid")]
        [XmlArray("childlist")]
        public List<Child> ChildList { get; set; }
        [XmlElement("plugregname")]
        public BaseStringElement PlugRegName { get; set; } //自动步骤，结束步骤
        [XmlElement("creatorregname")]
        public BaseStringElement CreatorRegName { get; set; }//开始步骤

        //人工步骤
        [XmlElement("actorregname")]
        public BaseStringElement ActorRegName { get; set; }
        [XmlArrayItem("controllaction")]
        [XmlArray("controllactions")]
        public List<ControllAction> ControllActions { get; set; }
        [XmlElement("manualpagexml")]
        public BaseStringElement ManualPageXml { get; set; }
        [XmlElement("process")]
        public Process Process { get; set; }
        [XmlElement("contentchoice")]
        public BaseIntElement ContentChoice { get; set; }
    }
}