using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class DataDict : IRegName, IAuthor
    {
        public DataDict()
        {
            DDItems = new RegNameList<KeyValueItem>();
        }

        [XmlAttribute]
        public string Author
        {
            get;
            set;
        }
        [XmlAttribute]
        public string Description
        {
            get;
            set;
        }
        [XmlAttribute]
        public string CreateDate
        {
            get;
            set;
        }

        public string RegName
        {
            get;
            set;
        }

        [XmlArrayItem("DDItem")]
        public RegNameList<KeyValueItem> DDItems { get; set; }

    }

    public class DictCodeTable : CodeTable<CodeDataModel>
    {
        protected string DictXmlFile
        {
            get;
            set;
        }

        protected DataDict DataDict
        {
            get;
            set;
        }

        public void InitDataDict(DataDict dataDict)
        {
            DataDict = dataDict;
        }

        //public override CodeDataModel this[string key]
        //{
        //    //get {
        //    //    DataDict.DDItems.FirstOrDefault(a => a.Key == key);
        //    //}
        //    get { 
        //    };
        //    set{}
        //}

        public override bool HasCache
        {
            get;
            set;
        }

        public override IEnumerable<CodeDataModel> FillData(DataSet postDataSet)
        {
            throw new NotImplementedException();
        }

        //public override IEnumerable<CodeDataModel> FillData(DataSet postDataSet, Pagination pagination)
        //{
        //    throw new NotImplementedException();
        //}

        public override IEnumerable<CodeDataModel> BeginSearch(DataSet postDataSet, string key)
        {
            throw new NotImplementedException();
        }

        //public override IEnumerable<CodeDataModel> BeginSearch(DataSet postDataSet, string key, Pagination pagination)
        //{
        //    throw new NotImplementedException();
        //}

        public override IEnumerable<CodeDataModel> Search(DataSet postDataSet, string key)
        {
            throw new NotImplementedException();
        }

        //public override IEnumerable<CodeDataModel> Search(DataSet postDataSet, string key, Pagination pagination)
        //{
        //    throw new NotImplementedException();
        //}

        public override IEnumerable<CodeDataModel> FillAllData(DataSet postDataSet)
        {
            throw new NotImplementedException();
        }


        public override CodeDataModel this[string key]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
