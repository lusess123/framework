using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core
{
    [CodePlug("ClubType", Author = "zgl", Description = "圈子类别")]
    public enum ClubType
    {
        [Description("文学")]
        Literature = 0,
        [Description("管理")]
        Manage = 1,
        [Description("技术")]
        Technology = 2,
        [Description("娱乐")]
        Entertainment = 3,
        [Description("美食")]
        Cate = 4,
        [Description("交流")]
        Exchange = 5,
        [Description("其他")]
        Else = 50
    }
}
