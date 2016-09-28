using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Ataw.Framework.Core
{
    [DescriptionAttribute("SIM卡设置状态")]
    public enum GPSSIMStatus
    {
        [DescriptionAttribute("未审核")]
        UnAudit = -1,
        [DescriptionAttribute("未使用")]
        Unused,
        [DescriptionAttribute("锁定")]
        Lock,
        [DescriptionAttribute("使用中")]
        Inuse,
        [DescriptionAttribute("报废")]
        Scrap
    }


    [DescriptionAttribute("GPS设置状态")]
    public enum GPSSetStatus
    {
        [DescriptionAttribute("未审核")]
        UnAudit = -1,
        [DescriptionAttribute("未使用")]
        Unused,
        [DescriptionAttribute("锁定")]
        Lock,
        [DescriptionAttribute("使用中")]
        Inuse,
        [DescriptionAttribute("报废")]
        Scrap
    }
}
