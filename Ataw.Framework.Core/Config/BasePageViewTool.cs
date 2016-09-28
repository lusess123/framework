using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
   public class BasePageViewTool:IPageViewTool
    {

       //private string fMethodName;
       private string fXml;
       private string fDs;
       private string fPageStyle;
       private string fKeyValue;
       private string fForm;
        //public string MethodName
        //{
        //    get { return fMethodName; }
        //}

        public virtual string Xml
        {
            get { return fXml; }
        }

        public virtual string Ds
        {
            get { return fDs; }
        }

        public virtual string PageStyle
        {
            get { return fPageStyle; }
        }

        public virtual  string KeyValue
        {
            get { return fKeyValue; }
        }

        public virtual string Form
        {
            get { return fForm; }
        }

        private ModuleConfig fModuleObj;
        public virtual ModuleConfig ModuleObj
        {
            get { return fModuleObj; }
        }

        public virtual void BeginModuleInterceptor(ref string ds, ref string xml, ref string pageStyle, ref string keyValue, ref  ModuleConfig moduleObj)
        {
            fDs = ds;
            fXml = xml;
            fPageStyle = pageStyle;
            fKeyValue = keyValue;
            fModuleObj = moduleObj;
        }

        public virtual void BeginModulePageInterceptor(ref string ds, ref string xml, ref string keyValue, ref  ModuleConfig moduleObj)
        {
            fDs = ds;
            fXml = xml;
            fKeyValue = keyValue;
            fModuleObj = moduleObj;
        }

        public virtual void BeginModuleMergeInterceptor(ref string ds, ref string xml, ref string pageStyle, ref  ModuleConfig moduleObj)
        {
            fDs = ds;
            fXml = xml;
            fPageStyle = pageStyle;
            fModuleObj = moduleObj;
        }

        public virtual string EndModuleInterceptor(AtawPageConfigView view)
        {
            return ReturnJsonObject(view);
        }

        public virtual string EndModulePageInterceptor(AtawPageConfigView view)
        {
            return ReturnJsonObject(view);
        }

        public virtual string EndModuleMergeInterceptor(int submitInt, int dataSourceInt, IEnumerable<string> insertKeys)
        {
            if (dataSourceInt > 0)
            {
                submitInt = dataSourceInt;
            }
            string _keys;
            if (insertKeys == null)
            {
                _keys = "";
            }
            else
            {
                _keys = string.Join(",", insertKeys);
            }
            var p = new { res = submitInt, keys = _keys };
            return ReturnJsonObject(p);
            //throw new NotImplementedException();
        }

        protected  string ReturnJsonObject<T>(T res)
        {
            JsResponseResult<T> ree = new JsResponseResult<T>()
            {
                ActionType = JsActionType.Object,
                Obj = res
            };
            //  ree.SaveString(System.Xml.Formatting.Indented);
            // string str = JsonConvert.SerializeObject(ree);
            ree.EndTimer = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
            ree.BeginTime = AtawAppContext.Current.GetItem("__beginTime").Value<DateTime>().ToString("yyyy/MM/dd HH:mm:ss.ffff");
            string str = AtawAppContext.Current.FastJson.ToJSON(ree);
            AtawTrace.WriteFile(LogType.PageViewData, str);



           // HttpContext.Response.HeaderEncoding = Encoding.UTF8;
            //HttpContext.Response.
            return str;
        }


        public virtual void BeginSearchFormInterceptor(ref string ds, ref string xml, ref string form, ref  ModuleConfig moduleObj)
        {
           // throw new NotImplementedException();
            fModuleObj = moduleObj;
            fDs = ds;
            fXml = xml;
            fForm = form;
            fPageStyle = "List";
        }

        public virtual string EndSearchFormInterceptor(AtawPageConfigView view)
        {
            return ReturnJsonObject(view);
        }

    }
}
