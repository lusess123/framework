using System;
using System.Runtime.Serialization;

namespace Ataw.Framework.Core
{
    [Serializable]
    public class AtawAssertException : AtawException
    {
        protected AtawAssertException()
        {
        }

        protected AtawAssertException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public AtawAssertException(string message, object errorObject)
            : base(message, errorObject)
        {
        }
    }
}
