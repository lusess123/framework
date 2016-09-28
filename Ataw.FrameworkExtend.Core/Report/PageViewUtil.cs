using Ataw.Framework.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ataw.FrameworkExtend.Core.Report
{
    public class PageViewUtil
    {
        public void createFillDataSet(AtawPageConfigView _this, string formName)
        {
            AtawFormConfigView _form = _this.Forms[formName];
            DataTable dt = _this.Data.Tables[_form.TableName];
            foreach (DataRow row in dt.Rows)
            {
                _form.Columns.ForEach(col =>
                {
                    //-----------
                    string _val = row[col.Name].ToString();
                    if (!col.Options.RegName.IsEmpty())
                    {
                        //------------
                        string _indexName = col.Name + "_CODEINDEX";
                        if (row.Table.Columns.Contains(_indexName))
                        {
                            if (row[_indexName] is int[])
                            {
                                int[] _indexs = row[_indexName].Value<int[]>();
                                DataTable _codetable = _this.Data.Tables[col.Options.RegName];
                                List<string> _texts = new List<string>();
                                foreach (int index in _indexs)
                                {
                                    if (index >= 0)
                                    {
                                        DataRow _row = _codetable.Rows[index];

                                        string _text = _row["CODE_TEXT"].ToString();
                                        Regex reg = new Regex(@"<[^<>]+>");
                                        // string str = "_text";
                                        // ;
                                        bool isMath = true;
                                        while (isMath)
                                        {
                                            var math = reg.Match(_text);
                                            isMath = math.Success;
                                            if (isMath)
                                            {
                                                _text = _text.Replace(math.Value, "");
                                            }
                                        }

                                        _texts.Add(_text);
                                    }
                                }
                                //if()
                                if (row.Table.Columns[col.Name].DataType == typeof(string))
                                {
                                    row[col.Name] = _texts.JoinString(",");
                                }
                                else
                                {
                                    if (_texts.Count > 0)
                                    {
                                        row[col.Name] = _texts[0].Value<int>();
                                    }
                                }


                            }
                            else
                            {
                                int _index = row[_indexName].Value<int>();
                                DataTable _codetable = _this.Data.Tables[col.Options.RegName];
                                DataRow _row = _codetable.Rows[_index];
                                string _text = _row["CODE_TEXT"].ToString();
                                //-------------
                                row[col.Name] = _text;
                            }
                        }


                    }

                });
            }

            //----------
            //   return this.Data;
        }

        private string Xml(string xml)
        {
            //if(xml)

            if (xml.IndexOf(".xml") == -1)
                xml = xml + ".xml";
            return xml;
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

        public AtawPageConfigView getPageView(string xml, string pageStyle)
        {

            string ds = "";
            string keyValue = "";
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
            // AtawAppContext.Current.FastJson.ToJSON(ds ?? "");

            pageCreator.Initialize(mc, ds.SafeJsonObject<DataSet>(), keyValue, "", false);
            var apcv = pageCreator.Create();
            apcv.RegName = xml;
            return apcv;
        }



    }
}
