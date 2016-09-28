
using System.Linq;
using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;
namespace Ataw.Workflow.Core
{
    public abstract class Step
    {
        private readonly Workflow fWorkflow;

        protected Step(Workflow workflow, StepConfig config)
        {
            fWorkflow = workflow;
            Config = config;
        }
        public IUnitOfData Source
        {
            get
            {
                return fWorkflow.Source;
            }
            set
            {
                fWorkflow.Source = value;
            }
        }
        public StepConfig Config { get; private set; }

        public Workflow Workflow
        {
            get
            {
                return fWorkflow;
            }
        }

        public WF_WORKFLOW_INST WorkflowRow
        {
            get
            {
                return fWorkflow.WorkflowRow;
            }
        }

        public void ClearDataSet()
        {
            // WorkflowDbContext db = Source as WorkflowDbContext;
            //AtawDebug.AssertNotNullOrEmpty(WorkflowConfig.ConnString, "工作流连接字符串需要配置", this);
            // string conn = (Source as AtawDbContext).Database.Connection.ConnectionString;
            var db = new WorkflowDbContext(Source.UnitSign);
            Source = db;
            var src = Workflow.WorkflowRow;
            //db.WF_WORKFLOW_INST.Attach(wfInst);
            var wfInst = db.WF_WORKFLOW_INST.FirstOrDefault(a => a.WI_ID == Workflow.WorkflowId);
            if (wfInst == null) { return; }
            wfInst.WI_ADMIN_DATA = src.WI_ADMIN_DATA;
            wfInst.WI_APPROVE = src.WI_APPROVE;
            wfInst.WI_APPROVE_NOTE = src.WI_APPROVE_NOTE;
            wfInst.WI_CONTENT_XML = src.WI_CONTENT_XML;
            wfInst.WI_CREATE_DATE = src.WI_CREATE_DATE;
            wfInst.WI_CREATE_USER = src.WI_CREATE_USER;
            wfInst.WI_CURRENT_CREATE_DATE = src.WI_CURRENT_CREATE_DATE;
            wfInst.WI_CURRENT_STEP = src.WI_CURRENT_STEP;
            wfInst.WI_CURRENT_STEP_NAME = src.WI_CURRENT_STEP_NAME;
            wfInst.WI_CUSTOM_DATA = src.WI_CUSTOM_DATA;
            wfInst.WI_ENABLE_TIMELIMIT = src.WI_ENABLE_TIMELIMIT;
            wfInst.WI_END_DATE = src.WI_END_DATE;
            wfInst.WI_END_USER = src.WI_END_USER;
            wfInst.WI_ERROR_TYPE = src.WI_ERROR_TYPE;
            wfInst.WI_ID = src.WI_ID;
            wfInst.WI_INDEX = src.WI_INDEX;
            wfInst.WI_INFO1 = src.WI_INFO1;
            wfInst.WI_IS_NOTIFY = src.WI_IS_NOTIFY;
            wfInst.WI_LAST_MANUAL = src.WI_LAST_MANUAL;
            wfInst.WI_LAST_MANUAL_NAME = src.WI_LAST_MANUAL_NAME;
            wfInst.WI_LAST_PROCESS_ID = src.WI_LAST_PROCESS_ID;
            wfInst.WI_LAST_STEP = src.WI_LAST_STEP;
            wfInst.WI_LAST_STEP_NAME = src.WI_LAST_STEP_NAME;
            wfInst.WI_NAME = src.WI_NAME;
            wfInst.WI_NEXT_EXE_DATE = src.WI_NEXT_EXE_DATE;
            wfInst.WI_NEXT_INDEX = src.WI_NEXT_INDEX;
            wfInst.WI_NOTE = src.WI_NOTE;
            wfInst.WI_PREV_STEP_NAME = src.WI_PREV_STEP_NAME;
            wfInst.WI_PRIORITY = src.WI_PRIORITY;
            wfInst.WI_PROCESS_DATE = src.WI_PROCESS_DATE;
            wfInst.WI_PROCESS_ID = src.WI_PROCESS_ID;
            wfInst.WI_RECEIVE_COUNT = src.WI_RECEIVE_COUNT;
            wfInst.WI_RECEIVE_DATE = src.WI_RECEIVE_DATE;
            wfInst.WI_RECEIVE_ID = src.WI_RECEIVE_ID;
            wfInst.WI_RECEIVE_LIST = src.WI_RECEIVE_LIST;
            wfInst.WI_REF_LIST = src.WI_REF_LIST;
            wfInst.WI_RETRIEVABLE = src.WI_RETRIEVABLE;
            wfInst.WI_RETRY_TIMES = src.WI_RETRY_TIMES;
            wfInst.WI_SEND_DATE = src.WI_SEND_DATE;
            wfInst.WI_SEND_ID = src.WI_SEND_ID;
            wfInst.WI_STATUS = src.WI_STATUS;
            wfInst.WI_STEP_TYPE = src.WI_STEP_TYPE;
            wfInst.WI_TIMEOUT_DATE = src.WI_TIMEOUT_DATE;
            //wfInst.WI_WD_NAME = src.
            Workflow.WorkflowRow = wfInst;
            // Workflow.WorkflowRow;
            // db= AtawIocContext.Current.FetchInstance<Isou
            //db.e
            // db.
            // db.WF_APPROVE_HISTORY.
        }

        protected abstract bool Execute();

        protected abstract void Send(StepConfig nextStep);

        public abstract State GetState(StepState state);

        public bool ExecuteStep()
        {
            bool result = Execute();
            ClearDataSet();
            return result;
        }

        public bool SendStep(StepConfig nextStep)
        {
            Send(nextStep);
            ClearDataSet();
            return true;
        }
    }
}
