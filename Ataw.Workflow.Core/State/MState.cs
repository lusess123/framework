using Ataw.Framework.Core;
namespace Ataw.Workflow.Core
{
    public class MState : State
    {
        protected MState()
        {
        }

        public override StepState StepState
        {
            get
            {
                return StepState.Mistake;
            }
        }

        public override bool Execute(Workflow workflow)
        {
            Step step = workflow.CurrentStep;
            StepState lastState;
            MistakeReason reason = workflow.WorkflowRow.WI_ERROR_TYPE.Value<MistakeReason>();
            if (reason == MistakeReason.NoActor)
                lastState = StepState.ProcessNotSend;
            else
                lastState = StepState.NotReceive;
            step.ClearDataSet();
            workflow.UpdateState(step.GetState(lastState));
            return true;
        }
    }
}
