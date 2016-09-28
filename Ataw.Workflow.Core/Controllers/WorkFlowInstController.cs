using System.Linq;
using System.Web.Mvc;
using Ataw.Framework.Core;
using Ataw.Framework.Web;
using Ataw.Workflow.Core.DataAccess;
using Ataw.Workflow.Web.Bussiness;

namespace Ataw.Workflow.Web
{
    public class WorkFlowInstController : AtawBaseController
    {
        //
        // GET: /WorkFlowInst/
        private WorkFlowInst workflowInst = new WorkFlowInst();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(string id)
        {
            var workFlowInst = workflowInst.GetWorkFlowInst(id);
            return View(workFlowInst == null ? new WF_WORKFLOW_INST() : workFlowInst);
        }
        public string GetSteps(string instId, string sort, string order)
        {
            if (string.IsNullOrEmpty(instId))
            {
                return "{\"total\":0,\"rows\":[]}";
            }
            var steps = workflowInst.GetSteps(instId, sort, order);
            return FastJson(new { total = steps.Count(), rows = steps });
        }
        public string GetApproveHistory(string instId, string sort, string order)
        {
            if (string.IsNullOrEmpty(instId))
            {
                return "{\"total\":0,\"rows\":[]}";
            }
            var approves = workflowInst.GetApproveHistory(instId, sort, order);
            return FastJson(new { total = approves.Count(), rows = approves });
        }
        public string InitData(string defName, string name, string begin, string end, string order, string sort)
        {
            Pagination pagination = new Pagination { PageSize = int.Parse(Request.Form["rows"]), PageIndex = int.Parse(Request.Form["page"]) - 1, IsASC = order.ToUpper() == "ASC" ? true : false, SortName = sort };
            var workflowInsts = workflowInst.QueryData(defName, name, begin, end, order, sort, pagination);
            return FastJson(new { total = pagination.TotalCount, rows = workflowInsts });
        }
        public string Delete(string fids)
        {
            return workflowInst.DeleteWorkFlowInst(fids);
        }
    }
}
