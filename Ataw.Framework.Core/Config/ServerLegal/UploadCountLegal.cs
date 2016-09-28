using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core.Config.ServerLegal
{
     [CodePlug("UploadCountLegal", BaseClass = typeof(IServerLegal),
     CreateDate = "2015-04-22", Author = "zyk", Description = "上传文件数量验证插件")]
    class UploadCountLegal : IServerLegal
    {
        public LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data)
        {
            return new LegalObject { IsLegal = true  };
        }
    }
}
