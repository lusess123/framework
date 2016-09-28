using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace Ataw.Framework.Core
{
    public class CustomScriptConfig : FileXmlConfigBase
    {
        [XmlArrayItem("CustomScript")]
        public List<CustomScript> CustomScripts { get; set; }

        public static CustomScriptConfig ReadConfig(string fileName)
        {
            string fpath = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH,
                "Script", fileName);

            return XmlUtil.ReadFromFile<CustomScriptConfig>(fpath);
        }
        public static IList<CustomScriptConfig> ReadConfigFromDir()
        {
            string fpath = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH,
                "Script");
            if (!Directory.Exists(fpath))
            {
                Directory.CreateDirectory(fpath);
            }
            string[] scriptXmls = Directory.GetFiles(fpath, "*.xml", SearchOption.AllDirectories);
            IList<CustomScriptConfig> scriptList = new List<CustomScriptConfig>();
            foreach (var scriptXml in scriptXmls)
            {
                scriptList.Add(XmlUtil.ReadFromFile<CustomScriptConfig>(scriptXml));
            }
            return scriptList;
        }
    }
}
