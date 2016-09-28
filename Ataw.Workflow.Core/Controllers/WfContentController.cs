using System.Web.Mvc;
using Ataw.Framework.Core;
using Ataw.Framework.Web;
using Ataw.Workflow.Core;

namespace Ataw.Workflow.Web.Controllers
{
    public class WfContentController : AtawBaseController
    {
        //
        // GET: /WfContent/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StepContent(string contentXml)
        {
            WorkflowContent content = XmlUtil.ReadFromString<WorkflowContent>(contentXml);
            return View();
        }

    }
}
