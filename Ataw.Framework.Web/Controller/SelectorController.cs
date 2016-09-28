using System.Collections.Generic;
using System.Linq;
using Ataw.Framework.Core;
using System.Data;
using Ataw.Framework.Core.Instance;
using System.Web;
using Newtonsoft.Json;
namespace Ataw.Framework.Web
{
    public class SelectorController : AtawBaseController
    {

        public class SelectorModel
        {
            public List<CodeDataModel> List { get; set; }
            public int Total { get; set; }
            public int Index { get; set; }
            public int Size { get; set; }
        }





        private CodeTable<CodeDataModel> GetCodeTableInstance(string regName)
        {
            CodeTable<CodeDataModel> rr = AtawIocContext.Current.FetchInstance<CodeTable<CodeDataModel>>(regName);
            //SetPostDataSet(ds);
            // rr.
            rr.Initialize(PostDataSet);
            return rr;
        }
        public string GetDataValueList(string regName, string key)
        {
            AtawDebug.AssertArgumentNullOrEmpty(regName, "亲，请求选择器数据源的注册名不可以为空哦", this);
            AtawDebug.AssertArgumentNullOrEmpty(regName, "亲，请求的值不可以为空哦", this);
            var dt = GetCodeTableInstance(regName);
            Dictionary<string, string> dict = new Dictionary<string, string>();

            string[] keys = key.Split(',');

            keys.ToList().ForEach((a) => {
                var _bean = dt[a];
                if(_bean != null){
                    dict.Add(_bean.CODE_VALUE,_bean.CODE_TEXT);
                }
               // dict.Add();
            });

            return ReturnJson(dict);
        }
        public string GetDataValue(string regName, string key)
        {
            AtawDebug.AssertArgumentNullOrEmpty(regName, "亲，请求选择器数据源的注册名不可以为空哦", this);
            AtawDebug.AssertArgumentNullOrEmpty(regName, "亲，请求的值不可以为空哦", this);
            var dt = GetCodeTableInstance(regName);
            string _res = dt[key].CODE_TEXT;

            return ReturnJson(_res);
        }

        public string FillData(string regName)
        {
            AtawDebug.AssertArgumentNullOrEmpty(regName, "亲，请求选择器数据源的注册名不可以为空哦", this);
            // regName = regName ?? "";
            var dt = GetCodeTableInstance(regName);
            var res = dt.FillData(null);
            return ReturnJson(res == null ? new List<CodeDataModel>() : res.ToList());
        }

        public string AutoComplete(string regName, string key, string ds)
        {
            SetPostDataSet(ds);
            key = key ?? "";
            var dt = GetCodeTableInstance(regName);
            var res = dt.BeginSearch(null, key);
            return ReturnJson(res == null ? new List<CodeDataModel>() : res.ToList());
        }

        public string Search(string regName, string key, string ds, int? pageSize, int? pageIndex)
        {
            var pagination = new Pagination();
            pagination.PageSize = pageSize ?? 28;
            pagination.PageIndex = pageIndex ?? 0;
            SetPostDataSet(ds);
            key = key ?? "";
            var dt = GetCodeTableInstance(regName);
            pagination.AppendToDataSet(PostDataSet, regName);
            var res = dt.Search(PostDataSet, key);
            //pagination.TotalCount = res.Count();
            SelectorModel model = new SelectorModel();
            model.List = res.ToList();
            model.Index = pagination.PageIndex;
            model.Size = pagination.PageSize;
            model.Total = PostDataSet.Tables[regName + "_PAGER"].Rows[0]["TotalCount"].Value<int>();

            //res = res.Skip((pagination.PageIndex - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
            return ReturnJson(model == null ? new SelectorModel() : model);
        }

        public string LoadPage(string regName, string pageStyle, string xml, string ds)
        {
            SetPostDataSet(ds);
            string moduleXml = xml;
            if (xml.IsEmpty())
            {
                var singleCodeTable = AtawIocContext.Current.FetchInstance<CodeTable<CodeDataModel>>(regName) as SingleCodeTable<CodeDataModel>;
                if (singleCodeTable != null)
                    moduleXml = singleCodeTable.ModuleXml;
                AtawDebug.AssertNotNull(moduleXml, string.Format("插件名为{0}的CodeTable需要配置ModuleXml", regName), this);
            }
            moduleXml = Xml(moduleXml);
            ModuleConfig mc = moduleXml.SingletonByPage<ModuleConfig>();

            var tool = GetPageViewTool(mc);
            // string ds = "";
            string keyValue = "";
            tool.BeginModuleInterceptor(ref ds, ref moduleXml, ref pageStyle, ref keyValue, ref mc);

            AtawBasePageViewCreator pageCreator = (pageStyle + "PageView").SingletonByPage<AtawBasePageViewCreator>();
            pageCreator.Initialize(mc, JsonConvert.DeserializeObject<DataSet>(ds ?? ""), "", "", false);
            var apcv = pageCreator.Create();
            apcv.RegName = moduleXml;
            //(apcv as AtawListPageConfigView).PageSelector = new PageSelector()
            //{

            //    ValueField = singleCodeTable.ValueField,
            //    TextField = singleCodeTable.TextField
            //};
            // return ReturnJson(apcv);
            return tool.EndModuleInterceptor(apcv);


        }

