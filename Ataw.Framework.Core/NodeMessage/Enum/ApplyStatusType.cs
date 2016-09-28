using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core
{
    [CodePlug("ApplyStatusType", Author = "zgl", Description = "申请状态")]
    public enum ApplyStatusType
    {
        [Description("申请中")]
        Applying = 0,
        [Description("同意")]
        Agree = 1,
        [Description("拒绝")]
        Refuse = 2
    }
}
