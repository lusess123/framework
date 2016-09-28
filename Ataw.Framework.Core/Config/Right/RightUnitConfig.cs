using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class RightUnitConfig
    {
        [XmlAttribute]
        public string Name { get; set; }

        public RightType RightType { get; set; }

        public string RegName { get; set; }

        public string RightUnitExe()
        {
            if (RightType == RightType.MvcFilter)
            {
                var type = RightUtil.RightVerification(RegName);
                return type.ToString();
            }
            else
            {
                return "";
            }
        }
    }
}
