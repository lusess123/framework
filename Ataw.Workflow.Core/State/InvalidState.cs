namespace Ataw.Workflow.Core
{
    public abstract class InvalidState : State
    {
        protected InvalidState()
        {
        }

        public override bool Execute(Workflow workflow)
        {
            ThrowInvalidState();
            return false;
        }
    }
}
