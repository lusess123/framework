using System.Web;
using Ataw.Framework.Core;

namespace Ataw.Framework.Web
{
    public sealed class CookieUtil
    {
        private static readonly string AppName = AtawAppContext.Current.ApplicationXml.AppName;
        //Request.Cookies.Add(new HttpCookie("LoginName", model.LoginName));
        public static void Add(string key, string value)
        {
            value = HttpUtility.UrlEncode(value);
            GlobalVariable.Response.Cookies.Add(new HttpCookie(AppName + key, value));
            GlobalVariable.Request.Cookies.Remove(AppName + key);
            GlobalVariable.Request.Cookies.Add(new HttpCookie(AppName + key, value));
            //GlobalVariable.Response.Cookies.Add(new HttpCookie(AppName + key, value));
        }
        public static void Add(string key, string value, string Domain)
        {
            value = HttpUtility.UrlEncode(value);
            var cc = new HttpCookie(AppName + key, value);
            cc.Domain = Domain;
            GlobalVariable.Response.Cookies.Add(cc);
            GlobalVariable.Request.Cookies.Remove(AppName + key);
            GlobalVariable.Request.Cookies.Add(cc);
            //GlobalVariable.Response.Cookies.Add(new HttpCookie(AppName + key, value));
        }


        public static string Get(string key)
        {
            var keyC = GlobalVariable.Request.Cookies[AppName + key];
            string str = "";
            if (keyC != null)
            {
                str = HttpUtility.UrlDecode(keyC.Value);
            }
            return str;
        }
        public static string ResponseGet(string key)
        {
            var keyC = GlobalVariable.Response.Cookies[AppName + key];
            return keyC == null ? null : HttpUtility.HtmlDecode(keyC.Value);
        }
    }
}
