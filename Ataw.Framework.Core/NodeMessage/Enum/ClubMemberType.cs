using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core
{
    [CodePlug("ClubMemberType", Author = "zgl", Description = "成员类别")]
    public enum ClubMemberType
    {
        [Description("我管理的圈")]
        Admin = 0,
        [Description("我加入的圈")]
        Member = 1,
        [Description("我申请的圈")]
        Applicant = 2
    }
}
