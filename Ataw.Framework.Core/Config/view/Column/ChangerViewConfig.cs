using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
   public class ChangerViewConfig
    {
        public string Expression { get; set; }
        [XmlElement("Depend")]
        public List<string> DependColumns { get; set; }

        [XmlElement("Notify")]
        public List<string> NotifyColumns { get; set; }
    }
}
