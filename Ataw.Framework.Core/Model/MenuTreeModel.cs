using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    public class MenuTreeModel
    {
        public string Icon { get; set; }
        public RightKeyType KeyType { get; set; }
        public MenuRightType RightType { get; set; }
        /// <summary>
        /// 一般情况是url
        /// </summary>
        public string RightValue { get; set; }
        //RightType
    }
}
