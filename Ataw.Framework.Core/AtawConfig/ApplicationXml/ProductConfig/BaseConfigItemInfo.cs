using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class BaseConfigItemInfo : IRegName
    {
        [XmlAttribute]
        public string Key { get; set; }
        [XmlAttribute]
        public string Value { get; set; }
        [XmlAttribute]
        public string Default { get; set; }
        [XmlAttribute]
        public string Desc { get; set; }
        //单个
        public string BaseConfig { get; set; }

        [XmlIgnore]
        public string RegName
        {
            get { return Key; }
        }


    }
}
