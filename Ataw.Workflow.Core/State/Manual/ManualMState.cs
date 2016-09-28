namespace Ataw.Workflow.Core
{
    public class ManualMState : MState
    {
        public static readonly State Instance = new ManualMState();

        private ManualMState()
        {
        }

        public override string ToString()
        {
            return "人工步骤的错误状态";
        }
    }
}
