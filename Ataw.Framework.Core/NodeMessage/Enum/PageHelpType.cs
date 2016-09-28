using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core
{
    [CodePlug("PageHelpType", Author = "zgl", Description = "反馈评论")]
    public enum PageHelpType
    {
        [Description("未处理")]
        Unprocessed = 0,
        [Description("已处理")]
        Processed = 1,
    }
}
