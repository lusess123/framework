

namespace Ataw.Workflow.Core
{
    public class MergeStepConfig : StepConfig
    {
        //  private ErrorConfig fError;

        public MergeStepConfig()
        {
        }

        public MergeStepConfig(WorkflowConfig workflowConfig)
            : base(workflowConfig)
        {
        }

        public override StepType StepType
        {
            get
            {
                return StepType.Merge;
            }
        }

        //[DynamicXmlElement(MergerConfigFactory.REG_NAME)]
        //public IConfigCreator<IMerger> Merger { get; set; }

        //[ObjectXmlElement]
        //public ErrorConfig Error
        //{
        //    get
        //    {
        //        if (fError == null)
        //            fError = new ErrorConfig();
        //        return fError;
        //    }
        //    public set
        //    {
        //        fError = value;
        //    }
        //}

        public override Step CreateStep(Workflow workflow)
        {
            return new MergeStep(workflow, this);
        }
    }
}
