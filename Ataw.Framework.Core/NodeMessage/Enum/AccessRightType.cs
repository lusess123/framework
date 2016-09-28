using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core
{
    [CodePlug("AccessRightType", Author = "zgl", Description = "圈子访问权限")]
    public enum AccessRightType
    {
        [Description("所有人均可加入")]
        AllAccess = 0,
        [Description("批准后可加入")]
        ApproveAccess = 1,
        [Description("不准加入")]
        NoAccess = 2
    }
}
