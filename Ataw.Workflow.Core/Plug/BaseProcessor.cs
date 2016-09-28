using System.Text;
using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;
using System.Transactions;
namespace Ataw.Workflow.Core
{
    public abstract class BaseProcessor
    {
        protected BaseProcessor()
        {
        }

        public StepConfig Config { get; set; }

        public IUnitOfData Source { get; set; }

        public WorkflowContent Content { get; set; }

        public virtual bool AddContent()
        {
            return false;
        }

        public virtual void ApplyDatas(Transaction transaction)
        {
        }

        public bool SaveContent(WF_WORKFLOW_INST workflowRow)
        {
            if (AddContent())
            {
                StringBuilder sb = new StringBuilder();
                Content.SaveStringBuilder(sb);
                workflowRow.WI_CONTENT_XML = sb.ToString();
                return true;
            }
            return false;
        }
    }
}
