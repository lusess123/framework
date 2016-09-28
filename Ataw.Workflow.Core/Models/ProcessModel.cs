using Ataw.Framework.Core;
using Ataw.Workflow.Core;

namespace Ataw.Workflow.Web
{
    public class ProcessModel
    {
        public DetailModel DetailModel { get; set; }
        public UIOperationConfig UIOperation { get; set; }


        public RegNameList<NonUIOperationConfig> NonUIOperations
        {
            get;
            set;
        }
    }


}