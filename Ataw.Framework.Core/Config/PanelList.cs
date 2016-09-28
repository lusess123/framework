using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    /// <summary>
    /// 布局面板列表
    /// </summary>
    public class PanelList
    {
        public ShowKind ShowKind { get; set; }

        public bool VerticalTab { get; set; }

        public List<Panel> Panels { get; set; }
    }
}
