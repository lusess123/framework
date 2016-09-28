using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Ataw.Framework.Core;
using Newtonsoft.Json;
namespace Ataw.Framework.Web
{
    public class ModuleController : AtawBaseController
    {
        private const string PAGESTYLE_ASSERT = "不存在 {0}  这样的PageStyle ,url参数写错了 请检查";
        public ActionResult Page(string pagestyle, string path)
        {
            path = path.Replace("$","/");
            bool isPageStyle = false;
            foreach (var en in Enum.GetValues(typeof(PageStyle)))
            {
                if (en.ToString().ToUpper() == pagestyle.ToUpper())
                {
                    pagestyle = en.ToString();
                    isPageStyle = true;
                    break;
                }
            }
            AtawDebug.Assert(isPageStyle, string.Format(ObjectUtil.SysCulture, PAGESTYLE_ASSERT, pagestyle), this);
            var flyw = AtawAppContext.Current.PageFlyweight.PageItems;
            flyw["pagestyle"] = pagestyle;
            flyw["path"] = path;

            //flyw["data"] = Module("", "", path, pagestyle, "").Encode();
           // return View("~/Areas/ProjectProgress/Views/Module.cshtml");
            return View("~/Views/Module.cshtml");
        }


        private ModuleConfig getModule(string xml)
        {
            return xml.XmlConfig<ModuleConfig>();
        }

        public string PostInsert()
        {
            return "";
        }


        public string test()
        {
            //ModuleConfig config = new ModuleConfig();
            //config.DataPlug = "PlugData";
            //config.ConfigXml = "form\\plugData.xml";
            //config.SaveFileByName("module\\plug.xml");
            //config. = "";

            return "test";
        }

        public int TestUpdate(string ds, string xml)
        {
            try
            {
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public string GetData(string key)
        {
            // dynamic dd = new ExpandoObject();
            var dt = key.CodePlugIn<IListDataTable>();
            var list = dt.List.ToList();
            var dd = DataSetUtil.ToDataTable(list, list[0].GetType());
            //IDictionary<string,IEnumerable<ObjectData>> res = new 
            //DataSetSource dsc = new DataSetSource();
            //dsc.AddDataTable(dt);
            //dsc.l
            // dsc.Add(dt);
            // return JsonConvert.SerializeObject(dd);
            return ReturnJson(dd);
        }

        public string ff()
        {
            string g = " [{" +
"RegName: \"Selector\"," +
"BassClassName: \"AtawOptionCreator\"," +
"InstanceClassName: \"AtawSelectorOptionCreator\"," +
"DllPath: \"Ataw.Framework.Core.dll\"," +
                //"Desc: \"选择器控件参数创建插件\"," +
                //"Author: \"zhengyk\"," +
"CreateDate: \"2012-11-15 00:00:00\"" +
"}]";
            var table = JsonConvert.DeserializeObject<DataTable>(g);

            List<PlugModel> list = DataSetUtil.FillModel<PlugModel>(table).ToList();


            string ss = JsonConvert.SerializeObject(list);
            return ss;

        }

        public string GetData2(string key)
        {
            // dynamic dd = new ExpandoObject();
            var dt = key.CodePlugIn<IListDataTable>();
            DataSet ds = new DataSet();

            dt.AppendTo(ds);
            // var list = dt.List.ToList();
            // var dd = DataSetUtil.ToDataTable(list, list[0].GetType());
            //IDictionary<string,IEnumerable<ObjectData>> res = new 
            //DataSetSource dsc = new DataSetSource();
            //dsc.AddDataTable(dt);
            //dsc.l
            // dsc.Add(dt);
            // return JsonConvert.SerializeObject(ds, "yyyy-MM-dd");
            // string ss = FastJson(ds);
            // var table = FastToObject<DataTable>(ss);
            string ss = JsonConvert.SerializeObject(ds.Tables[0]);
            var table = JsonConvert.DeserializeObject<DataTable>(ss);



            return FastJson(table);
            //return ReturnJson(ds);
        }

        public string ModuleXml2(string key)
        {
            //string path = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH, "module", key);
            //ModuleConfig config = XmlUtil.ReadFromFile<ModuleConfig>(path);


            //string cpath = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH, config.ConfigXml);
            //DataFormConfig dataFoem0 = XmlUtil.ReadFromFile<DataFormConfig>(cpath);
            ////view.DataForms.Add(dataFoem0);
            //PageViewModelCreator pvMCreator = new PageViewModelCreator();
            //pvMCreator.DataFormConfigs = new DataFormConfig[] { dataFoem0 };
            ////pvMCreator.DataSourceRegName = config.DataPlug;
            //var res = pvMCreator.Create();

            return ReturnJson("");
        }

        public string ModuleXml(string key)
        {
            string path = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH, "module", key);
            ModuleConfig config = XmlUtil.ReadFromFile<ModuleConfig>(path);
            //var dataSource = AtawIocContext.Current.FetchInstance<IListDataTable>(config.DataPlug);
            //ModuleView view = new ModuleView();
            //view.ObjectDatas = dataSource.List.ToList();
            //string cpath = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH, config.ConfigXml);
            //DataFormConfig dataFoem0 = XmlUtil.ReadFromFile<DataFormConfig>(cpath);
            //view.DataForms.Add(dataFoem0);

            //JsResponseResult<object> res = new JsResponseResult<object>();
            //res.ActionType = JsActionType.Object;
            //res.Obj = view;

            //return AtawAppContext.Current.FastJson.ToJSON(res);

            return "";

        }

        public string LoadDataByPager(int pageIndex = 1)
        {
            List<object> cols = new List<object>();
            cols.Add(new { DisplayName = "用户编号" });
            cols.Add(new { DisplayName = "用户名" });
            cols.Add(new { DisplayName = "年龄" });
            cols.Add(new { DisplayName = "性别" });
            cols.Add(new { DisplayName = "联系地址" });
            cols.Add(new { DisplayName = "电话" });
            cols.Add(new { DisplayName = "备注" });


            List<object> toolbar = new List<object>();
            toolbar.Add(new { DisplayName = "用户编号", Name = "UserId", ControlType = "Text" });
            toolbar.Add(new { DisplayName = "用户名", Name = "UserName", ControlType = "Date" });
            toolbar.Add(new { DisplayName = "地址", Name = "Address", ControlType = "Text" });

            List<object> rows = new List<object>();
            for (int i = 0; i < 15; i++)
            {
                rows.Add(new
                {
                    UserId = System.Guid.NewGuid(),
                    UserName = "张三疯00" + i,
                    Age = 25,
                    Gender = "男",
                    Address = "浙江省杭州市西湖区",
                    Tel = "1879789789",
                    Remark = "这是第" + pageIndex + "页",
                    Operate = new[] { "del", "detail", "edit" }
                });
            }

            List<object> pager = new List<object>();
            pager.Add(new { TOTAL = 100, INDEX = 0 });

            var obj = new
            {
                Data = new { PlugData = rows, PAGER = pager },
                Forms = new
                {
                    plugData = new
                    {
                        FormType = "Grid",
                        TableName = "Customer",
                        Title = "用户列表",
                        Columns = cols
                    },
                    SearchForm = new
                    {
                        FormType = "Normal",
                        TableName = "Customer",
                        Title = "条件查询",
                        Columns = toolbar
                    }
                }
            };

            return JSON.Instance.ToJSON(obj);

            //return ReturnJson(obj);
            // return FastJson(new { ActionType = "Object", Content = "", Obj = obj });

        }
    }
}
