using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Ataw.Framework.Core
{
    [DescriptionAttribute("服务表的状态")]
    public enum EnumServiceObjType
    {
        [DescriptionAttribute("设备申请")]
        GPSApply = 0,
        [DescriptionAttribute("短信费用")]
        SMSFee=1,
        [DescriptionAttribute("组织机构")]
        ControlUnitFee= 2,
        [DescriptionAttribute("组织机构-菜单(子模块)")]
        ControlUnitMenu=3,
        [DescriptionAttribute("GPS设备服务时间")]
        GPSFee=4,
        [DescriptionAttribute("SIM卡服务时间")]
        SIMFee = 5
    }
}
