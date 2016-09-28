using System;

namespace Ataw.Framework.Core
{
    internal class logger : ILog
    {
        public logger(Type type, bool hasLogger)
        {
            hasLogger = true;
            typename = type.Namespace + "." + type.Name;
            fHasLogger = hasLogger;
        }

        private string typename = "";

        private bool fHasLogger = false;


        private void log(string logtype, string msg, params object[] objs)
        {
            string meth = "";
            if (FileLogger.Instance.ShowMethodNames)
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(2);
                System.Diagnostics.StackFrame sf = st.GetFrame(0);
                meth = sf.GetMethod().Name;
            }
            FileLogger.Instance.Log(logtype, typename, meth, msg, objs);
        }

        #region ILog Members

        public void Debug(object msg, params object[] objs)
        {
            if (fHasLogger &&　　AtawAppContext.Current.ApplicationXml.HasLogger)
                log("DEBUG", "" + msg, objs);
        }

        public void Error(object msg, params object[] objs)
        {
            if (fHasLogger)
                log("ERROR", "" + msg, objs);
        }

        public void Info(object msg, params object[] objs)
        {
           // if (fHasLogger)
                log("INFO", "" + msg, objs);
        }

        public void Warn(object msg, params object[] objs)
        {
            if (fHasLogger)
                log("WARN", "" + msg, objs);
        }

        public void Fatal(object msg, params object[] objs)
        {
            if (fHasLogger)
                log("FATAL", "" + msg, objs);
        }
        #endregion


        public bool IsErrorEnabled
        {
            get;
            set;
        }




        public bool IsDebugEnabled
        {
            get;
            set;
        }


        public bool IsInfoEnabled
        {
            get;
            set;
        }
    }
}
