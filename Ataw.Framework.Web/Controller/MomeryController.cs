using Ataw.Framework.Core;
using Ataw.Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Web.Controller
{
    public class MomeryModel
    {
        public List<string> List { get; set; }
        public int Total { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
    }

   // public class CodeModel

    public class MomeryController : AtawBaseController
    {
        private IMomery GetMomeryInstance(string regName)
        {
            IMomery rr = AtawIocContext.Current.FetchInstance<IMomery>(regName);
            //SetPostDataSet(ds);
            // rr.
           // rr.Initialize(PostDataSet);
            return rr;
        }

         public string AutoComplete(string regName, string key, string ds)
        {
            SetPostDataSet(ds);
            key = key ?? "";
            var dt = GetMomeryInstance(regName);
            var res = dt.BeginSearch(key);
            List<CodeDataModel> list = new List<CodeDataModel>();
            res.ToList().ForEach(a => {

                list.Add(new CodeDataModel() { 
                 CODE_TEXT = a ,
                  CODE_VALUE = a
                });
            });

            return ReturnJson(list);
        }

         public string Search(string regName, string key, string ds, int? pageSize, int? pageIndex)
         {
             SetPostDataSet(ds);
             key = key ?? "";
             var dt = GetMomeryInstance(regName);
             int pageCount = 0;
             var res = dt.Search(key,pageIndex??0,pageSize??20,ref pageCount);

             MomeryModel model = new MomeryModel();
             model.List = res.ToList();
             model.Index = pageIndex ?? 0;
             model.Size = (pageSize ?? 20) == 0 ? 20 : (pageSize ?? 20);
             model.Total = pageCount;
             return ReturnJson(model);
         }

         public string Add(string regName, string text, string ds)
         {

             SetPostDataSet(ds);
             text = text ?? "";
             var dt = GetMomeryInstance(regName);
              dt.AddText(text,AtawAppContext.Current.UnitOfData);
             int res = AtawAppContext.Current.UnitOfData.Submit();
             return ReturnJson(res);
         }
         public string Remove(string regName, string text, string ds)
         {

             SetPostDataSet(ds);
             text = text ?? "";
             var dt = GetMomeryInstance(regName);
              dt.RemoveText(text,AtawAppContext.Current.UnitOfData);
              int res = AtawAppContext.Current.UnitOfData.Submit();
             return ReturnJson(res);
         }
    }
}
