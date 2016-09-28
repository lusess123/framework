using System.Web.Mvc;
using System.Web.Routing;
using Ataw.Framework.Core;

namespace Ataw.Framework.Web
{
    public class CoreAreaRegistration : AreaRegistration
    {

        public override string AreaName
        {
            get { return "Core"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            ControllerRoute("Selector");
            ControllerRoute("MapCutter");
            ControllerRoute("Uploader");
            ControllerRoute("Module");
            ControllerRoute("plug");
            ControllerRoute("SimpleCutImage");
            ControllerRoute("EditorUploadJson");
            ControllerRoute("EditorFileManager");
            ControllerRoute("Desk");
            ControllerRoute("WebService");
            ControllerRoute("Card");
            ControllerRoute("File");
            ControllerRoute("Momery");
           // ControllerRoute("WebService");
        }

        private static void ControllerRoute(string controllerName)
        {
            RouteTable.Routes.MapRoute(
              "AtawWeb_" + controllerName, // 路由名称
            string.Format(ObjectUtil.SysCulture, "Core/{0}/{{action}}", controllerName), // 带有参数的 URL
             new { controller = controllerName },
             new string[] { "Ataw.Framework.Web" }

         );

            RouteTable.Routes.MapRoute(
              "AtawWeb_NEW" + controllerName, // 路由名称
            string.Format(ObjectUtil.SysCulture, "{0}/{{action}}", controllerName), // 带有参数的 URL
             new { controller = controllerName },
             new string[] { "Ataw.Framework.Web" }
         );
        }
    }

    public class WebAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "akpage"; }
        }

        private static void RegisterModulePage()
        {
            RouteTable.Routes.MapRoute(
                    "AtawWebModulePage__", // 路由名称
                    "akpage/{path}/{pagestyle}",
                    new { controller = "Module", action = "Page" },
                    new string[] { "Ataw.Framework.Web" }
                    );
        }

        //注册Area 以便视图页面调用控制器方法
        public override void RegisterArea(AreaRegistrationContext context)
        {
            RegisterModulePage();
        }
    }
}
