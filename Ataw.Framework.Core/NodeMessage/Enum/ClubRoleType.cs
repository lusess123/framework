using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core
{
    [CodePlug("ClubRoleType", Author = "zgl", Description = "圈子角色")]
    public enum ClubRoleType
    {
        [Description("管理者")]
        Admin = 0,
        [Description("普通成员")]
        User = 1
    }
}
