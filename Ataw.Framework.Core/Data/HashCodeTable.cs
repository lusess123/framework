using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Ataw.Framework.Core
{
    public class HashCodeDataModel : CodeDataModel
    {
        public string CODE_NAME { get; set; }
    }


  public  class HashCodeTable: CodeTable<CodeDataModel>
    {
        public const string KEY_NAME = "SELECTOR_ENUM";

        protected  virtual Dictionary<string, HashCodeDataModel> DataList { get; set; }

        public override CodeDataModel this[string key]
        {
            get
            {
                if (DataList.ContainsKey(key))
                { 
                    return DataList[key];
                }
                else
                    return null;

            }
        }

        public override bool HasCache
        {
            get;
            set;
        }

        public override IEnumerable<CodeDataModel> FillData(DataSet postDataSet)
        {

            foreach (var item in DataList)
            {
                yield return item.Value;
            }

        }

        //public override IEnumerable<CodeDataModel> FillData(DataSet postDataSet, Pagination pagination)
        //{

        //    foreach (var item in DataList)
        //    {
        //        yield return item.Value;
        //    }

        //}

        public override IEnumerable<CodeDataModel> BeginSearch(DataSet postDataSet, string key)
        {
            var res = DataList.Where(a => a.Value.CODE_TEXT.IndexOf(key) == 0).Select(a => a.Value);
            List<HashCodeDataModel> list = new List<HashCodeDataModel>();
            list.AddRange(res);
            return list;
        }

        //public override IEnumerable<CodeDataModel> BeginSearch(DataSet postDataSet, string key, Pagination pagination)
        //{
        //    var res = DataList.Where(a => a.Value.CODE_TEXT.IndexOf(key) == 0).Select(a => a.Value);
        //    List<HashCodeDataModel> list = new List<HashCodeDataModel>();
        //    list.AddRange(res);
        //    return list;
        //}

        public override IEnumerable<CodeDataModel> Search(DataSet postDataSet, string key)
        {
            var res = DataList.Where(a => a.Value.CODE_TEXT.Contains(key)).Select(a => a.Value);
            List<HashCodeDataModel> list = new List<HashCodeDataModel>();
            list.AddRange(res);
            return list;
        }

        //public override IEnumerable<CodeDataModel> Search(DataSet postDataSet, string key, Pagination pagination)
        //{
        //    var res = DataList.Where(a => a.Value.CODE_TEXT.Contains(key)).Select(a => a.Value);
        //    pagination.TotalCount = res.Count();
        //    res = res.Skip((pagination.PageIndex - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
        //    List<HashCodeDataModel> list = new List<HashCodeDataModel>();
        //    list.AddRange(res);
        //    return list;
        //}


        public override IEnumerable<CodeDataModel> FillAllData(DataSet postDataSet)
        {
            return FillData(postDataSet);
        }
    }
}
