using System;
using Ataw.Framework.Web;
using Ataw.Workflow.Core.DataAccess;
using Ataw.Workflow.Core;
using System.Collections.Generic;
using Ataw.Framework.Core;
using System.Linq;

namespace Ataw.Workflow.Web.Bussiness
{
    public abstract class MyWorkflowBase
    {
        protected WorkflowDbContext context = new WorkflowDbContext(PlugAreaRegistration.CONN);
        protected string UserId = GlobalVariable.UserId.ToString();
        protected abstract Action<BaseTree> ResultForEach
        {
            get;
        }
        private MapModel GetMapModel(Workflow.Core.Workflow workflow)
        {
            List<StepView> otherSteps = new List<StepView>();
            var lastStep = workflow.Config.Steps[workflow.CurrentStep.WorkflowRow.WI_LAST_STEP];
            if (lastStep.StepType == StepType.Route)
            {
                var steps = lastStep.NextSteps.ToList();
                steps.ForEach(
                    a =>
                    {
                        if (a.Name != workflow.CurrentStep.Config.Name)
                        {
                            otherSteps.Add(new StepView()
                            {
                                Name = a.Name,
                                DisplayName = a.DisplayName
                            });
                        }
                    }
                    );

            }
            var nextStep = workflow.CurrentStep.Config.NextSteps.FirstOrDefault();
            return new MapModel
            {
                CurrentStep = new StepView { Name = workflow.CurrentStep.Config.Name, DisplayName = workflow.CurrentStep.Config.DisplayName },
                LastStep = new StepView { Name = workflow.CurrentStep.WorkflowRow.WI_LAST_STEP, DisplayName = workflow.CurrentStep.WorkflowRow.WI_LAST_STEP_NAME },
                OtherSteps = otherSteps,
                NextStep = nextStep == null ? null : new StepView { Name = nextStep.Name, DisplayName = nextStep.DisplayName },
            };
        }
        //详情数据
        public DetailModel GetDetail(string id)
        {
            var workflow = Workflow.Core.Workflow.CreateWorkflow(context, id);
            WorkflowContent content = WorkflowInstUtil.CreateContent(workflow.WorkflowRow);
            //得到上一个人工步骤
            var workflowInst = context.WF_WORKFLOW_INST.FirstOrDefault<WF_WORKFLOW_INST>(n => n.WI_ID == id);
            var stepInst = workflowInst == null ? null : context.WF_STEP_INST.OrderByDescending(m => m.SI_INDEX).FirstOrDefault<WF_STEP_INST>(m => m.SI_WI_ID == id && m.SI_CURRENT_STEP == workflowInst.WI_LAST_MANUAL);

            return new DetailModel
            {
                WorkflowInstId = workflow.WorkflowId,
                Title = string.Format(ObjectUtil.SysCulture, "{0}-{1}-{2}", workflow.Config.DisplayName, workflow.WorkflowRow.WI_NAME, workflow.CurrentStep.Config.DisplayName),
                TabControlActions = workflow.Config.ControlActions.Where(a => a.ShowKind == ShowKind.Tab).OrderBy(a => a.Order).ToList(),
                TileControlActions = workflow.Config.ControlActions.Where(a => a.ShowKind == ShowKind.Tile).OrderBy(a => a.Order).ToList(),
                MapModel = GetMapModel(workflow),
                ReceiveTime = stepInst == null ? null : stepInst.SI_RECEIVE_DATE,
                ProcessTime = stepInst == null ? null : stepInst.SI_PROCESS_DATE,
                WorkflowContent = content.SaveString()
            };
        }
    }
}