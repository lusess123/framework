
using System;
using System.Xml.Serialization;
namespace Ataw.Framework.Core
{
  


    [Serializable]
    public class DbCodeTable : IRegName, IAuthor, IReadXmlCallback
    {
        public DbCodeTable() {
            TableName = new MacroConfig();
            TextField = new MacroConfig();
            ValueField = new MacroConfig();
            Where = new MacroConfig();
        }

        public const string CODE_TABLE_KEY_STR = "CodeTable-{0}-{1}";

        //[XmlIgnore]
        //public SingleCodeTable<CodeDataModel> InternalDbCodeTable { get; set; }

        //public DbCodeTable()
        //{

        //}

        public string Sign
        {
            get;
            set;
        }

        public string DataBase
        {
            get;
            set;
        }

        public string RegName
        {
            get;
            set;
        }

        public MacroConfig TableName
        {
            get;
            set;
        }

        /// <summary>
        /// stringformat的字面量，{0}表示value ,{1}表示text
        /// </summary>
        public string TextFormat
        {
            get;
            set;
        }

        public MacroConfig TextField
        {
            get;
            set;
        }

        public MacroConfig ValueField
        {
            get;
            set;
        }

        public bool HasCache
        {
            get;
            set;
        }
        public MacroConfig Where
        {
            get;
            set;
        }

        public string Product
        {
            get;
            set;
        }

        public string ModuleXml
        {
            get;
            set;
        }

        public void OnReadXml()
        {
           // InternalDbCodeTable = new InternalDbCodeTable(this);
            
        }

        //public static DbCodeTable ReadConfig(string fileName)
        //{
        //    string fpath = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH,
        //        "CodeTable", fileName);

        //    return XmlUtil.ReadFromFile<DbCodeTableConfig>(fpath);
        //}
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
        public string Right
        {
            get;
            set;
        }
    }

    public class InternalDbCodeTable : SingleCodeTable<CodeDataModel>
    {

        public InternalDbCodeTable(DbCodeTable xml)
        { 
             RegName = xml.RegName;
                TableName =  xml.TableName.ExeValue();
                HasCache =  xml.HasCache;
                TextField =  xml.TextField.ExeValue();
                ValueField = xml.ValueField.ExeValue();
                Where = xml.Where.ExeValue();
                Product =  xml.Product;
                DataBase =  xml.DataBase;
                Sign =  xml.Sign;
                ModuleXml = xml.ModuleXml;
                if (xml.Right == "Org")
                {
                    if (AtawAppContext.Current.FControlUnitID != "1" && AtawAppContext.Current.FControlUnitID != "")
                    {
                        Where = " {0} AND  FControlUnitID='{1}' ".AkFormat(Where, AtawAppContext.Current.FControlUnitID);
                    }

                }

        }


        public override string RegName
        {
            get;
            set;
        }

        public override string TableName
        {
            get;
            set;
        }

        public override string TextField
        {
            get;
            set;
        }

        public override string ValueField
        {
            get;
            set;
        }

        public override string Where
        {
            get;
            set;
        }


    }

}
