using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Ataw.Framework.Core
{
    [CodePlug("UserName", BaseClass = typeof(IExpression),
        CreateDate = "2013-3-26", Author = "sj", Description = "当前用户")]
    public class UserNameExpression : IExpression
    {
        public string Execute()
        {
            return AtawAppContext.Current.UserName;
        }
    }
}
