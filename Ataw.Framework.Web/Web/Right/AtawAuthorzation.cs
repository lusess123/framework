using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Web
{
    public class AtawAuthorzation : IAuthorzation
    {
        public BaseLogonUserInfo GetLogonUserInfo(string logonId)
        {
            return new BaseLogonUserInfo();
        }

        public string GetLogonId()
        {
            return GlobalVariable.UserId.ToString();
        }
    }
}
