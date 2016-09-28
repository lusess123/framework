using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ataw.Framework.Core.AtawConfig.AppMenu
{
    public class AppMenuConfig
    {
        public AppMenuConfig()
        {
            Roots = new List<AppMenu>();
        }
        [XmlElement("AppMenu")]
        public List<AppMenu> Roots { get; set; }
    }
}