        public class ZTreeNode
        {
            public string id { get; set; }
            public string name { get; set; }

        }
        public string DeleteNode(string regName, string key)
        {
            var dt = GetCodeTableInstance(regName);
            var dbTreeCodeTable = dt as DbTreeCodeTable;
            return dbTreeCodeTable.DeleteNode(key);

        }
        public string DropNodes(string regName, string keys, string targetNodeId)
        {
            var dt = GetCodeTableInstance(regName);
            var dbTreeCodeTable = dt as DbTreeCodeTable;
            return dbTreeCodeTable.DropNodes(keys, targetNodeId);

        }

        public string LoadSubtree(string ztree, string ds, string regName, string CODE_VALUE)
        {
            SetPostDataSet(ds);
            var dt = GetCodeTableInstance(regName);
            TreeCodeTable cdm = dt as TreeCodeTable;
            if (!ztree.IsEmpty())
            {
                CODE_VALUE = (ztree.Split('=')[1]).ToString();
            }
            if (string.IsNullOrEmpty(CODE_VALUE))
            {
                CODE_VALUE = cdm.Root;
            }
            var res = cdm.GetChildrenNode(CODE_VALUE);
            //if (CODE_VALUE == "0")
            //FormaterTreeText(res);
            return FastJson(res.Children.ToArray());
            //  return FastJson(res);

        }

        public string LoadSubtreeFormatJson(string ztree, string ds, string regName, string CODE_VALUE)
        {
            SetPostDataSet(ds);
            var dt = GetCodeTableInstance(regName);
            TreeCodeTable cdm = dt as TreeCodeTable;
            if (!ztree.IsEmpty())
            {
                CODE_VALUE = (ztree.Split('=')[1]).ToString();
            }
            if (string.IsNullOrEmpty(CODE_VALUE))
            {
                CODE_VALUE = cdm.Root;
            }
            var res = cdm.GetChildrenNode(CODE_VALUE);
            //if (CODE_VALUE == "0")
            //FormaterTreeText(res);
            return ReturnJson(res.Children.ToArray());
            //  return FastJson(res);

        }

