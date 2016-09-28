using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Ataw.Framework.Core;

namespace Ataw.Framework.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class RightFilter : AuthorizeAttribute
    {
        /// <summary>
        /// eg:  MailOut.Del||MailDetail||MailIn.Search
        /// </summary>
        public string ExpressString { get; set; }
        public RightFilter()
        {
        }



        public RightFilter(string expressString)
        {
            ExpressString = expressString;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            //判断登录
            var b = filterContext.Controller;
            if (b != null)
            {
                if (!GlobalVariable.IsAuthenticated  )
                {
                    //string url = "~/Home/Login?returnUrl=" +
                    //        GlobalVariable.Context.Server.UrlEncode(GlobalVariable.Context.Request.RawUrl);
                    filterContext.Result = WebUtil.GetActionResultUrl("/Right/Home/UnAuthenticated?returnUrl=" + GlobalVariable.Context.Server.UrlEncode(GlobalVariable.Context.Request.RawUrl));
                    //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "UnAuthenticated", area = "", returnUrl = GlobalVariable.Context.Server.UrlEncode(GlobalVariable.Context.Request.RawUrl) }));
                    return;
                }
            }
            RegNameList<FunRightItem> funResultItems = new RegNameList<FunRightItem>();
            if (GlobalVariable.UserFID == "1001" || GlobalVariable.UserFID == "ataws")
            {
                var builder = AtawAppContext.Current.AtawRightBuilder.Value;
                List<string> menus = new List<string>();
                List<FunRightItem> menuFuns = new List<FunRightItem>();
                RightUtil.FormartString(ExpressString, menuFuns, menus);
                RegNameList<FunRightItem> items;
                foreach (string menu in menus)
                {
                    items = builder.CreateFunRight(menu);
                    if (items != null)
                    {
                        funResultItems.AddRange(items);
                    }
                }
                foreach (var item in funResultItems)
                {
                    filterContext.Controller.ViewData[item.RegName] = item.IsAllow ? "" : "display:none";
                }
                return;
            }

            var rightFilterType = RightUtil.RightVerification(ExpressString, funResultItems);
            GlobalVariable.CreateFunRights(funResultItems);
            foreach (var item in funResultItems)
            {
                filterContext.Controller.ViewData[item.RegName] = item.IsAllow ? "" : "display:none";
            }

            //JsResponseResult<string> res = new JsResponseResult<string>()
            //{
            //    ActionType = JsActionType.Url,
            //    Content = ""
            //};
            //filterContext.Result = new ContentResult() { Content = AtawAppContext.Current.FastJson.ToJSON(res) };
            switch (rightFilterType)
            {
                case RightFilterType.UnAuthenticated:
                    filterContext.Result = WebUtil.GetActionResultUrl("/Right/Home/UnAuthenticated?returnUrl=" + GlobalVariable.Context.Server.UrlEncode(GlobalVariable.Context.Request.RawUrl));
                    //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "UnAuthenticated", area = "", returnUrl = GlobalVariable.Context.Server.UrlEncode(GlobalVariable.Context.Request.RawUrl) }));
                    break;
                case RightFilterType.DenyPermission:
                    filterContext.Result = WebUtil.GetActionResultUrl("/Right/Home/WelComeRight?returnUrl=" + GlobalVariable.Context.Server.UrlEncode(GlobalVariable.Context.Request.RawUrl));
                    //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "WelComeRight", area = "", returnUrl = GlobalVariable.Context.Server.UrlEncode(GlobalVariable.Context.Request.RawUrl) }));
                    break;
                case RightFilterType.UnRenew:
                    
                    
                    
                    
                    
                    filterContext.Result = WebUtil.GetActionResultUrl("/Right/Home/WelComeRenew?returnUrl=" + GlobalVariable.Context.Server.UrlEncode(GlobalVariable.Context.Request.RawUrl));
                    //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "WelComeRenew", area = "", returnUrl = GlobalVariable.Context.Server.UrlEncode(GlobalVariable.Context.Request.RawUrl) }));
                    break;
            }
        }
    }
}
