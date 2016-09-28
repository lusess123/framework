using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    [Serializable]
    public class AtawPlugConfig : FileXmlConfigBase
    {
        public AtawPlugConfig()
        {
            PlugDllInfoes = new List<PlugDllInfo>();
        }

        public DateTime CreateTime
        {
            get;
            set;
        }
        [XmlArrayItem("PlugDll")]
        public List<PlugDllInfo> PlugDllInfoes { get; set; }

    }
}
