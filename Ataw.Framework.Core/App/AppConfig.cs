using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class AppConfig : FileXmlConfigBase, IReadXmlCallback
    {
        public AppConfig() {

            RoleInfoList = new List<RoleInfo>();
            Role2RightList = new List<Role2Right>();
            PanelList = new List<FormDoor>();
            WorkflowStartList = new List<WorkflowStart>();
            SysConfigForm = new FormDoor();
            ReportList = new List<FormDoor>();
            RootMenuRights = new List<MenuRight>();

            BaseInfo = new AppBaseInfo();
        }


        public static IList<AppConfig> ReadConfigFromDir()
        {
            string fpath = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH,
                "App");
            FileUtil.ConfirmPath(fpath);
            string[] xmls = Directory.GetFiles(fpath, "*.xml", SearchOption.AllDirectories);
            IList<AppConfig> dictXmlList = new List<AppConfig>();
            foreach (var dict in xmls)
            {
                dictXmlList.Add(XmlUtil.ReadFromFile<AppConfig>(dict));
            }
            return dictXmlList;
        }

        /// <summary>
        /// 应用的基本信息
        /// </summary>
      public  AppBaseInfo BaseInfo { get; set; }
        /// <summary>
        /// 应用的菜单
        /// </summary>
         [XmlArrayItem("Menu")]
      public  List<MenuRight> RootMenuRights { get; set; }
        /// <summary>
        /// 应用的角色
        /// </summary>
         [XmlArrayItem("RoleInfo")]
      public  List<RoleInfo> RoleInfoList { get; set; }
        /// <summary>
        /// 应用的角色菜单映射
        /// </summary>
       [XmlArrayItem("Role2Right")]
      public  List<Role2Right> Role2RightList { get; set; }
        /// <summary>
        /// 展板表单
        /// </summary>
         [XmlArrayItem("Panel")]
      public  List<FormDoor> PanelList { get; set; }
        /// <summary>
        /// 工作流入口
        /// </summary>
           [XmlArrayItem("WorkflowStart")]
      public  List<WorkflowStart> WorkflowStartList { get; set; }
        /// <summary>
        /// 系统配置表单
        /// </summary>
     
      public  FormDoor SysConfigForm { get; set; }
        /// <summary>
        /// 报表表单
        /// </summary>
           [XmlArrayItem("Report")]
      public  List<FormDoor> ReportList { get; set; }


           public void OnReadXml()
           {
              // throw new NotImplementedException();
           }
    }

   

    public class AppBaseInfo
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Mesg { get; set; } 
    }

    public class MenuRight
    {
        [XmlIgnore]
        public int __layer { get; set; }

        public string Title { get; set; }
        public string Key { get; set; }
        public string Url { get; set; }
          [XmlElement("Menu")]
        public List<MenuRight> Children { get; set; }
    }

    public class RoleInfo
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class Role2Right
    {
        public string role { get; set; }
        public string right { get; set; }
        [XmlAttribute]
        public bool IsPart { get; set; } 
    }

    public class FormDoor
    {
        string FormXml { get; set; }
    }

    public class WorkflowStart
    {
        string Name { get; set; }
        string Title { get; set; }
        string Url { get; set; }
    }
}
