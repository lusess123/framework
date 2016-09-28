using System.Text;
using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;
namespace Ataw.Workflow.Core
{
    public class BeginStep : Step
    {
        public BeginStep(Workflow workflow, StepConfig config)
            : base(workflow, config)
        {
        }

        public RegNameList<KeyValueItem> Parameter { get; set; }

        protected override bool Execute()
        {
            BeginStepConfig stepConfig = Config as BeginStepConfig;
            Creator creator = AtawIocContext.Current.FetchInstance<Creator>(stepConfig.CreatorRegName);
            creator.Parameter = Parameter;
            creator.Workflow = Workflow;

            WorkflowContent content = creator.CreateContent(Source);
            StringBuilder sb = new StringBuilder();
            content.SaveStringBuilder(sb);
            // throw new System.NotImplementedException();
            WorkflowDbContext wfContent = Source as WorkflowDbContext;
            //ATAW_WORKFLOWContext wfContent = context as ATAW_WORKFLOWContext;

            WorkflowRow.WI_CONTENT_XML = sb.ToString();
            creator.CreateWorkflowName(Source);
            WorkflowRow.WI_NAME = creator.WorkflowName;
            creator.UpdateWorkflowRow(Source);
            wfContent.WF_WORKFLOW_INST.Add(WorkflowRow);

            //步骤基本信息
            StepUtil.SetWorkflowByStep(WorkflowRow, stepConfig.Name, stepConfig.DisplayName, wfContent.Now,
                    (int)stepConfig.StepType, (int)StepState.ProcessNotSend);
            WorkflowRow.WI_INDEX = 1;
            WorkflowRow.WI_PRIORITY = (int)creator.Priority;
            //扩展信息
            //父子流程
            //主表控制
            content.SetAllMainRow(wfContent, stepConfig, WorkflowRow.WI_ID);

            Source.Submit();

            return true;
        }

        protected override void Send(StepConfig nextStep)
        {
            StepUtil.SendStep(Workflow, nextStep, Source);

        }

        public override State GetState(StepState state)
        {
            switch (state)
            {
                case StepState.NotReceive:
                    return BeginNRState.Instance;
                case StepState.ReceiveNotOpen:
                    return BeginRNOState.Instance;
                case StepState.OpenNotProcess:
                    return BeginONPState.Instance;
                case StepState.ProcessNotSend:
                    return BeginPNSState.Instance;
                case StepState.Mistake:
                    return BeginMState.Instance;
                default:
                    AtawDebug.ThrowImpossibleCode(this);
                    return null;
            }
        }

    }
}
