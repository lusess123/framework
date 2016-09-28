using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class FunctionRightConfig
    {
        [XmlArrayItem("RightUnit")]
        public List<RightUnitConfig> RightUnits { get; set; }

        [XmlArrayItem("PageStyleRight")]
        public List<PageStyleRightConfig> PageStyleRights { get; set; }

        [XmlArrayItem("ButtonRight")]
        public List<ButtonRightConfig> ButtonRights { get; set; }

    }
}
