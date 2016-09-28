using System.Xml.Serialization;
using System.Collections.Generic;
namespace Ataw.Framework.Core
{
    public class CustomScript
    {

        [XmlElement("RegName")]
        public string RegName { get; set; }

        [XmlElement("Path")]
        public string Path { get; set; }
       
    }
}
