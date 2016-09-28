using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Ataw.Framework.Core
{
    public interface IButtonRight
    {
        string GetButtons(ObjectData data,IEnumerable<ObjectData> listData);
    }
}
