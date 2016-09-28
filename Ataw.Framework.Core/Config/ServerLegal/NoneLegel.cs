using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core.Config.ServerLegal
{
     [CodePlug("none", BaseClass = typeof(IServerLegal),
     CreateDate = "2015-07-17", Author = "zyk", Description = "服务端验证默认插件")]
  public  class NoneLegel : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            return new LegalObject() { 
             IsLegal = true 
            };
        }
    }
}
