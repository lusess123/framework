using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    public interface IGPSBuilder
    {
        /// <summary>
        /// 删除终端信息
        /// </summary>
        /// <param name="TerminalNo">终端号</param>
        /// <returns>删除结果</returns>
        bool DeleteTerminalNoInfo(String TerminalNo);
    }
}
