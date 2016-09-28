using System;
using System.Collections.Generic;
using System.Linq;
using Ataw.Framework.Core;
using Ataw.Framework.Web;
using Ataw.MRP.BussinessLogic;
using Ataw.Workflow.Core.DataAccess;

namespace Ataw.Workflow.Web.Bussiness
{
    public class WorkFlowInst
    {
        private WorkflowDbContext context = new WorkflowDbContext(PlugAreaRegistration.CONN);
        public IList<WF_WORKFLOW_INST> QueryData(string defName, string name, string begin, string end, string order, string sort, Pagination pagination)
        {
            var list = context.WF_WORKFLOW_INST.Where(m => true);
            if (!string.IsNullOrEmpty(defName))
            {
                list = list.Where(m => m.WI_WD_NAME.Contains(defName));
            }
            if (!string.IsNullOrEmpty(name))
            {
                list = list.Where(m => m.WI_NAME.Contains(name));
            }
            if (!string.IsNullOrEmpty(begin))
            {
                var bdt = Convert.ToDateTime(begin);
                list = list.Where(m => m.WI_CREATE_DATE >= bdt);
            }

            if (!string.IsNullOrEmpty(end))
            {
                var edt = Convert.ToDateTime(end).AddDays(1);
                list = list.Where(m => m.WI_END_DATE < edt);
            }
            bool desc = order.ToLower() == "desc" ? true : false;
            switch (sort)
            {
                case "WI_WD_NAME":
                    list = desc ? list.OrderByDescending(m => m.WI_WD_NAME) : list.OrderBy(m => m.WI_WD_NAME);
                    break;
                case "WI_NAME":
                    list = desc ? list.OrderByDescending(m => m.WI_NAME) : list.OrderBy(m => m.WI_NAME);
                    break;
                case "WI_END_DATE":
                    list = desc ? list.OrderByDescending(m => m.WI_END_DATE) : list.OrderBy(m => m.WI_END_DATE);
                    break;
                default:
                    list = desc ? list.OrderByDescending(m => m.WI_CREATE_DATE) : list.OrderBy(m => m.WI_CREATE_DATE);
                    break;
            }
            pagination.TotalCount = list.Count();
            return list.QueryPage<WF_WORKFLOW_INST>(pagination).ToList();
            //动态linq
            //IQueryable<WF_WORKFLOW_INST> workflowInsts = context.WF_WORKFLOW_INST as IQueryable<WF_WORKFLOW_INST>;
            //var where = LinqUtil.True<WF_WORKFLOW_INST>();
            ////where = where.And<WF_WORKFLOW_INST>(m => m.FControlUnitID == GlobalVariable.FControlUnitID);
            //if (!string.IsNullOrEmpty(defName))
            //{
            //    where = where.And<WF_WORKFLOW_INST>(m => m.WI_WD_NAME.Contains(defName));
            //}
            //if (!string.IsNullOrEmpty(name))
            //{
            //    where = where.And<WF_WORKFLOW_INST>(m => m.WI_NAME.Contains(name));
            //}
            //if (!string.IsNullOrEmpty(begin))
            //{
            //    where = where.And<WF_WORKFLOW_INST>(m => m.WI_CREATE_DATE >= Convert.ToDateTime(begin));
            //}
            //if (!string.IsNullOrEmpty(end))
            //{
            //    where = where.And<WF_WORKFLOW_INST>(m => m.WI_END_DATE < Convert.ToDateTime(end).AddDays(1));
            //}
            //return workflowInsts.QueryData<WF_WORKFLOW_INST>(where, pagination).ToList() ;
        }
        public string DeleteWorkFlowInst(string fids)
        {
            try
            {
                var workflowInsts = context.WF_WORKFLOW_INST.Where<WF_WORKFLOW_INST>(m => fids.Contains(m.WI_ID));
                foreach (var workflowInst in workflowInsts)
                {
                    context.WF_WORKFLOW_INST.Remove(workflowInst);
                }
                context.SaveChanges();
                return "1";
            }
            catch
            {
                return "0";
            }
        }
        public IList<WF_STEP_INST> GetSteps(string instId, string sort, string order)
        {
            var steps = context.WF_STEP_INST.Where<WF_STEP_INST>(m => m.SI_WI_ID == instId);
            foreach (var step in steps)
            {
                step.SI_RECEIVE_ID = string.IsNullOrEmpty(step.SI_RECEIVE_ID) ? "" : Ataw_Users.Get(step.SI_RECEIVE_ID).NickName;
                step.SI_PROCESS_ID = string.IsNullOrEmpty(step.SI_PROCESS_ID) ? "" : Ataw_Users.Get(step.SI_PROCESS_ID).NickName;
            }
            if (order.ToUpper() == "DESC")
            {
                return steps.OrderByDescending<WF_STEP_INST>(sort).ToList();
            }
            else
            {
                return steps.OrderBy<WF_STEP_INST>(sort).ToList();
            }
        }
        public IList<WF_APPROVE_HISTORY> GetApproveHistory(string instId, string sort, string order)
        {
            var approves = context.WF_APPROVE_HISTORY.Where<WF_APPROVE_HISTORY>(m => m.AH_WORKFLOW_ID == instId);
            foreach (var approve in approves)
            {
                approve.AH_OPERATOR = string.IsNullOrEmpty(approve.AH_OPERATOR) ? "" : Ataw_Users.Get(approve.AH_OPERATOR).NickName;
            }
            if (order.ToUpper() == "DESC")
            {
                approves = approves.OrderByDescending<WF_APPROVE_HISTORY>(sort);
            }
            else
            {
                approves = approves.OrderBy<WF_APPROVE_HISTORY>(sort);
            }
            return approves.ToList();
        }
        public WF_WORKFLOW_INST GetWorkFlowInst(string id)
        {
            WF_WORKFLOW_INST workFlowInst = null;
            if (!string.IsNullOrEmpty(id))
            {
                workFlowInst = context.WF_WORKFLOW_INST.FirstOrDefault<WF_WORKFLOW_INST>(m => m.WI_ID == id);
                workFlowInst.WI_CREATE_USER = string.IsNullOrEmpty(workFlowInst.WI_CREATE_USER) ? "" : Ataw_Users.Get(workFlowInst.WI_CREATE_USER).NickName;
            }
            return workFlowInst;
        }
    }
}