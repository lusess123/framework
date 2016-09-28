using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class MacroConfig : BaseMacro
    {
      
        [XmlText]
        public string Value { get; set; }





        public override string ExeValue()
        {
            return Exe(Value);
        }
    }
}
