using System;
using System.Xml.Serialization;


namespace Ataw.Framework.Core
{
    [XmlRoot("Products")]
    [Serializable()]
    public class ProductItemInfo : IRegName
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Key { get; set; }
        [XmlAttribute]
        public string Default { get; set; }
        [XmlAttribute]
        public string Value { get; set; }
        [XmlAttribute]
        public string Desc { get; set; }

        [XmlIgnore]
        public string RegName
        {
            get { return Name; }
        }
    }
}
