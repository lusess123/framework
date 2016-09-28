using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Ataw.Framework.Core;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ataw.Framework.Web
{
    public abstract class AtawBaseController : System.Web.Mvc.Controller
    {

      

        private DateTime BeginTime { get; set; }

        protected IPageViewTool GetPageViewTool(ModuleConfig mc)
        {
            if (mc.Interceptor.IsEmpty())
            {
                return "EmptyPageViewTool".CodePlugIn<IPageViewTool>();

            }
            else
                return mc.Interceptor.CodePlugIn<IPageViewTool>();
        }

        protected string Xml(string xml)
        {
            //if(xml)

            if (xml.IndexOf(".xml") == -1)
                xml = xml + ".xml";
            return xml;
        }

        public DataSet PostDataSet
        {
            get;
            set;
        }

        public string FControlUnitID
        {
            get { return AtawAppContext.Current.FControlUnitID; }
        }

        public AtawBaseController()
        {
            this.BeginTime = DateTime.Now;
            AtawAppContext.Current.SetItem<DateTime>("__beginTime", DateTime.Now);
            //var pageItems = AtawAppContext.Current.PageFlyweight.PageItems;
            //pageItems["IsAuthenticated"] = GlobalVariable.IsAuthenticated;
            //pageItems["FControlUnitID"] = GlobalVariable.FControlUnitID;
            //pageItems["UserId"] = GlobalVariable.UserId;
            //pageItems["UserName"] = CookieUtil.Get("UserName");
            //pageItems["NickName"] = CookieUtil.Get("NickName");
        }

        public string FastJson(object obj)
        {
            // JsonConvert.SerializeObject(obj);
            return AtawAppContext.Current.FastJson.ToJSON(obj);
        }
        public T FastToObject<T>(string json)
        {
            return AtawAppContext.Current.FastJson.ToObject<T>(json);
        }


        public AtawBaseController(IAuthorzation authorzation)
            : this()
        {
            GlobalVariable.AuthorzationInitialization(() => new BaseLogonUserInfo(), authorzation);
        }

        protected void SetPostDataSet(string ds)
        {
            string str = string.Format(ObjectUtil.SysCulture, "{0}{1}{2}", Environment.NewLine, ds, Environment.NewLine);
           // GlobalVariable.AppContext.Logger.Debug(str, "post数据");
            AtawTrace.WriteFile(LogType.Post, str);
            if (!ds.IsEmpty())
            {
                PostDataSet = JsonConvert.DeserializeObject<DataSet>(ds);
            }
            else {
                PostDataSet = new DataSet();
            }
        }

        public string RowNumForm(string ds, string xml, string form)
        {
            xml = Xml(xml);
            AtawDebug.AssertNotNullOrEmpty(xml, "亲 ,modulexml 注册名不可以为空的", this);
            ModuleConfig mc = xml.SingletonByPage<ModuleConfig>();
            var tool = GetPageViewTool(mc);
            tool.BeginSearchFormInterceptor(ref ds, ref xml, ref form, ref mc);
            mc = xml.SingletonByPage<ModuleConfig>();
            AtawBasePageViewCreator pageCreator = ("ModulePageView").SingletonByPage<AtawBasePageViewCreator>();
            pageCreator.Initialize(mc, JsonConvert.DeserializeObject<DataSet>(ds ?? ""), "", form, false);
            var apcv = pageCreator.Create();
            apcv.RegName = xml;

            return tool.EndSearchFormInterceptor(apcv);
            //return "";
        }


        public string SearchForm(string ds, string xml, string form)
        {
            xml = Xml(xml);
            AtawDebug.AssertNotNullOrEmpty(xml, "亲 ,modulexml 注册名不可以为空的", this);
            ModuleConfig mc = xml.SingletonByPage<ModuleConfig>();
            var tool = GetPageViewTool(mc);
            tool.BeginSearchFormInterceptor(ref ds, ref xml, ref form,ref mc);
            mc = xml.SingletonByPage<ModuleConfig>();
            AtawBasePageViewCreator pageCreator = ("ListPageView").SingletonByPage<AtawBasePageViewCreator>();
            pageCreator.Initialize(mc, JsonConvert.DeserializeObject<DataSet>(ds ?? ""), "", form, false);
            var apcv = pageCreator.Create();
            apcv.RegName = xml;

            return tool.EndSearchFormInterceptor(apcv);
            //return "";
        }

        public string ModulePage(string ds, string xml, string keyValue)
        {
            xml = Xml(xml);
            AtawDebug.AssertNotNullOrEmpty(xml, "亲 ,modulexml 注册名不可以为空的", this);
            ModuleConfig mc = xml.SingletonByPage<ModuleConfig>();
            mc.Mode = ModuleMode.None;
            var tool = GetPageViewTool(mc);
            tool.BeginModulePageInterceptor(ref ds, ref xml, ref keyValue, ref mc);

            AtawBasePageViewCreator pageCreator = ("Module" + "PageView").SingletonByPage<AtawBasePageViewCreator>();
            pageCreator.Initialize(mc, JsonConvert.DeserializeObject<DataSet>(ds ?? ""), keyValue, "", false);
            var apcv = pageCreator.Create();
            apcv.RegName = xml;

            return tool.EndModulePageInterceptor(apcv);
        }

        protected AtawPageConfigView GetModule(string ds, string xml, string pageStyle, string keyValue)
        {
            BeginTime = DateTime.Now;
            try
            {
                xml = Xml(xml);
                AtawDebug.AssertNotNullOrEmpty(xml, "亲 ,modulexml 注册名不可以为空的", this);
                ModuleConfig mc = xml.PlugGet<ModuleConfig>();
                if (mc.Mode == ModuleMode.None)
                {
                    throw new AtawException("ModuleXml的Mode节点不能为空", this);
                }

                var tool = GetPageViewTool(mc);
                tool.BeginModuleInterceptor(ref ds, ref xml, ref pageStyle, ref keyValue, ref mc);

                //if (!AtawAppContext.Current.IsAuthenticated)
                //{
                //    JsResponseResult<object> ree = new JsResponseResult<object>()
                //    {
                //        ActionType = JsActionType.Alert,
                //        Content = "请登录，匿名暂不开放...."
                //    };
                //    return FastJson(ree);
                //}
                mc = xml.SingletonByPage<ModuleConfig>();
                if (!AtawBasePageViewCreator.IsSupportPage(mc.SupportPage, pageStyle.Value<PageStyle>()))
                {
                    JsResponseResult<object> ree = new JsResponseResult<object>()
                    {
                        ActionType = JsActionType.Alert,
                        Content = "无权访问该页面"
                    };
                    return null;
                }
                bool isXml2Db = AtawAppContext.Current.ApplicationXml.IsMigration && !mc.IsNoDb;
                if (isXml2Db && mc.DataBase == null)
                {
                    mc.Forms.Cast<FormConfig>().ToList().ForEach(a =>
                    {
                        var dataForm = a.File.XmlConfig<DataFormConfig>();
                        AtawAppContext.Current.Xml2Db.Migrations(dataForm);
                    }
                        );
                }

                if (isXml2Db)
                {
                    AtawTrace.WriteFile(LogType.DatabaseStructure, AtawAppContext.Current.Xml2Db.GetLogMessage());
                    var dbContext = AtawAppContext.Current.UnitOfData;
                    if (dbContext != null)
                    {
                        AtawAppContext.Current.UnitOfData.Submit();
                        AtawAppContext.Current.UnitOfData = null;
                    }
                }

                AtawBasePageViewCreator pageCreator = (pageStyle + "PageView").PlugGet<AtawBasePageViewCreator>();
                pageCreator.Initialize(mc, JsonConvert.DeserializeObject<DataSet>(ds ?? ""), keyValue, "", false);
                var apcv = pageCreator.Create();
                apcv.RegName = xml;

                //清空数据.....
                mc.Forms.Cast<FormConfig>().ToList().ForEach(a =>
                    {
                        string name = a.DataPlug;
                        AtawAppContext.Current.PageFlyweight.Remove<IListDataTable>(a.DataPlug, a.Name);
                        

                    });

                return (apcv);
            }
            catch (Exception ex)
            {
                RecoredException(ex);
                AtawPageConfigView apcv = new AtawPageConfigView();
                apcv.Header = new PageHeader();
                apcv.Header.IsValid = false;
                apcv.Header.Message = "<h2>系统出现异常，请跟管理员联系！</h2><p>异常信息是:{0}</p>".AkFormat(ex.Message);
                return (apcv);
            }

        }


        public string Module(string ds, string xml, string pageStyle, string keyValue)
        {
            try
            {
                xml = Xml(xml);
                AtawDebug.AssertNotNullOrEmpty(xml, "亲 ,modulexml 注册名不可以为空的", this);
                ModuleConfig mc = xml.SingletonByPage<ModuleConfig>();
                if (mc.Mode == ModuleMode.None)
                {
                    throw new AtawException("ModuleXml的Mode节点不能为空", this);
                }

                var tool = GetPageViewTool(mc);
                tool.BeginModuleInterceptor(ref ds, ref xml, ref pageStyle, ref keyValue, ref mc);

                //if (!AtawAppContext.Current.IsAuthenticated)
                //{
                //    JsResponseResult<object> ree = new JsResponseResult<object>()
                //    {
                //        ActionType = JsActionType.Alert,
                //        Content = "请登录，匿名暂不开放...."
                //    };
                //    return FastJson(ree);
                //}
                mc = xml.SingletonByPage<ModuleConfig>();
                if (!AtawBasePageViewCreator.IsSupportPage(mc.SupportPage, pageStyle.Value<PageStyle>()))
                {
                    JsResponseResult<object> ree = new JsResponseResult<object>()
                    {
                        ActionType = JsActionType.Alert,
                        Content = "无权访问该页面"
                    };
                    return FastJson(ree);
                }
                bool isXml2Db = AtawAppContext.Current.ApplicationXml.IsMigration && !mc.IsNoDb;
                if (isXml2Db && mc.DataBase == null)
                {
                    mc.Forms.Cast<FormConfig>().ToList().ForEach(a =>
                    {
                        var dataForm = a.File.XmlConfig<DataFormConfig>();
                        AtawAppContext.Current.Xml2Db.Migrations(dataForm);
                    }
                        );
                }

                if (isXml2Db)
                {
                    AtawTrace.WriteFile(LogType.DatabaseStructure, AtawAppContext.Current.Xml2Db.GetLogMessage());
                    var dbContext = AtawAppContext.Current.UnitOfData;
                    if (dbContext != null)
                    {
                        AtawAppContext.Current.UnitOfData.Submit();
                        AtawAppContext.Current.UnitOfData = null;
                    }
                }

                AtawBasePageViewCreator pageCreator = (pageStyle + "PageView").SingletonByPage<AtawBasePageViewCreator>();
                pageCreator.Initialize(mc, JsonConvert.DeserializeObject<DataSet>(ds ?? ""), keyValue, "", false);
                var apcv = pageCreator.Create();
                apcv.RegName = xml;


                return tool.EndModuleInterceptor(apcv);
            }
            catch (Exception ex)
            {
                RecoredException(ex);
                AtawPageConfigView apcv = new AtawPageConfigView();
                apcv.Header = new PageHeader();
                apcv.Header.IsValid = false;
                apcv.Header.Message ="<h2>系统出现异常，请跟管理员联系！</h2><p>异常信息是:{0}</p>".AkFormat( ex.Message);
                return ReturnJson(apcv);
            }
            //return ReturnJson(apcv);
        }

        public virtual string DelByModuleForm(string xml, string form, string ids)
        {
            xml = Xml(xml);
            if (form.IsEmpty())
            { 
                //----------
                ModuleConfig config = xml.SingletonByPage<ModuleConfig>();
                 var _form = config.Forms.First();
                 if (_form is FormConfig)
                 {
                     form = ((FormConfig)_form).TableName;
                 }
                 else
                 {
                     form = _form.Name;
                 }
            }
            string[] _idList = JsonConvert.DeserializeObject<string[]>(ids);
            DataSet ds = DataSetUtil.SetDelDataSet(form, _idList);
            string res = JsonConvert.SerializeObject(ds);

            string mres = ModuleMerge(res, xml, "Update");
            return mres;
        }


        public virtual string ModuleMerge(string ds, string xml, string pageStyle)
        {
            DateTime d1 = DateTime.Now;
            //try
            //{
                xml = Xml(xml);
                IPageViewTool tool = null;
                List<Func<SubmitData, SubmitData>> resultFunList = new List<Func<SubmitData, SubmitData>>();
                Merge(ds, xml, pageStyle, out tool, resultFunList);
                int res = 0;
                var dbContext = AtawAppContext.Current.UnitOfData;
                DateTime d0 = DateTime.Now;
                if (dbContext != null)
                {
                    res = AtawAppContext.Current.UnitOfData.Submit();
                }
                
                
                
                int objRes = (AtawAppContext.Current.PageFlyweight.PageItems["ATAW_DATASOURCE_RETURN"] ?? "0").Value<int>();
                if (objRes > 0)
                    res = objRes;
                var insertKeys = AtawAppContext.Current.PageFlyweight.PageItems["InsertKeys"] as List<string>;
                //DateTime d2 = DateTime.Now;
                //string dd = (d2 - d0).TotalMilliseconds.ToString();
                //if (insertKeys != null)
                //{
                //    return ReturnJsonObject("{\"res\":" + res + ",\"keys\":\"" + String.Join(",", insertKeys) + "\"}");
                //}
                SubmitData data = new SubmitData()
                {
                     DataSourceInt = objRes ,
                      SubmitInt = res ,
                       InsertKeys = insertKeys,
                        Result = null
                };
                foreach (Func<SubmitData, SubmitData> fun in resultFunList)
                {
                    data =  fun(data);
                }

                if (data.Result != null)
                {
                    string str = FastJson(data.Result);
                    AtawTrace.WriteFile(LogType.JsonData, str);
                    HttpContext.Response.HeaderEncoding = Encoding.UTF8;
                    return str;
                }
                else
                {

                    //return ReturnJsonObject("{\"res\":" + res + ",\"keys\":\"\"}");
                    return tool.EndModuleMergeInterceptor(res, objRes, insertKeys);
                }

           // }
            //catch (AtawLegalException ex)
            //{
            //    JsResponseResult<object> ree = new JsResponseResult<object>()
            //        {
            //            ActionType = JsActionType.Alert,
            //            Content = ex.Message
            //        };
            //    return FastJson(ree);
            //}
            //return FastJson(source.Result);
            //foreach(Da)
        }

        protected void Merge(string ds, string xml, string pageStyle,out IPageViewTool toolOut, List<Func<SubmitData, SubmitData>> resultFunList)
        {
            xml = Xml(xml);
            ModuleConfig config = xml.SingletonByPage<ModuleConfig>();

            var tool = GetPageViewTool(config);
            toolOut = tool;
            tool.BeginModuleMergeInterceptor(ref ds, ref xml, ref pageStyle, ref config);
            config = xml.SingletonByPage<ModuleConfig>();
            //FormConfig fc = config.Forms[0];
            //string plugName = fc.DataPlug;
            //IListDataTable source = plugName.PlugInPageGet<IListDataTable>();
            if (config.DataBase != null)
            {
                var app = AtawAppContext.Current;

                app.UnitOfData = config.DataBase.FetchUnitofData();

            }
            SetPostDataSet(ds);
            var forms = config.Forms;
            //int result = 0;
            //AtawDbContext dbContext = null;
            string foreignKey = "";
            forms.Where(a => a is FormConfig).Cast<FormConfig>().ToList().ForEach(form =>
            {
                //if (form.Action == PageStyle.Insert || form.Action == PageStyle.Update)
                //{
                IListDataTable source = form.DataPlug.InstanceByPage<IListDataTable>(form.Name);
                if (config.Mode == ModuleMode.MasterDetail)
                {
                    var relation = config.Relations.FirstOrDefault(a => a.DetailForm == form.Name);
                    foreignKey = relation == null ? "" : relation.DetailField;
                }

                var dataForm = form.File.InstanceByPage<DataFormConfig>(form.Name);
                if (dataForm.PrimaryKey.IsEmpty())
                {
                    dataForm.PrimaryKey = dataForm.Columns.First(col => col.IsKey).Name;
                }
                if (pageStyle.IsEmpty())
                {
                    pageStyle = form.Action.ToString();
                }

                List<ColumnConfig> _fullColumns = null;


                AtawBaseFormViewCreator.MergeColumns(form, dataForm, config, pageStyle.Value<PageStyle>(), out _fullColumns);

                source.Initialize(new ModuleFormInfo(this.PostDataSet, 0, "", "", form.TableName, "", foreignKey,
                    false, dataForm, form.OrderSql, config.DataBase, form)
                    {
                        FullColumns = _fullColumns
                    }
                    );
                source.Merge(true);
                resultFunList.Add(source.SubmitFilterFun);
               // source.SubmitFilterEvent(null);
                //}
                //if (dbContext == null && source is BaseDataTableSource)
                //{
                //    dbContext = ((BaseDataTableSource)source).DbContext;
                //}
            });
        }

        SubmitData source_SubmitFilterEvent(ResponseResult arg)
        {
            throw new NotImplementedException();
        }

        private ActionResult ReDirectAction(string name, object[] parameters)
        {
            Type p = this.GetType();
            MethodInfo m = p.GetMethod(name);
            AtawDebug.AssertNotNull(m, string.Format(ObjectUtil.SysCulture, "不能找到名为{0}的Action", name), this);
            var result = m.Invoke(this, null);
            if (result is string)
                return new ContentResult() { Content = result.ToString() };
            else
                return result as ActionResult;


        }

        public ActionResult PostDs(string ds, string name)
        {
            SetPostDataSet(ds);
            return ReDirectAction(name, null);
        }

        protected string ReturnJson<T>(T res)
        {
            JsResponseResult<T> ree = new JsResponseResult<T>()
            {
                ActionType = JsActionType.Object,
                Obj = res
            };
            ree.EndTimer = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
            ree.BeginTime = AtawAppContext.Current.GetItem("__beginTime").Value<DateTime>().ToString("yyyy/MM/dd HH:mm:ss.ffff");
            //  ree.SaveString(System.Xml.Formatting.Indented);
            // string str = JsonConvert.SerializeObject(ree);
            string str = FastJson(ree);
            AtawTrace.WriteFile(LogType.JsonData, str);
            HttpContext.Response.HeaderEncoding = Encoding.UTF8;
            //HttpContext.Response.
         
            return str;
        }
        protected string ReturnJsonObject<T>(T res)
        {
            JsResponseResult<T> ree = new JsResponseResult<T>()
            {
                ActionType = JsActionType.JsonObject,
                Obj = res
            };
            //  ree.SaveString(System.Xml.Formatting.Indented);
            // string str = JsonConvert.SerializeObject(ree);
            string str = FastJson(ree);
            AtawTrace.WriteFile(LogType.JsonData, str);
            HttpContext.Response.HeaderEncoding = Encoding.UTF8;

            ree.EndTimer = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
            ree.BeginTime = AtawAppContext.Current.GetItem("__beginTime").Value<DateTime>().ToString("yyyy/MM/dd HH:mm:ss.ffff");
            //HttpContext.Response.
            return str;
        }

        private static StringBuilder SetException(Exception ex, StringBuilder sb, bool hasStack)
        {
            string mesg = "";
            //AtawAppContext.Current.ApplicationXml.ExceptionStack
            if (hasStack)
            {
                mesg = string.Format(ObjectUtil.SysCulture,
                 "程序出现异常,异常信息是:{0}{1}{0}堆栈信息是：{0}{2}",
                   Environment.NewLine, ex.Message, ex.StackTrace);
            }
            else
            {
                mesg = string.Format(ObjectUtil.SysCulture,
                 "程序出现异常,异常信息是:{0}{1}{0}",
                 Environment.NewLine, ex.Message);
            }
            sb.Append(mesg);
            if (ex.InnerException != null)
            {
                sb.Append(Environment.NewLine);
                SetException(ex.InnerException, sb, hasStack);
            }
            return sb;
        }

        public class LegalPageView
        {
            public bool LegalException = true;
            public string Error
            {
                get;
                set;
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            JsResponseResult<object> ree = null;
            if (exception is AtawLegalException)
            {
              ree = new JsResponseResult<object>()
               {
                   ActionType = JsActionType.Object,
                   Obj = new LegalPageView() { 
                        Error = exception.Message
                   }
                   // Content =

               };
            }
            else
            {
                 ree = new JsResponseResult<object>()
                {
                    ActionType = JsActionType.Alert
                    // Content =

                };
            }
            StringBuilder sb = new StringBuilder();
            SetException(exception, sb, AtawAppContext.Current.ApplicationXml.ExceptionStack);
            ree.Content = sb.ToString();

            AtawTrace.WriteFile(LogType.Error, SetException(exception, new StringBuilder(), true).ToString());
            // this.ControllerContext.Controller.Co
            filterContext.Result = new ContentResult() { Content = FastJson(ree) };
           // AtawAppContext.Current.Logger.Debug(string.Format(ObjectUtil.SysCulture, "{0}/r/n{1}", exception.Message, exception.StackTrace));

            base.OnException(filterContext);
        }

        private void RecoredException(Exception exception)
        {
            StringBuilder sb = new StringBuilder();
            AtawTrace.WriteFile(LogType.Error, SetException(exception,sb, true).ToString());
            //SetException(exception, sb, AtawAppContext.Current.ApplicationXml.ExceptionStack);
            AtawAppContext.Current.Logger.Debug(string.Format(ObjectUtil.SysCulture, "{0}/r/n{1}", exception.Message, exception.StackTrace));
            
        }

        protected void SetItem<T>(string key, T obj)
        {
            AtawAppContext.Current.PageFlyweight.PageItems[key] = obj;
        }

        protected object GetItem(string key)
        {
            return AtawAppContext.Current.PageFlyweight.PageItems[key];
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (PostDataSet != null)
                    PostDataSet.Dispose();
            }
            base.Dispose(disposing);
        }

        private static string SetViewName(string actionName)
        {
            string _theme = "ViewTheme".AppKv<string>(string.Empty);
            if (!_theme.IsAkEmpty())
            {
                actionName = actionName.ToUpper();
                if (actionName.IndexOf('.') > 0)
                {
                    actionName = actionName.Replace(".CSHTML", "_{0}.CSHTML".AkFormat(_theme));
                    //return View(actionName);
                }
                else
                {
                    actionName = "{0}_{1}".AkFormat(actionName, _theme);
                }
            }
            return actionName;
 
        }
        protected ViewResult AkView(string actionName, object model)
        {
            string _viewName = SetViewName(actionName);
            return View(_viewName,model);
        }

        protected ViewResult AkView(string actionName)
        {
            string _viewName = SetViewName(actionName);
            return View(_viewName);
        }

    }
}
