using System.Xml.Serialization;
using Ataw.Framework.Core;
namespace Ataw.Workflow.Core
{
    public sealed class ConnectionConfig : IRegName
    {
        public ConnectionConfig()
        {
        }

        [XmlAttribute]
        public string Name { get;  set; }

        [XmlAttribute]
        public string DisplayName { get; set; }

        [XmlAttribute]
        public string NextStepName { get; set; }

        public string PlugName { get; set; }

        [XmlIgnore]
        public string RegName
        {
            get { return Name; }
        }
    }
}
