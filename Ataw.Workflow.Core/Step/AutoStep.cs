using Ataw.Framework.Core;
using System;


namespace Ataw.Workflow.Core
{
    public class AutoStep : Step
    {
        public AutoStep(Workflow workflow, StepConfig config)
            : base(workflow, config)
        {
        }

        protected override bool Execute()
        {
            AutoStepConfig config = Config as AutoStepConfig;

            WorkflowContent content = WorkflowInstUtil.CreateContent(WorkflowRow);
            try
            {
                AutoProcessor processor = AtawIocContext.Current.FetchInstance<AutoProcessor>(config.PlugRegName);
                processor.Config = config;
                processor.Source = Source;
                processor.Content = content;
                processor.Execute(WorkflowRow);
            }
            catch (Exception ex)
            {
                throw new PlugInException(config,config.Error, ex);
            }

            //    WorkflowRow.BeginEdit();
            WorkflowRow.WI_STATUS = (int)StepState.ProcessNotSend;
            WorkflowInstUtil.ClearError(WorkflowRow);
            int result = Source.Submit();
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
                    return AutoNRState.Instance;
                case StepState.ReceiveNotOpen:
                    return AutoRNOState.Instance;
                case StepState.OpenNotProcess:
                    return AutoONPState.Instance;
                case StepState.ProcessNotSend:
                    return AutoPNSState.Instance;
                case StepState.Mistake:
                    return AutoMState.Instance;
                default:
                    AtawDebug.ThrowImpossibleCode(this);
                    return null;
            }
        }
    }
}
