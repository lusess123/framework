using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    [Serializable]
    public sealed class UserApplicationConfig : FileXmlConfigBase
    {
       // public static string REAL_PATH = "..\\xml";

        public string AppName { get; set; }
        public string UserID { get; set; }
        public string RegName { get; set; }
        public List<App> Applications {get; set;}

        public class App
        {
           public string AppID {get; set;}
           public string DisplayName {get; set;}
           public string Url {get; set;}
           public bool IsFolder {get; set;}
           public string Icon {get; set;}
           public bool IsTopMenu {get; set;}
        }
        public static UserApplicationConfig ReadConfig(string binPath)
        {
            string fpath = Path.Combine(binPath, AtawApplicationConfig.REAL_PATH, "UserApp.xml");

            return XmlUtil.ReadFromFile<UserApplicationConfig>(fpath);
        }
    }
}
