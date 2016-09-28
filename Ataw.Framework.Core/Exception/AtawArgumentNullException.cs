using System;
using System.Runtime.Serialization;


namespace Ataw.Framework.Core
{
    [Serializable]
    public class AtawArgumentNullException : AtawArgumentException
    {
        protected AtawArgumentNullException()
        {
        }

        protected AtawArgumentNullException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public AtawArgumentNullException(string argument, object errorObject)
            : base(argument, string.Format(ObjectUtil.SysCulture,
            StrResource.ArgumentNull, argument), errorObject)
        {
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Argument))
                return base.ToString();
            else
                return string.Format(ObjectUtil.SysCulture, "参数{0}为NULL的例外", Argument);
        }
    }
}
