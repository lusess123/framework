using System.Xml.Serialization;
using Ataw.Framework.Core;

namespace Ataw.Workflow.Core
{
    public class WorkflowModel : FileXmlConfigBase, IRegName
    {

        public WorkflowModel()
        {
            InputParams = new RegNameList<KeyValueItem>();
        }
        public string RegName { get; set; }

        //public string Plugin { get; set; }

        [XmlArrayItem("InputParam")]
        public RegNameList<KeyValueItem> InputParams { get; set; }
    }


}
