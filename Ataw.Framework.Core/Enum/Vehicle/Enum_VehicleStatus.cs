using System.ComponentModel;

namespace Ataw.Framework.Core
{
    //车辆审核状态枚举
    public enum Enum_VehicleAuditStatus
    {
        [Description("待审核")]
        PendingAudit = 0,
        [Description("已审核")]
        Audit = 1
    }

    //车辆服务状态枚举
    public enum Enum_VehicleServiceStatus
    {
        [Description("有效期内")]
        Service = 0,
        [Description("不在有效期")]
        unService = 1
    }

}
