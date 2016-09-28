using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Ataw.Framework.Core
{
    [CodePlug("Now", BaseClass = typeof(IExpression),
        CreateDate = "2012-11-28", Author = "sj", Description = "当前时间")]
    public class NowExpression : IExpression
    {
        public string Execute()
        {
            return DateTime.Now.ToString();
        }
    }
}
