using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Ataw.Framework.Web
{
    public class FunRightFilter : MenuRightFilter
    {

        public FunRightFilter(string menuName ,string funName)
            : base(menuName)
        {
           // MenuName = menuName;
            FunName = funName;
        }

      

        public string FunName { get; set; }

        public override void OnAtawAuthorization(AuthorizationContext filterContext)
        {
            base.OnAtawAuthorization(filterContext);
            if (!IsReturn)
            {
                var items = GlobalVariable.FunRights;
                if (!string.IsNullOrEmpty(FunName))
                {
                    var item = items[FunName];
                    if (item == null || !item.IsAllow)
                    {
                        filterContext.Result =  WebUtil.GetActionResultUrl("/Home/WelComeRight");
                        IsReturn = true;
                        // HttpContext.Current.Response.Redirect();
                    }
                }
            }
        }
    }
}
