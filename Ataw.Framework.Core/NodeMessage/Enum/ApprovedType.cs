using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core
{
    [CodePlug("ApprovedType", Author = "zgl", Description = "审核状态")]
    public enum ApprovedType
    {
        [Description("通过")]
        Pass = 0,
        [Description("未通过")]
        Fail = 1
    }
}
