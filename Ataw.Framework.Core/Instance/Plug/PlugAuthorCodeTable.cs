using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core
{
       [CodePlug("PlugAuthorCodeTable", BaseClass = typeof(CodeTable<CodeDataModel>),
       CreateDate = "2012-06-16", Author = "zhengyk", Description = "作者数据源")]
    public class PlugAuthorCodeTable : HashCodeTable
    {
        private Dictionary<string, HashCodeDataModel> fDataList;
        protected override Dictionary<string, HashCodeDataModel> DataList
        {
            get
            {
                if (fDataList == null)
                {
                    var _list = AtawIocContext.Current.PlugInModelList;
                    var strs = _list.Select(a => (a.Author ?? "").ToLower()).GroupBy(a => a).Select(a => new { key = a.Key, Count = a.Count().ToString() }).ToList();
                    var res = new Dictionary<string, HashCodeDataModel>();
                    strs.ForEach(a =>
                    {
                       
                        res.Add(a.key, new HashCodeDataModel()
                        {
                            CODE_TEXT = "{0}【{1}】".AkFormat((a.key.IsEmpty()?"无名氏":a.key),a.Count),
                            CODE_VALUE = a.key
                        });
                    }
                        );
                    fDataList = res;
                }
                return fDataList;
            }
            set
            {
                fDataList = value;
            }
        } 
    }
}
