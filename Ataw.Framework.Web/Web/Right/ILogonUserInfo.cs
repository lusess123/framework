using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Web
{
    public interface ILogonUserInfo
    {
        string NickName { get; set; }

        string LogOnName { get; set; }

        string Encoding { get; set; }

        object UserId { get; set; }

        object RoleId { get; set; }

        bool IsLogOn { get; set; }

        object Data1 { get; set; }

        object Data2 { get; set; }
    }
}
