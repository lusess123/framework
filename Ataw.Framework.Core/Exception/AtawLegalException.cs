using System;
using System.Runtime.Serialization;

namespace Ataw.Framework.Core
{
    [Serializable]
    public class AtawLegalException : Exception
    {
        protected AtawLegalException()
        {
        }

        public AtawLegalException(string message)
            : base(message)
        {
        }

    }
}
