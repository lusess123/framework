using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public sealed class InitializationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the InitializationAttribute class.
        /// </summary>
        public InitializationAttribute(Type initClassType)
            : this(initClassType, InitPriority.Normal)
        {
        }

        /// <summary>
        /// Initializes a new instance of the InitializationAttribute class.
        /// </summary>
        public InitializationAttribute(Type initClassType, InitPriority priority)
        {
            InitClassType = initClassType;
            Priority = priority;
        }

        public Type InitClassType { get; private set; }

        public InitPriority Priority { get; private set; }
    }
}
