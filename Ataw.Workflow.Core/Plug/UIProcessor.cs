using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ataw.Framework.Core;
using System.Data;

namespace Ataw.Workflow.Core
{
    public abstract class UIProcessor : AutoProcessor
    {
        public DataSet UIData { get; set; }

    }
}
