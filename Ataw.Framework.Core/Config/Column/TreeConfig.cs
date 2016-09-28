
using System.Xml.Serialization;
namespace Ataw.Framework.Core
{
    public class TreeConfig
    {
        [XmlAttribute]
        public bool DisplayRoot { get; set; }
    }
}