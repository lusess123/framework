using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Ataw.Framework.Core
{
    [CodePlug("CurrentUserID", BaseClass = typeof(IExpression),
        CreateDate = "2014-04-16", Author = "sj", Description = "当前用户ID")]
    public class CurrentUserIDExpression : IExpression
    {
        public string Execute()
        {
            return AtawAppContext.Current.UserId;
        }
    }
}
