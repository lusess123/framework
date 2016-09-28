using System.Linq;
using System.Web.Mvc;
using Ataw.Framework.Web;
using Ataw.Workflow.Web.Bussiness;

namespace Ataw.Workflow.Web
{
    public class MyLaunchController : AtawBaseController
    {
        //
        // GET: /MyLaunch/
        private MyLaunch myLaunch = new MyLaunch();
        public ActionResult Index()
        {
            return View();
        }
        public string InitData(string name, string begin, string end)
        {
            //string now1 = DateTime.Now.Millisecond.ToString();
            // AtawAppContext.Current.Logger.Debug("开始--：" + now1);
            var myWorkFlowInstTrees = myLaunch.QueryData(name, begin, end);
            //IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm";
            return FastJson(new { total = myWorkFlowInstTrees.Count(), rows = myWorkFlowInstTrees });
            // string now2 = DateTime.Now.Millisecond.ToString();
            // AtawAppContext.Current.Logger.Debug("结束--：" + now2);
            // return "{\"total\":" + myWorkFlowInstTrees.Count() + ",\"rows\":" + JsonConvert.SerializeObject(myWorkFlowInstTrees, Formatting.Indented, timeConverter) + "}";
        }
        public ActionResult Detail(string id)
        {
            return View(myLaunch.GetDetail(id));
        }
    }
}