        private bool HasCodeValue(TreeCodeTable cdm, ref string key)
        {
            if (cdm.KeyValues != null && cdm.KeyValues.Count() > 0)
            {
                key = string.Join("", cdm.KeyValues);
                if (key.IsEmpty())
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public string LoadTree(string regName, string ds)
        {
            SetPostDataSet(ds);
            var dt = GetCodeTableInstance(regName);
            TreeCodeTable cdm = dt as TreeCodeTable;
            // cdm.Initialize(PostDataSet);
            TreeCodeTableModel result = null;
            string key = "";
            bool isCodeValue = HasCodeValue(cdm, ref key);
            if (isCodeValue)
            {
                result = cdm.GetDisplayTreeNode(key);
                if (result == null)
                    result = cdm.GetChildrenNode(cdm.Root);
            }
            else
            {
                result = cdm.GetChildrenNode(cdm.Root);
            }
            //FormaterTreeText(result);
            //if (result != null && result.Children != null)
            //{
            if (result.CODE_VALUE == "0")
            {
                return FastJson(result.Children.ToArray());
            }
            else
            {
                if (result.Children.Count() == 1)
                {
                    if (result.Children.First().CODE_VALUE == cdm.Root)
                    {
                        return FastJson(result.Children.ToArray());
                    }
                }
                result.CODE_TEXT = cdm[result.CODE_VALUE].CODE_TEXT;
                //result.open = true;
            }
            //}
            //else
            return FastJson(result);
            //return FastJson(result);

        }

        public string LoadAllTreeFormatJson(string regName, string ds) {
            SetPostDataSet(ds);
            var dt = GetCodeTableInstance(regName);
            TreeCodeTable cdm = dt as TreeCodeTable;
            // cdm.Initialize(PostDataSet);
            TreeCodeTableModel result = null;
            string key = "";

            bool isCodeValue = HasCodeValue(cdm, ref key);
            if (isCodeValue)
            {
                //result = cdm.GetDisplayTreeNode(key);
                //if (result == null)
                //    result = cdm.GetChildrenNode(cdm.Root);
                result = cdm.GetAllTree();
            }
            else
            {
                result = cdm.GetAllTree();
            }
            //FormaterTreeText(result);
            //if (result != null && result.Children != null)
            //{
            if (result.CODE_VALUE == "0")
            {
                return ReturnJson(result.Children.ToArray());
            }
            else
            {
                if (result.Children.Count() == 1)
                {
                    if (result.Children.First().CODE_VALUE == cdm.Root)
                    {
                        return ReturnJson(result.Children.ToArray());
                    }
                }
                result.CODE_TEXT = cdm[result.CODE_VALUE].CODE_TEXT;
                //result.open = true;
            }
            //}
            //else
            return ReturnJson(new TreeCodeTableModel[] { result });
        }

        public string LoadTreeFormatJson(string regName, string ds)
        {
            SetPostDataSet(ds);
            var dt = GetCodeTableInstance(regName);
            TreeCodeTable cdm = dt as TreeCodeTable;
            // cdm.Initialize(PostDataSet);
            TreeCodeTableModel result = null;
            string key = "";
            
            bool isCodeValue = HasCodeValue(cdm, ref key);
            if (isCodeValue)
            {
                result = cdm.GetDisplayTreeNode(key);
                if (result == null)
                    result = cdm.GetChildrenNode(cdm.Root);
            }
            else
            {
                result = cdm.GetChildrenNode(cdm.Root);
            }
            //FormaterTreeText(result);
            //if (result != null && result.Children != null)
            //{
            if (result.CODE_VALUE == "0")
            {
                return ReturnJson(result.Children.ToArray());
            }
            else
            {
                if (result.Children.Count() == 1)
                {
                    if (result.Children.First().CODE_VALUE == cdm.Root)
                    {
                        return ReturnJson(result.Children.ToArray());
                    }
                }
                result.CODE_TEXT = cdm[result.CODE_VALUE].CODE_TEXT;
                //result.open = true;
            }
            //}
            //else
            return ReturnJson(new TreeCodeTableModel[] { result });
            //return FastJson(result);

        }

        //private void FormaterTreeText(TreeCodeTableModel treeCodeTableModel)
        //{
        //    treeCodeTableModel.CODE_TEXT = HttpUtility.UrlDecode(treeCodeTableModel.CODE_TEXT);
        //    if (treeCodeTableModel.Children != null)
        //    {
        //        foreach (var child in treeCodeTableModel.Children)
        //        {
        //            FormaterTreeText(child);
        //        }
        //    }
        //}
        //public string LoadDataByPager(int page)
        //{
        //    List<object> list=new List<object>();
        //    list.Add(new { name = "这是第" + page + "页", value = page });
        //    list.Add(new { name = "这是第" + page + "页", value = page });
        //    list.Add(new { name = "这是第" + page + "页", value = page });
        //    list.Add(new { name = "这是第" + page + "页", value = page });
        //    list.Add(new { name = "这是第" + page + "页", value = page });
        //    list.Add(new { name = "这是第" + page + "页", value = page });
        //    list.Add(new { name = "这是第" + page + "页", value = page });
        //    list.Add(new { name = "这是第" + page + "页", value = page });
        //    list.Add(new { name = "这是第" + page + "页", value = page });
        //    return FastJson(list);
        //}
        public string LoadTree2(string id)
        {
            var list = new List<object>();
            //判断是否为空 如果为空则加载根节点 否则则加载其下的子节点
            if (!string.IsNullOrEmpty(id))
            {
                list.Add(new
                {
                    id = 8,
                    name = "襄阳市",
                    pid = id
                });
                list.Add(new
                {
                    id = 9,
                    name = "襄阳市",
                    pid = id
                });
                list.Add(new
                {
                    id = 10,
                    name = "襄阳市",
                    pid = id
                });
                list.Add(new
                {
                    id = 11,
                    name = "襄阳市",
                    pid = id
                });
                list.Add(new
                {
                    id = 12,
                    name = "襄阳市",
                    pid = id
                });
            }
            else
            {
                list.Add(new
                {
                    id = 0,
                    name = "全部",
                    pid = -1,
                    isParent = true
                });
                list.Add(new
                {
                    id = 1,
                    name = "浙江省",
                    pid = 0,
                    isParent = true
                });
                list.Add(new
                {
                    id = 2,
                    name = "湖北省",
                    pid = 0,
                    isParent = true
                });

                list.Add(new
                {
                    id = 3,
                    name = "湖北省",
                    pid = 1,
                    isParent = true
                });
                list.Add(new
                {
                    id = 4,
                    name = "杭州市",
                    pid = 1,
                    isParent = true
                });
                list.Add(new
                {
                    id = 5,
                    name = "宁波市",
                    pid = 1
                });
                list.Add(new
                {
                    id = 6,
                    name = "武汉市",
                    pid = 2,
                    isParent = true
                });
                list.Add(new
                {
                    id = 7,
                    name = "襄阳市",
                    pid = 2,
                    isParent = true
                });
            }

            return FastJson(list);
        }
    }
}
