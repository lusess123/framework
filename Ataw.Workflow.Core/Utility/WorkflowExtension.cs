using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ataw.Workflow.Core;
using Ataw.Framework.Core;
using Ataw.Framework.Web;

namespace Ataw.Workflow.Web
{
    public static class WorkflowExtension
    {
        public const string MYWORK_PAGE = "/WorkFlow/MyWork/Index";
        // public const string 

        public static string GetWorkflowUrl(this Workflow.Core.Workflow workflow)
        {
            string stepName;
            ManualStep currManualStep = workflow.CurrentStep as ManualStep;
            if (currManualStep != null)
                stepName = currManualStep.Config.Name;
            else
                stepName = string.Empty;
            string wdName = workflow.WorkflowRow.WI_WD_NAME;
            // WorkflowContent context = new WorkflowContent();
            if (workflow.IsUserStep(GlobalVariable.UserId.ToString()))
            {
                ManualStep step = workflow.CurrentStep as ManualStep;
                return string.Format(ObjectUtil.SysCulture,
                    "/workflow/MyWork/ProcessDetail?id={0}",
                    workflow.WorkflowId);
            }
            else
            {
                return MYWORK_PAGE;
            }
        }
    }
}