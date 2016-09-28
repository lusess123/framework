using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Ataw.Framework.Core
{
    public enum MessageType
    {

        [Description("待办事务")]
        ToDo = 0,
        [Description("公告")]
        Notice = 1

    }
}