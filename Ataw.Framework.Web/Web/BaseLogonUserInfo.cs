
using System;
namespace Ataw.Framework.Web
{
    [Serializable]
    public class BaseLogonUserInfo : ILogonUserInfo
    {

        public virtual string NickName { get; set; }

        public virtual string LogOnName { get; set; }

        public virtual string Encoding { get; set; }

        public virtual object UserId { get; set; }

        public virtual object RoleId { get; set; }

        public virtual bool IsLogOn { get; set; }

        public virtual object Data1 { get; set; }

        public virtual object Data2 { get; set; }
    }
}
