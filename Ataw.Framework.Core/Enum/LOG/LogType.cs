
namespace Ataw.Framework.Core
{
    [CodePlug("LogType", Author = "ace", Description = "日志类型")]
    public enum LogType
    {

        Post = 1,
        SubmitSql,
        QuerySql,
        PageQuerySql,
        JsonData,
        Error,
        Plugin,
        DatabaseStructure,
        Desk,
        RequestAsk,
        IoError,
        SqlSubmitError,
        SqlQueryError,
        TimeSign,
        RightLog,
        TestBusiness,
        EfErrot,
        QueryObject,
        PageViewData,
        WorkFlowError,

        QyWxPost ,
        QyWxError,
        JPush ,
        PageCacheHit,
        AppCacheHit,

        AppKvError,
        ReportLog,
        EmailLog,
        None 
    }
}
