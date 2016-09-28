namespace Ataw.Workflow.Core
{
    public class EndMState : MState
    {
        public static readonly State Instance = new EndMState();

        private EndMState()
        {
        }

        public override string ToString()
        {
            return "结束步骤的错误状态";
        }
    }
}
