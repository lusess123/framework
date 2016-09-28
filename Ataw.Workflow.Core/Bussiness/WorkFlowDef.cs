using System;
using System.Collections.Generic;
using System.Linq;
using Ataw.Framework.Core;
using Ataw.Framework.Web;
using Ataw.Workflow.Core;
using Ataw.Workflow.Core.DataAccess;

namespace Ataw.Workflow.Web.Bussiness
{
    public class WorkFlowDef
    {
        private WorkflowDbContext context = new WorkflowDbContext(PlugAreaRegistration.CONN);
        //动态linq
        //public IList<WF_WORKFLOW_DEF> QueryData(string shortName, string name, string begin, string end, string order, string sort, Pagination pagination)
        //{
        //    IQueryable<WF_WORKFLOW_DEF> workFlowDefs = context.WF_WORKFLOW_DEF as IQueryable<WF_WORKFLOW_DEF>;
        //    var where = LinqUtil.True<WF_WORKFLOW_DEF>();
        //    //where = where.And<WF_WORKFLOW_DEF>(m => m.FControlUnitID == GlobalVariable.FControlUnitID);
        //    if (!string.IsNullOrEmpty(shortName))
        //    {
        //        where = where.And<WF_WORKFLOW_DEF>(m => m.WD_SHORT_NAME.Contains(shortName));
        //    }
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        where = where.And<WF_WORKFLOW_DEF>(m => m.WD_NAME.Contains(name));
        //    }
        //    if (!string.IsNullOrEmpty(begin))
        //    {
        //        where = where.And<WF_WORKFLOW_DEF>(m => m.WD_CREATE_DATE >= Convert.ToDateTime(begin));
        //    }
        //    if (!string.IsNullOrEmpty(end))
        //    {
        //        where = where.And<WF_WORKFLOW_DEF>(m => m.WD_CREATE_DATE < Convert.ToDateTime(end).AddDays(1));
        //    }
        //    return workFlowDefs.QueryData<WF_WORKFLOW_DEF>(where, pagination).ToList();
        //}
        public IList<WF_WORKFLOW_DEF> QueryData(string shortName, string name, string begin, string end, string order, string sort, Pagination pagination)
        {
            var list = context.WF_WORKFLOW_DEF.Where(m => true);
            if (!string.IsNullOrEmpty(shortName))
            {
                list = list.Where(m => m.WD_SHORT_NAME.Contains(shortName));
            }
            if (!string.IsNullOrEmpty(name))
            {
                list = list.Where(m => m.WD_NAME.Contains(name));
            }
            if (!string.IsNullOrEmpty(begin))
            {
                var bdt = Convert.ToDateTime(begin);
                list = list.Where(m => m.WD_CREATE_DATE >= bdt);
            }

            if (!string.IsNullOrEmpty(end))
            {
                var edt = Convert.ToDateTime(end).AddDays(1);
                list = list.Where(m => m.WD_CREATE_DATE < edt);
            }
            //list = order.ToLower() == "desc" ? list.OrderByDescending(sort) : list.OrderBy(sort);
            //return list.ToList();
            bool desc = order.ToLower() == "desc" ? true : false;
            switch (sort)
            {
                case "WD_SHORT_NAME":
                    list = desc ? list.OrderByDescending(m => m.WD_SHORT_NAME) : list.OrderBy(m => m.WD_SHORT_NAME);
                    break;
                case "WD_NAME":
                    list = desc ? list.OrderByDescending(m => m.WD_NAME) : list.OrderBy(m => m.WD_NAME);
                    break;
                case "WD_IS_USED":
                    list = desc ? list.OrderByDescending(m => m.WD_IS_USED) : list.OrderBy(m => m.WD_IS_USED);
                    break;
                default:
                    list = desc ? list.OrderByDescending(m => m.WD_CREATE_DATE) : list.OrderBy(m => m.WD_CREATE_DATE);
                    break;
            }
            return list.QueryPage<WF_WORKFLOW_DEF>(pagination).ToList();
        }
        public WF_WORKFLOW_DEF GetWorkFlowDef(string shortName)
        {
            WF_WORKFLOW_DEF workFlowDef = null;
            if (!string.IsNullOrEmpty(shortName))
            {
                workFlowDef = context.WF_WORKFLOW_DEF.FirstOrDefault<WF_WORKFLOW_DEF>(m => m.WD_SHORT_NAME == shortName);
            }
            return workFlowDef == null ? new WF_WORKFLOW_DEF() : workFlowDef;
        }
        public string DeleteWorkFlow(string fids)
        {
            try
            {
                var workflowDefs = context.WF_WORKFLOW_DEF.Where<WF_WORKFLOW_DEF>(m => fids.Contains(m.WD_SHORT_NAME));
                foreach (var workflow in workflowDefs)
                {
                    context.WF_WORKFLOW_DEF.Remove(workflow);
                }
                context.SaveChanges();
                return "1";
            }
            catch
            {
                return "0";
            }
        }
        //-1重名，-2 异常，返回其他（id）正常成功
        public string AddOrEditWorkflowDefine(WorkflowConfig config)
        {
            try
            {
                var workflow = context.WF_WORKFLOW_DEF.FirstOrDefault<WF_WORKFLOW_DEF>(m => m.WD_ID == config.Id);
                if (workflow != null)
                {
                    workflow.WD_DESCRIPTION = config.Description;
                    workflow.WD_CONTENT = config.SaveString();
                    workflow.WD_DESCRIPTION = config.Description;
                    workflow.WD_NAME = config.DisplayName;
                    workflow.WD_UPDATE_DATE = context.Now;
                    workflow.WD_UPDATE_ID = GlobalVariable.UserId.ToString();
                    context.SaveChanges();
                    return workflow.WD_ID;
                }
                else
                {
                    WF_WORKFLOW_DEF def = new WF_WORKFLOW_DEF();
                    //如重名，返回-1
                    if (context.WF_WORKFLOW_DEF.FirstOrDefault<WF_WORKFLOW_DEF>(m => m.WD_SHORT_NAME == config.Name) != null)
                    {
                        return "-1";
                    }
                    def.WD_ID = context.GetUniId();
                    config.Id = def.WD_ID;
                    def.WD_CONTENT = config.SaveString();
                    def.WD_CREATE_DATE = context.Now;
                    def.WD_CREATE_ID = GlobalVariable.UserId.ToString();
                    def.WD_DESCRIPTION = config.Description;
                    def.WD_IS_USED = 1;
                    def.WD_NAME = config.DisplayName;
                    def.WD_SHORT_NAME = config.Name;
                    def.WD_UPDATE_DATE = context.Now;
                    def.FControlUnitID = GlobalVariable.FControlUnitID;
                    def.WD_UPDATE_ID = GlobalVariable.UserId.ToString();

                    context.WF_WORKFLOW_DEF.Add(def);
                    context.SaveChanges();
                    return def.WD_ID;
                }
            }
            catch
            {
                return "-2";
            }
        }

        public string EnableOrDisable(string shortNames, short? enable)
        {
            try
            {
                var workFlowDefs = context.WF_WORKFLOW_DEF.Where<WF_WORKFLOW_DEF>(m => shortNames.Contains(m.WD_SHORT_NAME));
                foreach (var workFlowDef in workFlowDefs)
                {
                    workFlowDef.WD_IS_USED = enable;
                }
                context.SaveChanges();
                return "1";
            }
            catch
            {
                return "0";
            }
        }
    }
}