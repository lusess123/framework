
namespace Ataw.Framework.Core
{
    public interface ILog
    {
        void Debug(object msg, params object[] objs);
        void Error(object msg, params object[] objs);
        void Info(object msg, params object[] objs);
        void Warn(object msg, params object[] objs);
        void Fatal(object msg, params object[] objs);
        bool IsErrorEnabled { get; set; }
        bool IsDebugEnabled { get; set; }
        bool IsInfoEnabled { get; set; }
    }

    public interface ILogs
    {
        void Debug(object msg, params object[] objs);
        void Error(object msg, params object[] objs);
        void Info(object msg, params object[] objs);
        void Warn(object msg, params object[] objs);
        void Fatal(object msg, params object[] objs);
        bool IsErrorEnabled { get; set; }
        bool IsDebugEnabled { get; set; }
        bool IsInfoEnabled { get; set; }
    }

    public class Logs 
    {
     public   void Debug(object msg, params object[] objs) { }
     public void Error(object msg, params object[] objs) { }
     public void Info(object msg, params object[] objs) { }
     public void Warn(object msg, params object[] objs) { }
     public void Fatal(object msg, params object[] objs) { }
     public bool IsErrorEnabled { get; set; }
     public bool IsDebugEnabled { get; set; }
     public bool IsInfoEnabled { get; set; }
    
    }
}
