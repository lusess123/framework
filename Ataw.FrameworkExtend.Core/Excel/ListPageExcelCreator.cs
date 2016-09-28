using Ataw.Framework.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.FrameworkExtend.Core.Excel
{
       [CodePlug("ListPageExcelCreator", BaseClass = typeof(IExcelCreator),
   CreateDate = "2016-01-15", Author = "zhengyk", Description = "")]
    public class ListPageExcelCreator : IExcelCreator
    {

        protected string ModuleXml
        {
            get;
            set;
        }

        private IPageViewTool GetPageViewTool(ModuleConfig mc)
        {
            if (mc.Interceptor.IsEmpty())
            {
                return "EmptyPageViewTool".CodePlugIn<IPageViewTool>();

            }
            else
                return mc.Interceptor.CodePlugIn<IPageViewTool>();
        }


        private string Xml(string xml)
        {
            //if(xml)

            if (xml.IndexOf(".xml") == -1)
                xml = xml + ".xml";
            return xml;
        }

        public ListPageExcelCreator() {
           

            NameValueCollection nvc = (NameValueCollection)AtawAppContext.Current.GetItem("querystring");



            string ds = nvc["ds"] ?? "";
            string keyValue = nvc["keyvalue"]??"" ;
            ModuleXml = nvc["xml"]??"";
            string xml = ModuleXml;
           // xml = Xml(xml);
            string pageStyle = nvc["pageStele"]??"List";

            xml = Xml(xml);
            AtawDebug.AssertNotNullOrEmpty(xml, "亲 ,modulexml 注册名不可以为空的", this);
            ModuleConfig mc = xml.PlugGet<ModuleConfig>();
            if (mc.Mode == ModuleMode.None)
            {
                throw new AtawException("ModuleXml的Mode节点不能为空", this);
            }

            var tool = GetPageViewTool(mc);
            tool.BeginModuleInterceptor(ref ds, ref xml, ref pageStyle, ref keyValue, ref mc);

           
            mc = xml.SingletonByPage<ModuleConfig>();
            if (!AtawBasePageViewCreator.IsSupportPage(mc.SupportPage, pageStyle.Value<PageStyle>()))
            {
                JsResponseResult<object> ree = new JsResponseResult<object>()
                {
                    ActionType = JsActionType.Alert,
                    Content = "无权访问该页面"
                };
               // return null;
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


            var __apcv = apcv as AtawListPageConfigView;
         string _formName = __apcv.ListFormName;
          string _tableName =  __apcv.Forms[_formName].TableName ;

           this.Table = apcv.Data.Tables[_tableName];
            apcv.createFillDataSet(__apcv.ListFormName);
            this.ColumnList = new List<ColumnItem>();
            apcv.Forms[_formName].Columns.ForEach((col) =>
            {
                if (col.ControlType != ControlType.Hidden)
                {
                    this.ColumnList.Add(new ColumnItem()
                    {
                        Name = col.Name,
                        Title = col.DisplayName
                    });
                }
            
            });

            this.FileName =  __apcv.Title +  "列表";

        }

        public List<ColumnItem> ColumnList
        {
            get;
            set;
        }

        public DataTable Table
        {
            get;
            set;
        }

        public string TableName
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }
    }
}
