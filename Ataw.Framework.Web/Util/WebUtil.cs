using System.Web.Mvc;
using Ataw.Framework.Core;
using Newtonsoft.Json;

namespace Ataw.Framework.Web
{
    public static class WebUtil
    {
        public static ActionResult GetActionResultUrl(string url)
        {
            if (GlobalVariable.IsAjax)
            {
                var jsRes = new JsResponseResult<string>() { ActionType = JsActionType.Url, Content = url };
                var result = new ContentResult() { Content = jsRes.ToJSON() };
                return result;
            }
            else
            {
                return new RedirectResult(url);
            }
        }

        public static string FunBt(this HtmlHelper helper, string funKey)
        {
            ///   OA/Mail/outbox.Search
            var control = helper.ViewContext.RouteData;
            string area = control.DataTokens["area"].ToString();
            string controller = control.Values["controller"].ToString();
            string action = control.Values["action"].ToString();
            // control.Route.

            string url = string.Format(ObjectUtil.SysCulture, "/{0}/{1}/{2}",
                area, controller, action);
            return helper.FunBt(url, funKey);
        }
        public static string FunBt(this HtmlHelper helper, string menuKey, string funKey)
        {
            ///   OA/Mail/outbox.Search
            string url = menuKey;
            string key = string.Format("{0}.{1}", url, funKey);
            return (helper.ViewData[key.ToUpper()] ?? "display:none").ToString();
        }

        public static object GetItem(this HtmlHelper helper, string key)
        {
            return AtawAppContext.Current.PageFlyweight.PageItems[key];
        }

        public static void SetItem<T>(this HtmlHelper helper, string key, T obj)
        {
            AtawAppContext.Current.PageFlyweight.PageItems[key] = obj;
        }

        public static string GetUrlRightValue(this HtmlHelper helper)
        {
            return GlobalVariable.Request.Url.PathAndQuery.Replace("&", "_").Replace("=", "_").Replace(".", "_");
        }

        public static string GetLoginUserLogoHttpPath(string userId)
        {
            //int _rootId = FileManagementUtil.GetRootID("CoreUserlogo");
            //string _pyPathRoot = FileManagementUtil.GetRootPath(_rootId, FilePathScheme.Physical);
            //FileManagementUtil.g
            string _path = FileManagementUtil.GetFullPath("CoreUserlogo", FilePathScheme.Http, 0, userId, "");
            //_path = new Uri(_path).LocalPath;
            //_path = "http:////{0}".AkFormat(_path.Replace(@"\", "/"));
            _path = FileUtil.ChangeFileNameAddSize(_path, "60-60");
            return _path.Replace("\\", "/").Replace("http:/", "http://");
        }

        public static T SafeJsonObjectByWeb<T>(this string str)
        {
            str = str ?? "";
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
