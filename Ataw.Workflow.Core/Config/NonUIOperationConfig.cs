using System.Data;
using System.Xml.Serialization;

namespace Ataw.Workflow.Core
{
    public sealed class NonUIOperationConfig : OperationConfig
    {
        [XmlAttribute]
        public bool NeedPrompt { get; set; }
    }
}
