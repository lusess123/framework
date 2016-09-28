using System;

namespace Ataw.Framework.Core
{
    public static class LogManager
    {
        public static ILog GetLogger(Type obj, bool hasLogger)
        {
            return new logger(obj, hasLogger);
        }

        public static void Configure(string filename, int sizelimitKB, bool showmethodnames)
        {
            FileLogger.Instance.Init(filename, sizelimitKB, showmethodnames);
        }
    }
}
