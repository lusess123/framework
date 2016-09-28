using System.Xml.Serialization;
namespace Ataw.Workflow.Core
{
    public class AutoStepConfig : StepConfig
    {
        // private ErrorConfig fError;

        protected AutoStepConfig()
        {
        }

        public AutoStepConfig(WorkflowConfig workflowConfig)
            : base(workflowConfig)
        {
        }

        public override StepType StepType
        {
            get
            {
                return StepType.Auto;
            }
        }
        [XmlIgnore]
        private ErrorConfig error;
        [XmlIgnore]
        public ErrorConfig Error
        {
            get
            {
                if (error == null)
                    error = new ErrorConfig();
                return error;
            }
            set
            {
                error = value;
            }
        }

        [XmlAttribute]
        public string PlugRegName { get; set; }

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
            return new AutoStep(workflow, this);
        }
    }
}
