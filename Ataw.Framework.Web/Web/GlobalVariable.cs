using System;
using System.Collections;
using System.Web;
using Ataw.Framework.Core;
using System.Data.SqlClient;
using System.Data;

namespace Ataw.Framework.Web
{
    public sealed class GlobalVariable
    {
        private static readonly string Key_ContextName = "_KeyContext";
        private static readonly string Key_RightName = "_KeyRight";

        public static AtawAppContext AppContext
        {
            get
            {
                return AtawAppContext.Current;
            }
        }

        public static T GetPageItem<T>(string regName = "")
        {
            if (string.IsNullOrEmpty(regName))
            {
                regName = typeof(T).ToString();
            }
            return PageItems[regName].Value<T>();
        }

        public static void SetPageItem<T>(T item, string regName = "")
        {
            if (string.IsNullOrEmpty(regName))
            {
                regName = typeof(T).ToString();
            }
            PageItems[regName] = item;
        }

        public static IDictionary PageItems
        {
            get
            {
                return Context.Items;
            }
        }

        public static RegNameList<FunRightItem> FunRights
        {
            get
            {
                return Context.Items[Key_RightName] as RegNameList<FunRightItem>;
            }
        }

        public static bool IsAjax
        {
            get
            {
                string sheader = Request.Headers["X-Requested-With"];
                return (sheader != null && sheader == "XMLHttpRequest");
            }
        }

        public static HttpContext Context
        {
            get
            {
                AssertHttpContext();
                return HttpContext.Current;
            }
        }

        public static HttpRequest Request
        {
            get
            {
                AssertHttpContext();
                return HttpContext.Current.Request;
            }
        }

        public static HttpCookieCollection Cookies
        {
            get
            {
                AssertHttpContext();
                return HttpContext.Current.Response.Cookies;

            }
        }

        public static HttpResponse Response
        {
            get
            {
                AssertHttpContext();
                return HttpContext.Current.Response;
            }
        }

        public static BaseLogonUserInfo Info
        {
            get
            {
                return Context.Items[Key_ContextName] as BaseLogonUserInfo;
            }
        }

        public static object UserId
        {
            get
            {
                return Context.User.Identity.Name;
                //object _userId =  AtawAppContext.Current.PageFlyweight.PageItems["STATIC_LOGIN_USERID"];
                //if (_userId != null)
                //{
                //    return _userId.ToString();
                //}
                //else
                //{
                //    return Context.User.Identity.Name;
                //}
            }
        }
         

        public static string FControlUnitID
        {
            get {
                if (IsAuthenticated)
                {
                    string _funitId = CookieUtil.Get("FControlUnitID");
                    if (_funitId.IsEmpty())
                    {
                        string sql = "SELECT TOP 1 FControlUnitID ,UserName,NickName FROM  Ataw_Users WHERE USERID = @userid ";
                        var db = AtawAppContext.Current.CreateDbContext();
                        DataSet ds = db.QueryDataSet(sql, new SqlParameter("@userid", UserFID));
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow row = ds.Tables[0].Rows[0];
                            _funitId = row["FControlUnitID"].Value<string>();
                            CookieUtil.Add("UserName", row["UserName"].ToString());
                            CookieUtil.Add("NickName", row["NickName"].ToString());
                        }
                        
                    }
                    return _funitId;
                }
                else
                    return "";
            }
            set { CookieUtil.Add("FControlUnitID", value); }
        }
 

        public static string UserFID
        {
            get { return  Context.User.Identity.Name; }
            set { CookieUtil.Add("UserID", value); }
        }

        //是否超级管理员
        public static bool IsAtawSuperUser
        {
            get
            {
                if (UserFID == "ataws" || UserFID == "1001")
                    return true;
                else
                    return false;
            }
        }

        public static bool IsAuthenticated
        {
            get
            {
                return Context.User.Identity.IsAuthenticated;
                   // &&  !(UserFID.IsEmpty());
            }
        }

        public static void AuthorzationPageCheck()
        {
            if (!IsAuthenticated)
                Response.Redirect("~/home/login");
        }

        public static void AuthorzationInitialization(Func<BaseLogonUserInfo> error, IAuthorzation customAuthorzation)
        {
            IAuthorzation authorzation = null;
            try
            {
                if (customAuthorzation == null)
                {
                    authorzation = new AtawAuthorzation();
                }
                AuthorzationInitialization(authorzation);
            }
            catch
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Context.Items[Key_ContextName] = error();
                }
            }


        }

        public static void AuthorzationInitialization(IAuthorzation authorzation)
        {
            //HttpContext.Current.
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string loginId = authorzation.GetLogonId();
                Context.Items[Key_ContextName] = authorzation.GetLogonUserInfo(loginId);
            }
        }

        internal static void AssertHttpContext()
        {
        }

        internal static void CreateFunRights(RegNameList<FunRightItem> funRights)
        {
            Context.Items[Key_RightName] = funRights;
        }
    }
}
