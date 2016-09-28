using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Ataw.Framework.Core;

namespace Ataw.Framework.Web
{
    public class AtawLoginFilter : AuthorizeAttribute
    {
        protected const string  FILTER_KEY = "_FILTER_KEY";
        protected bool IsReturn {
            get {
                return (GlobalVariable.PageItems[FILTER_KEY] ?? false).Value<bool>();

            }
            set {
                GlobalVariable.PageItems[FILTER_KEY] = value;
            }
        }

       // protected virtual string LoginUrl { get; private set; }

        protected virtual string SetReturnUrl(string redirectUrl)
        {
            string _ataw = AtawAppContext.Current.FControlUnitID;
            if (_ataw.IsEmpty())
            {
                _ataw = AtawAppContext.Current.MvcConfigXml.GetParam();
            }
            if (!_ataw.IsEmpty())
            {
                _ataw = "/" + _ataw;
            }

            string url = "~/Right/Home/Login" + _ataw + "?returnUrl=" + redirectUrl;
           return url;
        }   
        public virtual void OnAtawAuthorization(AuthorizationContext filterContext)
        {
            //if (filterContext.Controller is AtawBaseController)
            //{
             
                var b = filterContext.Controller;
                if (b != null)
                {
                    if (!GlobalVariable.IsAuthenticated)
                    {
                        
                        if (!GlobalVariable.IsAjax)
                        {
                            string url = SetReturnUrl(GlobalVariable.Context.Server.UrlEncode(GlobalVariable.Context.Request.RawUrl));
                           
                            filterContext.Result = WebUtil.GetActionResultUrl(url);
                            IsReturn = true;
                        }
                        else
                        {
                            JsResponseResult<string> obj = new JsResponseResult<string>();
                            obj.Content = "请登录...";
                            obj.ActionType = JsActionType.Alert;

                            filterContext.Result = new ContentResult() { 
                                 Content = AtawAppContext.Current.FastJson.ToJSON(obj)
                            
                            };

                            IsReturn = true;
                        }
                    }
                }
            //}
            
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!IsReturn)
            {
                OnAtawAuthorization(filterContext);
            }
            
        }
        

    }
}
