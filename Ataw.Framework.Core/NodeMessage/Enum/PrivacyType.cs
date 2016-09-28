using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core
{
    [CodePlug("PrivacyType", Author = "zgl", Description = "隐私类型")]
    public enum PrivacyType
    {
        [Description("所有人可见")]
        ToAllPeople = 0,
        [Description("部门内可见")]
        ToDepartment = 1,
        [Description("圈内可见")]
        ToClub = 2,
        [Description("对自己和个人可见")]
        ToUser = 3,
        [Description("仅自己可见")]
        ToMyself = 4
    }
}
