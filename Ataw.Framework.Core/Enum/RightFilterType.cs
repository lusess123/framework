
using System.ComponentModel;
namespace Ataw.Framework.Core
{
    public enum RightFilterType
    {
        [Description("未登录")]
        UnAuthenticated = 0,
        [Description("未续费")]
        UnRenew = -1,
        [Description("没权限")]
        DenyPermission = -2,
        [Description("成功")]
        Success = 1
    }
}
