using System.Web.Mvc;
using Ataw.Framework.Core;

namespace Ataw.Workflow.Web
{
    public class PlugAreaRegistration : AreaRegistration
    {

        internal static string CONN
        {
            get
            {
                return AtawAppContext.Current.ApplicationXml.Databases["Default"].ConnectionString;
            }
        }

        public override string AreaName
        {
            get
            {
                return "WorkFlow";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "Workflow_Default",
               "WorkFlow/{controller}/{action}/{id}", // URL with parameters
                new { controller = "WorkFlowInst", action = "Index", id = UrlParameter.Optional },
               new[] { "Ataw.Workflow.Web" }
           );


        }
    }
}
