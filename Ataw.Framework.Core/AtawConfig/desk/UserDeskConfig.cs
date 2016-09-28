using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class UserDeskConfig
    {
        [XmlArrayItem]
        public List<DeskForm> DeskForms { get; set; }

        [XmlArrayItem]
        public List<DeskBoard> DeskBoards { get; set; }
    }
}
