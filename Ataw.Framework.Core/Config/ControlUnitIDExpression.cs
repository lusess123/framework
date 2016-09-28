using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Ataw.Framework.Core
{
    [CodePlug("ControlUnitID", BaseClass = typeof(IExpression),
        CreateDate = "2012-11-28", Author = "sj", Description = "当前组织机构ID")]
    public class ControlUnitIDExpression : IExpression
    {
        public string Execute()
        {
            return AtawAppContext.Current.FControlUnitID;
        }
    }
}
