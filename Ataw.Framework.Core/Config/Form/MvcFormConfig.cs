using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class MvcFormConfig : BaseFormConfig
    {

        public RoutesInfo DataRoute { get; set; }
    }


    public class BaseFormConfig
    {

        [XmlAttribute]
        public string Title { get; set; }

        /// <summary>
        /// 必填的
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public ShowKind ShowKind { get; set; }

        [XmlAttribute]
        public int ShowType { get; set; }

        [XmlAttribute]
        public string Order { get; set; }

        public bool VerticalTab { get; set; }

        public string AfterInitFunName { get; set; }
        [XmlAttribute]
        public string Width { get; set; }

    }
}
