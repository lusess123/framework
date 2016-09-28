using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ataw.Framework.Web;
using Ataw.Workflow.Core;
using Ataw.Workflow.Core.DataAccess;
using Ataw.Framework.Core;

namespace Ataw.Workflow.Web.Bussiness
{
    public abstract class MyWorkflowINST : MyWorkflowBase
    {
        protected abstract Expression<Func<WorkflowInstView, BaseTree>> Selector
        {
            get;
        }
        protected abstract Expression<Func<WF_WORKFLOW_INST, bool>> InstWhereExpress
        {
            get;
        }
        public IList<BaseTree> QueryData(string name, string begin, string end)
        {
            var list = context.WF_WORKFLOW_INST.Where(InstWhereExpress).Join(context.WF_WORKFLOW_DEF,
                inst => inst.WI_WD_NAME, wd => wd.WD_SHORT_NAME,
                (l, r) => new WorkflowInstView { WF_WORKFLOW_INST = l, WF_WORKFLOW_DEF = r });
            if (!string.IsNullOrEmpty(name))
            {
                // where = where.And<WF_WORKFLOW_INST>(m => m.WI_NAME.Contains(name));
                list = list.Where(m => m.WF_WORKFLOW_INST.WI_NAME.Contains(name));
            }
            if (!string.IsNullOrEmpty(begin))
            {
                var bdt = Convert.ToDateTime(begin);
                // where = where.And<WF_WORKFLOW_INST>(m => m.WI_CREATE_DATE >= Convert.ToDateTime(begin));
                list = list.Where(m => m.WF_WORKFLOW_INST.WI_CREATE_DATE >= bdt);
            }
            if (!string.IsNullOrEmpty(end))
            {
                var edt = Convert.ToDateTime(end).AddDays(1);
                // where = where.And<WF_WORKFLOW_INST>(m => m.WI_CREATE_DATE < Convert.ToDateTime(end).AddDays(1));
                list = list.Where(m => m.WF_WORKFLOW_INST.WI_CREATE_DATE < edt);
            }

            list = list.OrderBy(a => a.WF_WORKFLOW_INST.WI_WD_NAME).OrderByDescending(a => a.WF_WORKFLOW_INST.WI_RECEIVE_DATE);

            var result = list.Select(Selector).ToList();

            if (ResultForEach != null)
            {
                result.ForEach(ResultForEach);
            }

            var def = result.GroupBy(a => a._parentId, (key, source) => new BaseTree()
            {
                ID = key,
                Name = source.FirstOrDefault().WDName,
                _parentId = ""
            }
                   );
            result.AddRange(def);
            return result;
        }

        public ProcessModel ProcessDetail(string id)
        {
            var workflow = Workflow.Core.Workflow.CreateWorkflow(context, id);
            var processModel = new ProcessModel
            {
                DetailModel = GetDetail(id)
            };
            if (workflow.CurrentStep.Config.StepType == StepType.Manual && workflow.WorkflowRow.WI_RECEIVE_ID == GlobalVariable.UserId.ToString())
            {
                var manualStep = workflow.CurrentStep.Config as ManualStepConfig;
                processModel.NonUIOperations = manualStep.Process.NonUIOperations;
                processModel.UIOperation = manualStep.Process.UIOperation;
                processModel.DetailModel.TabControlActions = manualStep.ControlActions.Where(a => a.ShowKind == ShowKind.Tab).OrderBy(a => a.Order).ToList();
                processModel.DetailModel.TileControlActions = manualStep.ControlActions.Where(a => a.ShowKind == ShowKind.Tile).OrderBy(a => a.Order).ToList();
            }
            return processModel;
        }
    }
}