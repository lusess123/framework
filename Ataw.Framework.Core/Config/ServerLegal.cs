using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Ataw.Framework.Core
{
    public interface IServerLegal
    {
        LegalObject CheckLegal(ColumnConfig colConfig, ObjectData data);
    }
}
