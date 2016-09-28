using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Workflow.Core
{
    public enum InstState
    {
        [Description("未知")]
       None = 0 ,
        [Description("处理")]
       Process = 1,
         [Description("详情")]
       Detail =2 ,
         [Description("结束")]
        Finish = 3
    }
}
