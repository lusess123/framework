using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class KeyValueItem : BaseMacro ,IRegName
    {
        [XmlAttribute]
        public string Key { get; set; }
        [XmlAttribute]
        public string Value { get; set; }

        [XmlIgnore]
        public string RegName
        {
            get { return Key; }
        }

      

        public  override string ExeValue()
        {
            return Exe(Value);
        }
    }
}
