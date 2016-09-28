using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core
{
    [CodePlug("DMViewType", Author = "zgl", Description = "资源状态")]
    public enum DMViewType
    {
        [Description("个人")]
        Personal = 1,
        [Description("公共")]
        Public = 2
    }
}
