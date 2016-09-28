using System.Web;
using System.Web.Mvc;

namespace Ataw.Framework.Web
{
    public class MenuRightFilter : AtawLoginFilter
    {
        public MenuRightFilter(string menuName)
        {
            MenuName = menuName;
        }

        public string MenuName { get; set; }

        public override void OnAtawAuthorization(AuthorizationContext filterContext)
        {
            base.OnAtawAuthorization(filterContext);
            if (!IsReturn)
            {
                var builder = GlobalVariable.AppContext.AtawRightBuilder.Value;
                //GlobalVariable.Request
                var status = builder.MenuRightCheck(MenuName);
                if (status == -1)
                {
                    filterContext.Result = WebUtil.GetActionResultUrl("~/Home/WelComeRenew");
                    //HttpContext.Current.Response.Redirect("~/Home/WelComeRenew", true);  //续费页面
                    IsReturn = true;
                }
                else if (status == -2)
                {
                    filterContext.Result = WebUtil.GetActionResultUrl("~/Home/WelComeRight");
                    IsReturn = true;
                    // HttpContext.Current.Response.Redirect("");
                }
                else
                {
                    var items = builder.CreateFunRight(MenuName);
                    GlobalVariable.CreateFunRights(items);
                    foreach (var item in items)
                    {
                        filterContext.Controller.ViewData[item.RegName] = item.IsAllow ? "" : "display:none";
                    }
                }
            }
        }
    }
}
