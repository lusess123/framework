using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Ataw.Framework.Core
{
    [CodePlug("SingleCheckBoxIsOrNo", Author = "wq", Description = "是否")]
    public enum SingleCheckBoxIsOrNo
    {
        [Description("是")]
        IsOrNo_Is = 1,
        [Description("否")]
        IsOrNo_No = 0
    }
}
