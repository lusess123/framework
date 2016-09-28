using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public class CodePlugAttribute : BasePlugInAttribute
    {
        public Type BaseClass
        {
            get;
            set;
        }

        public PlugInTag[] Tags { get; set; }
        public CodePlugAttribute(string regName)
            : base(regName)
        {
        }
    }
}
