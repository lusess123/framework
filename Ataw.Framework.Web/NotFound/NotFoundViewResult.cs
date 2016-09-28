using System;
using System.Web;
using System.Web.Mvc;
using Ataw.Framework.Core;
using Ataw.Framework.Web;
namespace NotFoundMvc
{
    /// <summary>
    /// Renders a view called "NotFound" and sets the response status code to 404.
    /// View data is assigned for "RequestedUrl" and "ReferrerUrl".
    /// </summary>
    public class NotFoundViewResult : HttpNotFoundResult
    {
        public NotFoundViewResult()
        {
            ViewName = "NotFound";
            ViewData = new ViewDataDictionary();
        }

        /// <summary>
        /// The name of the view to render. Defaults to "NotFound".
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// The view data passed to the NotFound view.
        /// </summary>
        public ViewDataDictionary ViewData { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            var request = context.HttpContext.Request;

            ViewData["RequestedUrl"] = GetRequestedUrl(request);
            ViewData["ReferrerUrl"] = GetReferrerUrl(request, request.Url.OriginalString);

            response.StatusCode = 404;
            // Prevent IIS7 from overwriting our error page!
            response.TrySkipIisCustomErrors = true;
            var req = context.HttpContext.Request;
            string mesg = "{1}IP地址：{0}{1}浏览器:{2}({3}){1}网址：{4}{1}".AkFormat(req.UserHostAddress, Environment.NewLine, req.Browser.Type, req.Browser.Browser, req.Url);
            if (AtawAppContext.Current.Logger != null)
                AtawAppContext.Current.Logger.Info(mesg);


            if (GlobalVariable.IsAjax)
            {
                JsResponseResult<string> jsRes = new JsResponseResult<string>() { ActionType = JsActionType.Alert };
                jsRes.Content = req.Url.ToString() + " 未找到";
                ContentResult res = new ContentResult();
                res.Content = AtawAppContext.Current.FastJson.ToJSON(jsRes);
                response.Clear();
                res.ExecuteResult(context);
            }
            else
            {
                ViewResult viewResult = null;
                viewResult = new ViewResult
               {
                   ViewName = ViewName,
                   ViewData = ViewData
               };
                response.Clear();
                viewResult.ExecuteResult(context);
            }

        }

        string GetRequestedUrl(HttpRequestBase request)
        {
            return request.AppRelativeCurrentExecutionFilePath == "~/notfound"
                       ? ExtractOriginalUrlFromExecuteUrlModeErrorRequest(request.Url)
                       : request.Url.OriginalString;
        }

        string GetReferrerUrl(HttpRequestBase request, string url)
        {
            return request.UrlReferrer != null && request.UrlReferrer.OriginalString != url
                       ? request.UrlReferrer.OriginalString
                       : null;
        }

        /// <summary>
        /// Handles the case when a web.config &lt;error statusCode="404" path="/notfound" responseMode="ExecuteURL" /&gt; is triggered.
        /// The original URL is passed via the querystring.
        /// </summary>
        string ExtractOriginalUrlFromExecuteUrlModeErrorRequest(Uri url)
        {
            // Expected format is "?404;http://hostname.com/some/path"
            var start = url.Query.IndexOf(';');
            if (0 <= start && start < url.Query.Length - 1)
            {
                return url.Query.Substring(start + 1);
            }
            else
            {
                // Unexpected format, so just return the full URL!
                return url.ToString();
            }
        }
    }
}