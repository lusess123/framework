using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Ataw.Framework.Core
{
    [CodePlug("PlugInTag",Author="zhengyk",Description="代码")]
    public enum PlugInTag
    {
        [Description("代码插件")]
        Code = 1,
        [Description("Xml插件")]
        Xml = 2,
        [Description("工作流相关插件")]
        Workflow = 3,
        [Description("系统插件")]
        Sys = 4
    }
}
