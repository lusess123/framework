using System.Xml.Serialization;
using Ataw.Framework.Core;

namespace Ataw.Workflow.Core
{
    public class RouteStepConfig : StepConfig
    {
        //private readonly List<ConnectionConfig> fConnections = new List<ConnectionConfig>();
        //private ErrorConfig fError;

        protected RouteStepConfig()
        {
            Connections = new RegNameList<ConnectionConfig>();
        }

        public RouteStepConfig(WorkflowConfig workflowConfig)
            : base(workflowConfig)
        {
            Connections = new RegNameList<ConnectionConfig>();
        }

        public sealed override StepType StepType
        {
            get
            {
                return StepType.Route;
            }
        }

        public override bool HasMultipleOutStep
        {
            get
            {
                return true;
            }
        }

        [XmlArrayItem("Connection")]
        public RegNameList<ConnectionConfig> Connections
        {
            get;
            set;
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

        //[XmlAttribute]
        //public FillContentMode FillMode { get; set; }

        public void AddConnectionConfig(ConnectionConfig connection)
        {
            Connections.Add(connection);
        }

        public override Step CreateStep(Workflow workflow)
        {
            return new RouteStep(workflow, this);
        }
    }
}
