using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class UserConfig : XmlConfigBase
    {
        public UserDeskConfig UserDesk { get; set; }

        public bool ShowMenuIntro { get; set; }

        public bool ShowWarmTip { get; set; }
    }
}
