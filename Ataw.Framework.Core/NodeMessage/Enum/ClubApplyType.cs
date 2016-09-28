using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core
{
    [CodePlug("ClubApplyType", Author = "zgl", Description = "圈子申请类别")]
    public enum ClubApplyType
    {
        [Description("普通成员申请")]
        MemberApply = 0,
        [Description("管理员申请")]
        AdminApply = 1
    }
}
