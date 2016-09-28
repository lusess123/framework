using System.Linq;
using System.Web.Mvc;
using Ataw.Framework.Core;
using Ataw.Framework.Web;
using Ataw.Workflow.Web.Bussiness;

namespace Ataw.Workflow.Web
{
    public class WorkFlowDefController : AtawBaseController
    {
        private WorkFlowDef workFlowDef = new WorkFlowDef();

        public ActionResult NewIndex()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(string shortName)
        {
            return View(workFlowDef.GetWorkFlowDef(shortName));
        }
        public string InitData(string shortName, string name, string begin, string end, string order, string sort)
        {
            Pagination pagination = new Pagination { PageSize = int.Parse(Request.Form["rows"]), PageIndex = int.Parse(Request.Form["page"]) - 1, IsASC = order.ToUpper() == "ASC" ? true : false, SortName = sort };
            var workFlowDefs = workFlowDef.QueryData(shortName, name, begin, end, order, sort, pagination);
            return FastJson(new { total = workFlowDefs.Count(), rows = workFlowDefs });
        }
        public string Delete(string fids)
        {
            return workFlowDef.DeleteWorkFlow(fids);
        }
        public string EnableOrDisable(string shortNames, short? enable)
        {
            return workFlowDef.EnableOrDisable(shortNames, enable);
        }
    }
}
