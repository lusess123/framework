using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core.Instance.Plug
{

     [CodePlug("PlugBaseClassCodeTable", BaseClass = typeof(CodeTable<CodeDataModel>),
       CreateDate = "2012-06-16", Author = "zhengyk", Description = "插件数据源")]
    public class PlugBaseClassCodeTable : HashCodeTable
    {
        private Dictionary<string, HashCodeDataModel> fDataList; 
        protected override Dictionary<string, HashCodeDataModel> DataList
        {
            get
            {
                if (fDataList == null)
                {
                    var _list =   AtawIocContext.Current.PlugInModelList;
                    var strs =  _list.GroupBy(a => a.BaseType.Name).Select(a=>a.Key.ToString()).OrderBy(a=>a).ToList();
                   var res =  new Dictionary<string, HashCodeDataModel>() ;
                   strs.ForEach(a =>
                   {
                       res.Add(a, new HashCodeDataModel()
                       {
                           CODE_TEXT = a.Replace("Ataw.Framework.", "")
                                .Replace("Senparc.Weixin.QY.MessageHandlers.QyMessageHandler", "")
                                .Replace("Ataw.QYWS.Web.", ""),
                           CODE_VALUE = a
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
