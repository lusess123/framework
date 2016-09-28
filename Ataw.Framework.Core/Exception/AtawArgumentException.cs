using System;
using System.Runtime.Serialization;
using System.Security.Permissions;


namespace Ataw.Framework.Core
{
    [Serializable]
    public class AtawArgumentException : AtawAssertException
    {
        protected AtawArgumentException()
        {
        }

        protected AtawArgumentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Argument = info.GetString("Argument");
        }

        public AtawArgumentException(string argument, string message, object errorObject)
            : base(message, errorObject)
        {
            AtawDebug.AssertArgumentNullOrEmpty(argument, "argument", null);

            Argument = argument;
        }

        public string Argument { get; set; }

        [SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Argument", Argument);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Argument))
                return base.ToString();
            else
                return string.Format(ObjectUtil.SysCulture, "参数{0}的例外", Argument);
        }
    }
}
