using Ataw.Framework.Core;
namespace Ataw.Workflow.Core
{
    public sealed class StepConfigCollection : RegNameList<StepConfig>
    {
        protected override void OnAdded(StepConfig item, int index)
        {
            if (item.StepType == StepType.Begin)
                BeginStep = item;
            else
            {
                if (item.StepType == StepType.End)
                    EndStep = item;
            }
        }

        protected override void OnCleared()
        {
            BeginStep = null;
        }

        protected override void OnRemoved(StepConfig item, int index)
        {
            if (item.StepType == StepType.Begin)
                BeginStep = null;
            else
            {
                if (item.StepType == StepType.End)
                    EndStep = null;
            }
        }

        public StepConfig BeginStep { get; private set; }

        public StepConfig EndStep { get; private set; }
    }
}
