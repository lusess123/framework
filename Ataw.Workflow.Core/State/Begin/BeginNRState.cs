namespace Ataw.Workflow.Core
{
    public class BeginNRState : NRState
    {
        public static readonly State Instance = new BeginNRState();

        private BeginNRState()
        {
        }

        public override string ToString()
        {
            return "开始步骤的未接收状态";
        }
    }
}
