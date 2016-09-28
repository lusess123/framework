using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    public class DeskBoard
    {
        //展板ID
        public string BoardID { get; set; }

        //展板名
        public string BoardName { get; set; }

        // 展板显示名
        public string BoardDisplayName { get; set; }

        //是否显示
        public bool IsDisplay { get; set; }

        //排序编号
        public int Order { get; set; }

        //应用展板Url
        public string Url { get; set; }
      
    }
}
