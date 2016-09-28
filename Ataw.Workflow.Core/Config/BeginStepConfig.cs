
using System.Xml.Serialization;
namespace Ataw.Workflow.Core
{
    public class BeginStepConfig : StepConfig
    {
        protected BeginStepConfig()
        {
        }

        public BeginStepConfig(WorkflowConfig workflowConfig)
            : base(workflowConfig)
        {
        }

        public sealed override StepType StepType
        {
            get
            {
                return StepType.Begin;
            }
        }

        public sealed override string Name
        {
            get
            {
                return "_Begin";
            }
            set
            {
            }
        }

        public sealed override string DisplayName
        {
            get
            {
                return "开始";
            }

        }

        public sealed override bool HasInStep
        {
            get
            {
                return false;
            }
        }

        [XmlAttribute]
        public string CreatorRegName { get; set; }

        public override Step CreateStep(Workflow workflow)
        {
            return new BeginStep(workflow, this);
        }
    }
}
