using System;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    [Serializable]
    public sealed class AtawApplicationConfig : FileXmlConfigBase
    {
        public static string REAL_PATH = ConfigurationManager.AppSettings["DptXmlPathString"]??  "..\\xml";
        /// <summary>
        /// 不允许被外部调用
        /// </summary>
        public AtawApplicationConfig()
        {
           // string myConn = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            //string basePath = 
            // Databases = new RegNameList<DatabaseConfigItem>();
            AppSettings = new RegNameList<KeyValueItem>();
        }

        public bool IsSupportMobile { get; set; }
        public bool IsSupportReport { get; set; }
        public bool IsSupportQing { get; set; }
        public bool PageHelpStack { get; set; }
        public bool SignStack { get; set; }

        public string AppName { get; set; }

        [XmlArrayItem("AppSetting")]
        public RegNameList<KeyValueItem> AppSettings { get; set; }

        public RightBuilderConfig RightBuilder { get; set; }

        public GroupBuilderConfig GroupBuilder { get; set; }

        public GPSBuilderConfig GPSBuilder { get; set; }
        public GPSSetConfig GPSSetBuilder { get; set; }

        public ServiceBuilderConfig ServiceBuilder { get; set; }
        public MessagesBuilderConfig MessagesBuilder { get; set; }
        public WorkflowDefBuilderConfig WorkflowDefBuilder { get; set; }

        public bool HasLogger { get; set; }
        public bool ExceptionStack { get; set; }
        //是否根据XML新增或更新数据库
        public bool IsMigration { get; set; }
        public MemcachedConfig Memcached { get; set; }
        public static AtawApplicationConfig ReadConfig(string binPath)
        {
            string fpath = Path.Combine(binPath, REAL_PATH, "Application.xml");

            return XmlUtil.ReadFromFile<AtawApplicationConfig>(fpath);
        }


        public void ExeMacro()
        {
           foreach(var item in this.AppSettings)
           {
               item.Value =   item.ExeValue();
           }
            //throw new NotImplementedException();
        }
    }
}
