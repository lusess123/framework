using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Ataw.Framework.Core
{
    [Serializable]
    [XmlRoot("AtawMvcConfig")]
    public class MvcConfigInfo : FileXmlConfigBase
    {
        public MvcConfigInfo()
        {
            DataRoutes = new RegNameList<RoutesInfo>();

        }
        public string AppName { get; set; }

        [XmlArrayItem("DataRoute")]
        public RegNameList<RoutesInfo> DataRoutes { get; set; }

        public RegRoutes Routes { get; set; }

        public static MvcConfigInfo ReadConfig(string binPath)
        {
            string fpath = Path.Combine(binPath, AtawApplicationConfig.REAL_PATH, "MvcConfig.xml");
            return XmlUtil.ReadFromFile<MvcConfigInfo>(fpath);
        }

        public string GetParam()
        {
            if(Routes != null){
            string _rout = Routes.RegName;
            if (!_rout.IsEmpty())
            {
                var _d = DataRoutes.FirstOrDefault(a => a.Name == _rout);
                var _p = _d.Param;
                if (!_p.IsEmpty()) return _p;
            }
            }
            return "";
        }
        //public static AtawConfigInfo SaveConfig(string binPath,AtawConfigInfo xmlObj)
        //{
        //    string fpath = Path.Combine(binPath, AtawApplicationConfig.REAL_PATH, "ProductConfig.xml");
        //    return XmlUtil.SaveToFile<AtawConfigInfo>(fpath, xmlObj);
        //}
    }
}
