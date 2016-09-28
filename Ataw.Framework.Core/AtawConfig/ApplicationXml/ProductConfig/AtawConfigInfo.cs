using System.IO;
using System.Xml.Serialization;
///xml实体类
namespace Ataw.Framework.Core
{
    [XmlRoot("Ataw")]
    public class AtawConfigInfo : FileXmlConfigBase
    {
        public AtawConfigInfo()
        {
            //  Databases = new RegNameList<DatabaseConfigItem>();
            ControlUnits = new RegNameList<ControlUnitItemInfo>();
            BaseConfigs = new RegNameList<BaseConfigItemInfo>();
        }

        [XmlArrayItem("ControlUnit")]
        public RegNameList<ControlUnitItemInfo> ControlUnits { get; set; }
        [XmlArrayItem("BaseConfig")]
        public RegNameList<BaseConfigItemInfo> BaseConfigs { get; set; }
        public static AtawConfigInfo ReadConfig(string binPath)
        {
            string fpath = Path.Combine(binPath, AtawApplicationConfig.REAL_PATH, "ProductConfig.xml");
            return XmlUtil.ReadFromFile<AtawConfigInfo>(fpath);
        }
        //public static AtawConfigInfo SaveConfig(string binPath,AtawConfigInfo xmlObj)
        //{
        //    string fpath = Path.Combine(binPath, AtawApplicationConfig.REAL_PATH, "ProductConfig.xml");
        //    return XmlUtil.SaveToFile<AtawConfigInfo>(fpath, xmlObj);
        //}
    }
}
