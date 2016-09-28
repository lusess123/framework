using System;
using System.Linq;
using System.Xml.Serialization;
using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;
namespace Ataw.Workflow.Core
{
    [Serializable]
    public class WorkflowConfig : XmlConfigBase
    {
        public WorkflowConfig()
        {
            Steps = new StepConfigCollection();
            ControlActions = new RegNameList<ControlActionConfig>();
            ContentChoice = ConfigChoice.None;
        }

        public ConfigChoice ContentChoice { get; set; }
        //public string ContentXml { get; set; }
        public RegNameList<ControlActionConfig> ControlActions { get; set; }

        // [XmlIgnore]
        public static string ConnString { get; set; }

        [XmlAttribute]
        public string Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        //[XmlAttribute]
        //public bool Retrievable { get; set; }

        [XmlAttribute]
        public string DisplayName { get; set; }

        public string Description { get; set; }

        [XmlAttribute]
        public WorkflowPriority Priority { get; set; }
        public string GuideXml { get; set; }
        public string ContentXml { get; set; }
        public string ContentXmlHis { get; set; }
        [XmlAttribute]
        public bool IsSaveContent { get; set; }

        // [XmlChoiceIdentifier("Steps")]
        [XmlArrayItem(Type = typeof(BeginStepConfig))]
        [XmlArrayItem(Type = typeof(ManualStepConfig))]
        [XmlArrayItem(Type = typeof(RouteStepConfig))]
        [XmlArrayItem(Type = typeof(MergeStepConfig))]
        [XmlArrayItem(Type = typeof(EndStepConfig))]
        [XmlArrayItem(Type = typeof(AutoStepConfig))]
        public StepConfigCollection Steps
        {
            get;
            set;
        }

        public static WorkflowConfig GetByName(string name, IUnitOfData source)
        {
            WorkflowDbContext context = source as WorkflowDbContext;

            var define = context.WF_WORKFLOW_DEF.FirstOrDefault(a => a.WD_SHORT_NAME == name);
            // define.WD_CONTENT
            if (define != null)
            {
                WorkflowConfig config = XmlUtil.ReadFromString<WorkflowConfig>(define.WD_CONTENT);
                config.Steps.ToList().ForEach(a => a.Parent = config);
                return config;
            }
            else

                return null;


        }

    }
}
