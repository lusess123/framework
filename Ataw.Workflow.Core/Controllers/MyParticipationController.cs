using System.Linq;
using System.Web.Mvc;
using Ataw.Framework.Web;
using Ataw.Workflow.Web.Bussiness;

namespace Ataw.Workflow.Web
{
    public class MyParticipationController : AtawBaseController
    {
        //
        // GET: /MyParticipation/
        private MyParticipation myParticipation = new MyParticipation();
        public ActionResult Index()
        {

            return View();
        }
        public string InitData(string name, string begin, string end, string flowType)
        {

            var myWorkFlowInstTrees = myParticipation.QueryData(name, begin, end);
            return FastJson(new { total = myWorkFlowInstTrees.Count(), rows = myWorkFlowInstTrees });
        }
        public ActionResult Detail(string id)
        {
            return RedirectToAction("Detail", "MyLaunch", new { id = id });
        }
    }
}
