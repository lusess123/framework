using Ataw.Framework.Core;

namespace Ataw.Workflow.Core
{
    public abstract class Creator
    {
        private const int WFNNAME_LENGTH = 250;
        private string fWorkflowName;

        public RegNameList<KeyValueItem> Parameter
        {
            get;
            set;
        }

        public Workflow Workflow
        {
            get;
            set;
        }

        protected Creator()
        {
        }

        public virtual string WorkflowName
        {
            get
            {
                return fWorkflowName;
            }
            set
            {
                fWorkflowName = StringUtil.TruncString(value, WFNNAME_LENGTH);
            }
        }

        public WorkflowPriority Priority { get; set; }

        public abstract WorkflowContent CreateContent(IUnitOfData source);

        public abstract void CreateWorkflowName(IUnitOfData source);

        public abstract void UpdateWorkflowRow(IUnitOfData source);
    }
}
