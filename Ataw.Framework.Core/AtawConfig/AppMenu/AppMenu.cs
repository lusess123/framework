using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ataw.Framework.Core.AtawConfig.AppMenu
{
    public class AppMenu
    {

        public AppMenu()
        {
            Children = new List<AppMenu>();
        }
        /// <summary>
        /// 必填
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 可选，如果没有直接用Name的值
        /// </summary>
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }

        [XmlElement("AppMenu")]
        public List<AppMenu> Children { get; set; } 
    }
}
