using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Ataw.Framework.Core;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Collections.Specialized;

namespace Ataw.Framework.Web
{
   public class WebServiceController : AtawBaseController
    {


       public string MethodList(string resolvers,string data) {

           string[] list = resolvers.Split(',');
           Dictionary<string,IMethod> methods = new Dictionary<string,IMethod>();
           NameValueCollection nvl = getNameValueCollection();
           AtawDebug.AssertArgument(list.Length > 0, "resolvers","请求插件名集合不可以为空",this);
           for (int i = 0; i < list.Length; i++) {
               var _method = list[i].PlugGet<IMethod>();
               _method.Params = nvl;
               methods.Add(list[i],  _method);
           }
           //--------
           var _list =  methods.OrderBy(a=>a.Value.Order).ToList();
           _list.ForEach((a) => {
               a.Value.Before();
           });

           Dictionary<string ,object>  olist = new Dictionary<string ,object>();
           _list.ForEach((a) =>
           {
               a.Value.Exe();
               olist.Add(a.Key,a.Value.ResultObj);
           });

           int resInt = 0;
           var _db =  AtawAppContext.Current.PageFlyweight.Get<IUnitOfData>("MethodList_IUnitOfData");
           if (_db != null) {
               resInt = _db.Submit();
           }


           for (int i = 0; i < _list.Count; i++) {

              // var _obj = _list[i].Value.ResultObj;
               _list[i].Value.After(resInt, olist);

           }

           return   ReturnJson(olist);

       }

       private NameValueCollection getNameValueCollection()
       {
           NameValueCollection res = new NameValueCollection();
           NameValueCollection nv = Request.Form;
           string[] forms = nv.AllKeys;
           foreach (string f in forms)
           {
               res.Add(f, nv[f]);
           }


           NameValueCollection qu = Request.QueryString;
           string[] keys = qu.AllKeys;
           foreach (string key in keys)
           {
               if (res[key] == null)
               {
                   res.Add(key, qu[key]);
               }
           }
           return res;
       }

       public string Method(string resolver)
       {
          var _method =  resolver.PlugGet<IMethod>();     
          _method.Params = this.getNameValueCollection();
          _method.Exe();
          return   ReturnJson(  _method.ResultObj);
       }

       public ActionResult Error303()
       {

           if (GlobalVariable.IsAjax)
           {
               JsResponseResult<string> jsRes = new JsResponseResult<string>() { ActionType = JsActionType.Alert };
               jsRes.Content =  Request.RawUrl + " 未找到";
               ContentResult res = new ContentResult();
               res.Content = AtawAppContext.Current.FastJson.ToJSON(jsRes);
               return res;
           }


          string mvcName =   "303MVC".AppKv<string>("");
          if (!mvcName.IsEmpty())
          {
             var mvc = AtawAppContext.Current.MvcConfigXml.DataRoutes.Where(a => a.Name == mvcName).FirstOrDefault();
             if (mvc != null)
             {
                 return  RedirectToAction(mvc.ActionName,mvc.ControlName, new { area=mvc.AreaName});
             }
             else
             {
                 return new ContentResult() { Content = "配置的 303MVC 路由名称貌似没有找到" };
             }
          }
          
           return View("NotFound");
       }
    

       public string ToSisenMess(string userId, string mesg)
       {
           //Thread th = new Thread(() =>
           //{
               try
               {
                 //  string mesg = CreateSisenMess("zyk", DateTime.Now.ToString());
                   string path = AtawAppContext.Current.ApplicationXml.AppSettings["SisenMessNotifyPath"].Value;

                   FileUtil.VerifySaveFile(Path.Combine(path, "wf.sisenmsg"), mesg, Encoding.Unicode);
               }
               catch (Exception ex)
               {
                   string str = "触发到布谷鸟异常： {0} {1} ".AkFormat( Environment.NewLine, ex.Message);
                   //string str = "工作流触发到布谷鸟异常：" + path + ex.Message;
                   AtawAppContext.Current.Logger.Debug(str);
                   AtawTrace.WriteFile(LogType.IoError, str);
               }

           //});
           //// th.
           //th.Start();
           return "ok";
       }
    }
}
