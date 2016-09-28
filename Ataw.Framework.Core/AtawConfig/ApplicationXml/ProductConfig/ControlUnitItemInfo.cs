using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class ControlUnitItemInfo : IRegName
    {
        [XmlAttribute]
        public string FControlUnitID { get; set; }
        [XmlAttribute]
        public string Desc { get; set; }
        [XmlArrayItem("Product")]
        public List<ProductItemInfo> Products { get; set; }

        [XmlArrayItem("Config")]
        public List<ControlUnitConfigItemInfo> ControlUnitConfigs { get; set; }
        [XmlIgnore]
        public string RegName
        {
            get { return FControlUnitID; }
        }
    }
}
