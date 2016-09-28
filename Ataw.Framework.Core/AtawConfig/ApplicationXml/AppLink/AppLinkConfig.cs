using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    [Serializable]
   public class AppLinkConfig : FileXmlConfigBase
    {
        public AppLinkConfig()
        {
            Roots = new List<AppLink>();
        }
        [XmlElement("AppLink")]
        public List<AppLink> Roots { get; set; }
    }
}
