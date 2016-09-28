using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ataw.Framework.Core;


namespace Ataw.Framework.Web
{
    public class AtawWebAppRegistration : IAppRegistration
    {
        public const string REG_NAME = "ATAW_WEB";

        public AtawWebAppRegistration()
        {
            MvcActionModels = new List<MvcActionModel>();
        }

        //        public void BundleJavaScript(BundleCollection bundles)
        //        {
        ////D:\project\AtawCode\src\framework\Web\Ataw.WebSite\Scripts\Core\ataw-common.js
        //            bundles.Add(new ScriptBundle("~/Scripts/Core/ataw-common.js"));
        //        }

        public void Initialization()
        {
            //var request = GlobalVariable.Request;
            //string host = request.Url.Host;
            //string port = request.Url.Port.ToString();
            //string appPath = request.ApplicationPath;

            //AtawAppContext.Current.WebRootPath = string.Format("{0}:{1}{2}", host, port, appPath);
            AreaRegistration.RegisterAllAreas();

            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            // RegisterGlobalFilters(GlobalFilters.Filters);
            // RegisterRoutes(RouteTable.Routes);

            RouteCollection routes = RouteTable.Routes;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            MvcConfigInfo xmlObj = GlobalVariable.AppContext.MvcConfigXml;
            if (xmlObj.Routes.RegName != null)
            {

                var homeModel = xmlObj.DataRoutes.Where(a => a.RegName == xmlObj.Routes.RegName).FirstOrDefault();
                if (homeModel != null)
                {

                    routes.MapRoute(
                      "Default", // Route name
                      "{area}/{controller}/{action}/{id}", // URL with parameters
                        //homeModel.Param.IsEmpty() ?
                        //new { controller = homeModel.ControlName, action = homeModel.ActionName }
                        //:
                      new { area = homeModel.AreaName, controller = homeModel.ControlName, action = homeModel.ActionName, id = homeModel.Param }
                      , new { incoming = new HomeRequestRouteConstraint() },
                       new[] { homeModel.NameSpace }// Parameter defaults
                  );
                }

            }
            else
            {

                //var homeModel = MvcActionModels.OrderByDescending(a => a.Order).FirstOrDefault();
                //if (homeModel != null)
                //{
                //    routes.MapRoute(
                //        "Default", // Route name
                //        "{controller}/{action}/{id}", // URL with parameters
                //        new { controller = homeModel.ControlName, action = homeModel.ActionName, id = UrlParameter.Optional },
                //         new[] { homeModel.NameSpace }// Parameter defaults
                //    );
                //}
            }

            //var homeModel = MvcActionModels.OrderByDescending(a => a.Order).FirstOrDefault();
            //if (homeModel != null)
            //{
            //    routes.MapRoute(
            //        "Default", // Route name
            //        "{controller}/{action}/{id}", // URL with parameters
            //        new { controller = homeModel.ControlName, action = homeModel.ActionName, id = UrlParameter.Optional },
            //         new[] { homeModel.NameSpace }// Parameter defaults
            //    );
            //}

            //routes.MapRoute(
            //        "Default_RIGHT_DEFAULT", // Route name
            //        "{area}/{controller}/{action}/{id}", // URL with parameters
            //        new { area = "Right", controller = "Home", action = "Login", id = UrlParameter.Optional }
            //        , new { incoming = new HomeRequestRouteConstraint() },
            //         new[] { "Ataw.Right.Web.Controllers" }// Parameter defaults
            //    );
        }

        public List<MvcActionModel> MvcActionModels { get; set; }

        public void SetAssamblyClassType(Type type)
        {
            // throw new System.NotImplementedException();

            if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(AtawBaseController)))
            {
                string controlName = type.Name;
                foreach (var p in type.GetMethods())
                {
                    var att = Attribute.GetCustomAttribute(p, typeof(WebSiteHomeAttribute)) as WebSiteHomeAttribute;
                    if (att != null)
                    {
                        MvcActionModels.Add(
                            new MvcActionModel()
                            {
                                ControlName = controlName.Replace("Controller", ""),
                                ActionName = p.Name,
                                NameSpace = type.Namespace,
                                Order = att.Order
                            }
                            );
                    }
                }
            }
        }

        class HomeRequestRouteConstraint : IRouteConstraint
        {
            private static string[] fAreas = null;
            public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
            {
                var areaName = values["area"].ToString().ToLower();
                if (string.IsNullOrWhiteSpace(areaName) || areaName == "core" || areaName == "akpage")
                    return true;
                if (fAreas == null)
                    fAreas = RouteTable.Routes.OfType<Route>()
                        .Where(d => d.DataTokens != null && d.DataTokens.ContainsKey("area"))
                        .Select(r => r.DataTokens["area"].ToString().ToLower()).Distinct().ToArray();
                return fAreas.Contains(areaName);
            }
        }
    }
}
