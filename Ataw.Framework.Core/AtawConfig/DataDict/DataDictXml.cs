using System.Collections.Generic;
using System.IO;

namespace Ataw.Framework.Core
{
    public class DataDictXml : FileXmlConfigBase, IReadXmlCallback
    {
        public DataDictXml()
        {
            DataDicts = new RegNameList<DataDict>();
        }

        public bool HasCache
        {
            get;
            set;
        }

        public RegNameList<DataDict> DataDicts { get; set; }

        public void OnReadXml()
        {
            //throw new NotImplementedException();
        }

        public static DataDictXml ReadConfig(string fileName)
        {
            string fpath = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH,
                "DataDict", fileName);

            return XmlUtil.ReadFromFile<DataDictXml>(fpath);
        }
        public static IList<DataDictXml> ReadConfigFromDir()
        {
            string fpath = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH,
                "DataDict");
            FileUtil.ConfirmPath(fpath);
            string[] xmls = Directory.GetFiles(fpath, "*.xml", SearchOption.AllDirectories);
            IList<DataDictXml> dictXmlList = new List<DataDictXml>();
            foreach (var dict in xmls)
            {
                dictXmlList.Add(XmlUtil.ReadFromFile<DataDictXml>(dict));
            }
            return dictXmlList;
        }
    }
}
