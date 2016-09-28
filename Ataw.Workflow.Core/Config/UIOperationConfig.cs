namespace Ataw.Workflow.Core
{
    public sealed class UIOperationConfig : OperationConfig
    {
        public override OperationType OperationType
        {
            get
            {
                return OperationType.UI;
            }
        }
    }
}
