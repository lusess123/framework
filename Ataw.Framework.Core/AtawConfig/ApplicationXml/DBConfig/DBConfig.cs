using System;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace Ataw.Framework.Core
{
    [Serializable]
    public class DBConfig : FileXmlConfigBase, IReadXmlCallback
    {
        public DBConfig()
        {
            Databases = new RegNameList<DatabaseConfigItem>();
        }

        [XmlArrayItem("Database")]
        public RegNameList<DatabaseConfigItem> Databases { get; set; }

        public static DBConfig ReadConfig(string binPath)
        {
            string fpath = Path.Combine(binPath, AtawApplicationConfig.REAL_PATH, "DB.xml");

            return XmlUtil.ReadFromFile<DBConfig>(fpath);
        }

        void IReadXmlCallback.OnReadXml()
        {
            AtawDebug.AssertNotNull(Databases.First(a => a.IsDefault), "应该有一个默认的连接字符串", this);
        }
    }
}
