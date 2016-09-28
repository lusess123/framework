using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ataw.Framework.Core;

namespace Ataw.Workflow.Core
{
    public class ControlActionConfig : IRegName
    {
        public ShowKind ShowKind { get; set; }

        public int Order { get; set; }

        public string AreaName { get; set; }

        public string ControlName { get; set; }

        public string ActionName { get; set; }

        public string Title { get; set; }

        public string RegName
        {
            get { return string.Format("{0}-{1}-{2}", AreaName, ControlName, ActionName); }
        }
    }
}
