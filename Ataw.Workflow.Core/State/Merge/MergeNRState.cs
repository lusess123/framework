namespace Ataw.Workflow.Core
{
    public class MergeNRState : NRState
    {
        public static readonly State Instance = new MergeNRState();

        private MergeNRState()
        {
        }

        public override string ToString()
        {
            return "聚合步骤的未接收状态";
        }
    }
}
