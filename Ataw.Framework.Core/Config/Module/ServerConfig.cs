using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
namespace Ataw.Framework.Core
{
    public class ServerConfig
    {
        [XmlAttribute]
        public bool IsSubmit { get; set; }

        public string PlugName { get; set; }

    }
}
