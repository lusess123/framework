using System.Linq;
using System.Web.Mvc;
using Ataw.Framework.Web;
using Ataw.Workflow.Core.DataAccess;
using Ataw.Workflow.Web.Bussiness;

namespace Ataw.Workflow.Web
{
    public class MyHistoryController : AtawBaseController
    {
        //
        // GET: /MyHistory/

        private MyHistory myHistory = new MyHistory();

        public ActionResult Launch(string id)
        {
            return Index("Launch");
        }
        public ActionResult Index(string id)
        {
            if (!string.IsNullOrEmpty(id) && id == "Launch")
            {
                ViewData["id"] = id;
            }
            else
            {
                ViewData["id"] = "";
            }
            return View("Index");
        }
        public string InitData(string name, string begin, string end, string flowType)
        {
            var myHistoryTrees = myHistory.QueryData(name, begin, end, flowType);
            return FastJson(new { total = myHistoryTrees.Count(), rows = myHistoryTrees });
        }
        public ActionResult Detail(string id)
        {
            return View(myHistory.GetDetail(id));
        }
        public string GetStepsHis(string instId, string sort, string order)
        {
            if (string.IsNullOrEmpty(instId))
            {
                return "{\"total\":0,\"rows\":[]}";
            }
            var steps = myHistory.GetStepsHis(instId, sort, order);
            return FastJson(new { total = steps.Count(), rows = steps });
        }
    }
}
