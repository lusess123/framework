using Ataw.Framework.Core;
namespace Ataw.Workflow.Core
{
    public class MergeStep : Step
    {
        public MergeStep(Workflow workflow, StepConfig config)
            : base(workflow, config)
        {
        }

        protected override bool Execute()
        {
            //if (string.IsNullOrEmpty(WorkflowRow["WI_ADMIN_DATA"].ToString()))
            //{
            //    MergeStepConfig config = Config as MergeStepConfig;
            //    IMerger merger = config.Merger.CreateObject();
            //    // 从配置中获取IMerger，计算后根据IMerger的结果返回
            //    try
            //    {
            //        return !merger.IsWait(WorkflowRow["WI_ID"].Value<int>(), WorkflowRow, null, Source);
            //    }
            //    catch
            //    {
            //        return false;
            //    }
            //}
            //else
            //{
            //    WorkflowRow["WI_ADMIN_DATA"] = DBNull.Value;
            //    return true;
            //}
            return true;
        }
        protected override void Send(StepConfig nextStep)
        {
            //StepUtil.SendStep(Workflow, nextStep);
        }

        public override State GetState(StepState state)
        {
            switch (state)
            {
                case StepState.NotReceive:
                    return MergeNRState.Instance;
                case StepState.ReceiveNotOpen:
                    return MergeRNOState.Instance;
                case StepState.OpenNotProcess:
                    return MergeONPState.Instance;
                case StepState.ProcessNotSend:
                    return MergePNSState.Instance;
                default:
                    AtawDebug.ThrowImpossibleCode(this);
                    return null;
            }
        }
    }
}
