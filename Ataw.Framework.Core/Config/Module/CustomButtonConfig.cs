using System.Xml.Serialization;
namespace Ataw.Framework.Core
{
    public class CustomButtonConfig
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Text { get; set; }

        [XmlAttribute]
        public bool IsData { get; set; }
        [XmlAttribute]
        public string BtnCss { get; set; }
        [XmlAttribute]
        public string Icon { get; set; }
        /// <summary>
        /// 是否不允许批量操作
        /// </summary>
        [XmlAttribute]
        public bool Unbatchable { get; set; }

        public ClientConfig Client { get; set; }

        public ServerConfig Server { get; set; }

    }
}
