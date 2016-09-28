using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ataw.Framework.Core;
using Ataw.MRP.BussinessLogic;
using Ataw.Workflow.Core;
using Ataw.Workflow.Core.DataAccess;

namespace Ataw.Workflow.Web.Bussiness
{
    public abstract class MyWorkflowHisINST : MyWorkflowBase
    {
        protected abstract Expression<Func<WorkflowInstHisView, BaseTree>> Selector
        {
            get;
        }
        protected abstract Expression<Func<WF_WORKFLOW_INST_HIS, bool>> InstWhereExpress
        {
            get;
        }
        public IList<BaseTree> QueryData(string name, string begin, string end, string flowType)
        {
            var list = context.WF_WORKFLOW_INST_HIS.Where(InstWhereExpress).Join(context.WF_WORKFLOW_DEF,
                inst => inst.WI_WD_NAME, wd => wd.WD_SHORT_NAME,
                (l, r) => new WorkflowInstHisView { WF_WORKFLOW_INST_HIS = l, WF_WORKFLOW_DEF = r });
            if (!string.IsNullOrEmpty(name))
            {
                list = list.Where(m => m.WF_WORKFLOW_INST_HIS.WI_NAME.Contains(name));
            }
            if (!string.IsNullOrEmpty(begin))
            {
                var bdt = Convert.ToDateTime(begin);
                list = list.Where(m => m.WF_WORKFLOW_INST_HIS.WI_CREATE_DATE >= bdt);
            }
            if (!string.IsNullOrEmpty(end))
            {
                var edt = Convert.ToDateTime(end).AddDays(1);
                list = list.Where(m => m.WF_WORKFLOW_INST_HIS.WI_CREATE_DATE < edt);
            }
            if (!string.IsNullOrEmpty(flowType))
            {
                if (flowType == "1")//我参与的历史流程
                {
                    list = list.Where(m => m.WF_WORKFLOW_INST_HIS.WI_REF_LIST != null && m.WF_WORKFLOW_INST_HIS.WI_REF_LIST.Contains(UserId));
                }
                else if (flowType == "2")//我发起的历史流程
                {
                    list = list.Where(m => m.WF_WORKFLOW_INST_HIS.WI_CREATE_USER == UserId);
                }
            }
            list = list.OrderBy(a => a.WF_WORKFLOW_INST_HIS.WI_WD_NAME).OrderByDescending(a => a.WF_WORKFLOW_INST_HIS.WI_END_DATE);

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
        public IList<WF_STEP_INST_HIS> GetStepsHis(string instId, string sort, string order)
        {
            var steps = context.WF_STEP_INST_HIS.Where<WF_STEP_INST_HIS>(m => m.SI_WI_ID == instId);
            foreach (var item in steps)
            {
                if (!string.IsNullOrEmpty(item.SI_PROCESS_ID))
                    item.SI_PROCESS_ID = MRP.BussinessLogic.CommonRight.Ataw_UsersGet(item.SI_PROCESS_ID).NickName;
            }
            if (order.ToUpper() == "DESC")
            {
                return steps.OrderByDescending<WF_STEP_INST_HIS>(sort).ToList();
            }
            else
            {
                return steps.OrderBy<WF_STEP_INST_HIS>(sort).ToList();
            }
        }
        public WF_WORKFLOW_INST_HIS GetWorkFlowInstHis(string id)
        {
            WF_WORKFLOW_INST_HIS workFlowInstHis = null;
            if (!string.IsNullOrEmpty(id))
            {
                workFlowInstHis = context.WF_WORKFLOW_INST_HIS.FirstOrDefault<WF_WORKFLOW_INST_HIS>(m => m.WI_ID == id);
                workFlowInstHis.WI_CREATE_USER = string.IsNullOrEmpty(workFlowInstHis.WI_CREATE_USER) ? "" : CommonRight.Ataw_UsersGet(workFlowInstHis.WI_CREATE_USER).NickName;
                if (!string.IsNullOrEmpty(workFlowInstHis.WI_END_USER))
                {
                    var cd = AtawIocContext.Current.FetchInstance<CodeTable<CodeDataModel>>("USER_NICKNAME");

                    workFlowInstHis.WI_END_USER = cd[workFlowInstHis.WI_END_USER].CODE_TEXT;
                }
            }
            return workFlowInstHis;
        }
    }
}