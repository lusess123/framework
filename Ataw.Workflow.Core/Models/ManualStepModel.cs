using System.Collections.Generic;
using Ataw.Framework.Core;
using Ataw.Workflow.Core;

namespace Ataw.Workflow.Web
{
    public class ManualStepModel
    {
        public string Title
        {
            get;
            set;
        }

        public ShowKind Kind
        {
            get;
            set;
        }

        public string WorkflowInstId
        {
            get;
            set;
        }

        public UIOperationConfig UIOperation { get; set; }


        public RegNameList<NonUIOperationConfig> NonUIOperations
        {
            get;
            set;
        }

        public string WorkflowContent { get; set; }

        public List<ControlActionConfig> TabControlActions { get; set; }
        public List<ControlActionConfig> TileControlActions { get; set; }

        public StepView CurrentStep { get; set; }

        public StepView LastStep { get; set; }

        public StepView NextStep { get; set; }

        public List<StepView> OtherSteps { get; set; }

        // public List<StepView> RouteSteps { get; set; }

    }


}