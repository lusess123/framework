using Ataw.Framework.Core;
using System;

namespace Ataw.Workflow.Core
{
    public class RouteStep : Step
    {
        public RouteStep(Workflow workflow, StepConfig config)
            : base(workflow, config)
        {
        }

        protected override bool Execute()
        {
            RouteStepConfig config = Config as RouteStepConfig;
            WorkflowContent content = WorkflowInstUtil.CreateContent(WorkflowRow);

            if (string.IsNullOrEmpty(WorkflowRow.WI_ADMIN_DATA))
            {
                SetRouteConnection(config, content);
            }
            else
            {
                WorkflowRow.WI_CUSTOM_DATA = WorkflowRow.WI_ADMIN_DATA;
                WorkflowRow.WI_ADMIN_DATA = "";
            }
            WorkflowRow.WI_STATUS = (int)StepState.ProcessNotSend;

            return true;
        }

        private void SetRouteConnection(RouteStepConfig config, WorkflowContent content)
        {
            string nextStep = null;
            try
            {
                foreach (ConnectionConfig connection in config.Connections)
                {
                    bool isMatch = false;
                    var plugIn = AtawIocContext.Current.FetchInstance<IConnection>(connection.PlugName);
                    isMatch = plugIn.Match(WorkflowRow, content, Source);
                    if (isMatch)
                    {
                        nextStep = connection.NextStepName;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new NoRouteException(config, config.Error);
            }

            AtawDebug.AssertNotNull(nextStep, string.Format(ObjectUtil.SysCulture,
                 "路由步骤{0} 的下一个步骤不能为空", config.Name), this);
            //if (string.IsNullOrEmpty(nextStep))
            //{
            //    throw new NoRouteException(config, config.Error);
            //}
            //下一个步骤名
            WorkflowRow.WI_CUSTOM_DATA = nextStep;
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
                    return RouteNRState.Instance;
                case StepState.ReceiveNotOpen:
                    return RouteRNOState.Instance;
                case StepState.OpenNotProcess:
                    return RouteONPState.Instance;
                case StepState.ProcessNotSend:
                    return RoutePNSState.Instance;
                case StepState.Mistake:
                    return RouteMState.Instance;
                default:
                    AtawDebug.ThrowImpossibleCode(this);
                    return null;
            }
        }
    }
}
