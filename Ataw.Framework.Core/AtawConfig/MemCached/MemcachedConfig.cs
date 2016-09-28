using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class MemcachedConfig
    {
        public MemcachedConfig()
        {
            ServerList = new List<string>();
        }
        [XmlArrayItem("Server")]
        public List<string> ServerList { get; set; }
        [XmlAttribute("Init")]
        public int Init { get; set; }
        [XmlAttribute("Min")]
        public int Min { get; set; }
        [XmlAttribute("Max")]
        public int Max { get; set; }

    }
}
