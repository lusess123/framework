using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    [XmlRoot("DataRoute")]
    public class RoutesInfo : IRegName
    {
        [XmlAttribute]
        public string Name { get; set; }

        public string ControlName { get; set; }
        public string ActionName { get; set; }
        public string AreaName { get; set; }
        public string NameSpace { get; set; }
        public string Param { get; set; }
        [XmlIgnore]
        public string RegName
        {
            get { return Name; }
        }
    }
}
