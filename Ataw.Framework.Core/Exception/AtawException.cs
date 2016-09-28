using System;
using System.Runtime.Serialization;

namespace Ataw.Framework.Core
{
    [Serializable]
    public class AtawException : Exception
    {
        [NonSerialized]
        private readonly object fErrorObject;

        protected AtawException()
        {
        }

        protected AtawException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public AtawException(string message, object errorObject)
            : base(message)
        {
            AtawDebug.AssertArgumentNullOrEmpty(message, "message", null);

            fErrorObject = errorObject;
        }

        public AtawException(string message, Exception innerException, object errorObject)
            : base(message, innerException)
        {
            AtawDebug.AssertArgumentNullOrEmpty(message, "message", null);
            AtawDebug.AssertArgumentNull(innerException, "innerException", null);

            fErrorObject = errorObject;
        }

        public object ErrorObject
        {
            get
            {
                return fErrorObject;
            }
        }

        public override string ToString()
        {
            if (fErrorObject == null)
                return base.ToString();
            else
                return string.Format(ObjectUtil.SysCulture,
                    "对象{0}的例外", fErrorObject);
        }
    }
}
