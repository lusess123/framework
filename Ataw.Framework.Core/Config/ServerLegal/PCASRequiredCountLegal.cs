using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core.Config.ServerLegal
{
     [CodePlug("PCASRequiredCountLegal", BaseClass = typeof(IServerLegal),
     CreateDate = "2015-07-30", Author = "cl", Description = "PCAS控件必填项个数验证插件")]
    class PCASRequiredCountLegal : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            return new LegalObject { IsLegal = true  };
        }
    }
}
