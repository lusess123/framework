using System;
using Ataw.Framework.Core;

namespace Ataw.Workflow.Core
{
    [Serializable]
    public class InvalidStateException : AtawException
    {
        public InvalidStateException(object state)
            : base(string.Format(ObjectUtil.SysCulture,
            "当前对象的状态是{0}，属于无效状态", state), state)
        {
        }
    }
}
