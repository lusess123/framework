using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;
using System.Linq;
namespace Ataw.Workflow.Core
{
    public abstract class StepConfig : IRegName
    {
        // private readonly RegNameList<StepConnectionConfig> fNextStepNames;
        //private readonly List<string> fPrevStepNames;
        private string fName;
        private ReadOnlyCollection<StepConfig> fNextSteps;
        private ReadOnlyCollection<StepConfig> fPrevSteps;
        private StepConfig[] fNextStepArray;
        private StepConfig[] fPrevStepArray;
        private bool fInitialized;
        private WorkflowConfig fParent;

        protected StepConfig()
        {
            NextStepNames = new RegNameList<StepConnectionConfig>();
            PrevStepName = new List<string>();
        }

        public StepConfig(WorkflowConfig workflowConfig)
            : this()
        {
            fParent = workflowConfig;
        }

        #region IRegName 成员

        public string RegName
        {
            get
            {
                return Name;
            }
        }

        #endregion

        #region 设计器支持
        [XmlAttribute]
        public string Id { get; set; }

        [XmlAttribute]
        public int Height { get; set; }

        [XmlAttribute]
        public int Width { get; set; }

        [XmlAttribute]
        public int Left { get; set; }

        [XmlAttribute]
        public int Top { get; set; }
        [XmlElement]
        public virtual string DisplayName { get; set; }
        #endregion
        [XmlArrayItem("PrevStep")]
        public List<string> PrevStepName
        {
            get;
            set;
        }


        [XmlElement]
        public virtual string Name
        {
            get
            {
                return fName;
            }
            set
            {
                if (fName == value)
                    return;
                fName = value;
                if (Parent != null)
                {

                }
            }
        }
        [XmlIgnore]
        public WorkflowConfig Parent
        {
            get
            {
                return fParent;
            }
            set
            {
                fParent = value;
            }
        }
        [XmlIgnore]
        public IEnumerable<StepConfig> PrevSteps
        {
            get
            {
                Initialize();
                return fPrevSteps;
            }
        }
        [XmlIgnore]
        public IEnumerable<StepConfig> NextSteps
        {
            get
            {
                Initialize();
                return fNextSteps;
            }
        }

        [XmlArrayItem("NextStep")]
        public RegNameList<StepConnectionConfig> NextStepNames
        {
            get;
            set;
        }

        public int NextStepCount
        {
            get
            {
                return NextStepNames.Count;
            }
        }

        public virtual StepType StepType
        {
            get
            {
                return StepType.None;
            }
        }
       // public RegNameList<ControlActionConfig> ControlActions { get; set; }
        public virtual bool HasInStep
        {
            get
            {
                return true;
            }
        }

        public virtual bool HasOutStep
        {
            get
            {
                return true;
            }
        }

        public virtual bool HasMultipleInStep
        {
            get
            {
                return true;
            }
        }

        public virtual bool HasMultipleOutStep
        {
            get
            {
                return false;
            }
        }

        private void Initialize()
        {
            if (!fInitialized)
            {
                fInitialized = true;
                fPrevStepArray = Array.ConvertAll(PrevStepName.ToArray(),
                    (input) => Parent.Steps[input]);
                fPrevSteps = new ReadOnlyCollection<StepConfig>(fPrevStepArray);
                fNextStepArray = Array.ConvertAll(NextStepNames.ToArray(),
                    (input) => Parent.Steps[input.StepName]);
                fNextSteps = new ReadOnlyCollection<StepConfig>(fNextStepArray);
            }
        }

        public static void AddConfigInfo()
        {
        }

        public void SetParentObj(object parent)
        {
            WorkflowConfig config = parent as WorkflowConfig;
            SetParent(config);
        }

        protected virtual void SetParent(WorkflowConfig parent)
        {
        }

        public virtual void Prepare(WF_WORKFLOW_INST workflowRow, IUnitOfData source)
        {
        }

        public void AddNextConfig(StepConfig config)
        {
            AddNextConfig(config, 0, 0, 0, 0);
        }

        public void AddNextConfig(StepConfig config, int fromX, int fromY, int toX, int toY)
        {
            config.PrevStepName.Add(Name);
            NextStepNames.Add(new StepConnectionConfig(config.Name, fromX, fromY, toX, toY));
        }

        public abstract Step CreateStep(Workflow workflow);
    }
}
