using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Ataw.Framework.Core
{
    public sealed class AtawTrace
    {
        private readonly static TraceSource fSource = new TraceSource("Ataw");

        private const string POST_DATA_LOG_FILE = "AtawLogs\\post.json";
        private const string DATABASE_SUBMIT_LOG_FILE = "AtawLogs\\submitsql.txt";
        private const string DATABASE_QUERY_LOG_FILE = "AtawLogs\\querysql.txt";
        private const string DATABASE_PAGEQUERY_LOG_FILE = "AtawLogs\\pagequerysql.txt";
        private const string JSONDATA_LOG_FILE = "AtawLogs\\jsondata.json";
        private const string ERROR_LOG_FILE = "AtawLogs\\error.txt";
        private const string PLUGIN_LOG_FILE = "AtawLogs\\plugin.txt";
        private const string DATABASE_STRUCTURE_FILE = "AtawLogs\\databasestructure.txt";
        private const string DESK_LOG_FILE = "AtawLogs\\desk.txt";
        private const string REQUEST_ASK_LOG_FILE = "AtawLogs\\rquest_ask.txt";

        private const string IO_ERROR_LOG_FILE = "AtawLogs\\io_error.txt";

        private const string SQL_SUBMIT_ERROR_LOG_FILE = "AtawLogs\\sql_submit_error.txt";
        private const string SQL_QUERY_ERROR_LOG_FILE = "AtawLogs\\sql_query_error.txt";

        private const string TIME_SIGN = "AtawLogs\\time_sign.txt";

        private const string RIGHT_LOG = "AtawLogs\\RIGHT_LOG.txt";

        private const string TEST_BUSINESS = "AtawLogs\\TEST_BUSINESS.txt";
        private const string EF_ERROR_LOG_FILE = "AtawLogs\\EF_ERROR_LOG_FILE.txt";
        private const string PAGE_VIEW_DATA = "AtawLogs\\page_View_Data.txt";

        private const string WORKFLOW_ERROR = "AtawLogs\\WORKFLOW_ERROR.txt";

        private const string QYWX_POST = "AtawLogs\\QYWX_POST.txt";
        private const string QYWX_ERROR = "AtawLogs\\QYWX_ERROR.txt";

        [Conditional("TRACE")]
        public static void WriteFile(string fileName, string content)
        {
            fSource.TraceData(TraceEventType.Verbose, TraceFileListener.FILELISTENER_SIGN, fileName, content);
        }

        [Conditional("TRACE")]
        public static void WriteFile(string fileName, string content, Encoding encoding)
        {
            fSource.TraceData(TraceEventType.Verbose, TraceFileListener.FILELISTENER_SIGN, fileName, content, encoding);
        }


        public static void AddWriteFileSave(LogType logType, string text)
        {
            string _regName = "ATAW_LOG_CONTENT_" + logType.ToString();
            var _obj = AtawAppContext.Current.PageFlyweight.PageItems[_regName];
            if (_obj != null)
            {
                AtawAppContext.Current.PageFlyweight.PageItems[_regName] = "{0}{1}{2}".AkFormat(_obj.ToString(), Environment.NewLine, text);
                //_obj = _obj.ToString() + text;
            }
            else
            {
                AtawAppContext.Current.PageFlyweight.PageItems[_regName] = text;
            }
        }

        public static void EndWriteFileSave(LogType logType)
        {
            string _regName = "ATAW_LOG_CONTENT_" + logType.ToString();
            var _obj = AtawAppContext.Current.PageFlyweight.PageItems[_regName];
            if (_obj != null)
            {
                WriteFile(logType, _obj.ToString());
                AtawAppContext.Current.PageFlyweight.PageItems.Remove(_regName);
            }
        }

        public static void WriteStringFile(string fileName, string content)
        {
            string fullfileName = Path.Combine(AtawAppContext.Current.BinPath,
                     AtawApplicationConfig.REAL_PATH, fileName);
            if ("MultiLog".AppKv<bool>(false))
            {
                string _fileName = Path.GetFileNameWithoutExtension(fullfileName);
                fullfileName = FileUtil.ChangeFileName(fullfileName, "{0}_{1}".AkFormat(_fileName, DateTime.Now.ToString("yyyyMMddHHmmss-fffffff")));
            }
            // fileName = string.Format(ObjectUtil.SysCulture, "{0}\\{1}\\{2}", AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH.,fileName);
            WriteFile(fullfileName, content);
        }

        [Conditional("TRACE")]
        public static void WriteCustomFile(string logType, string content)
        {
           // string fileName = string.Empty;
            string fileName = "AtawLogs\\{0}.txt".AkFormat(logType.ToString());
                  //  break;
          //  }
            if (fileName != string.Empty)
            {
                WriteStringFile(fileName,content);
            }

        }

        [Conditional("TRACE")]
        public static void WriteFile(LogType logType, string content)
        {
            switch (logType)
            {
                case LogType.JsonData:
                case LogType.PageViewData:
                case LogType.Plugin:
                   // case LogType.
                    break;

                default:
                    content = string.Format(ObjectUtil.SysCulture, "{0}生成时间：{1}{0}{2}",
                              System.Environment.NewLine, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), content);
                    break;
            }
            string fileName = string.Empty;
            switch (logType)
            {
                case LogType.Error:
                    //content = string.Format(ObjectUtil.SysCulture, "{0}生成时间：{1}{0}{2}",
                    //    System.Environment.NewLine, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), content);
                    fileName = ERROR_LOG_FILE;
                    break;
                case LogType.JsonData:
                    fileName = JSONDATA_LOG_FILE;
                    break;
                case LogType.Plugin:
                    fileName = PLUGIN_LOG_FILE;
                    break;
                case LogType.Post:
                    fileName = POST_DATA_LOG_FILE;
                    break;
                case LogType.QuerySql:
                    fileName = DATABASE_QUERY_LOG_FILE;
                    break;
                case LogType.PageQuerySql:
                    fileName = DATABASE_PAGEQUERY_LOG_FILE;
                    break;
                case LogType.SubmitSql:
                    fileName = DATABASE_SUBMIT_LOG_FILE;
                    break;
                case LogType.DatabaseStructure:
                    fileName = DATABASE_STRUCTURE_FILE;
                    break;
                case LogType.Desk:
                    fileName = DESK_LOG_FILE;
                    break;
                case LogType.RequestAsk:
                    fileName = REQUEST_ASK_LOG_FILE;
                    break;
                case LogType.IoError:
                    fileName = IO_ERROR_LOG_FILE;
                    break;
                case LogType.SqlSubmitError:
                    fileName = SQL_SUBMIT_ERROR_LOG_FILE;
                    break;
                case LogType.SqlQueryError:
                    fileName = SQL_QUERY_ERROR_LOG_FILE;
                    break;
                case LogType.TimeSign:
                    fileName = TIME_SIGN;
                    break;
                case LogType.RightLog:
                    fileName = RIGHT_LOG;
                    break;
                case LogType.TestBusiness:
                    fileName = TEST_BUSINESS;
                    break;
                case LogType.EfErrot:
                    fileName = EF_ERROR_LOG_FILE;
                    break;
                case LogType.PageViewData:
                    fileName = PAGE_VIEW_DATA;
                    break;
                case LogType.WorkFlowError:
                    fileName = WORKFLOW_ERROR;
                    break;
                case LogType.QyWxPost:
                    fileName = QYWX_POST;
                    break;
                case LogType.QyWxError:
                    fileName = QYWX_ERROR;
                    break;
                default:
                    fileName = "AtawLogs\\{0}.txt".AkFormat(logType.ToString());
                    break;
            }
            if (fileName != string.Empty)
            {
                WriteStringFile(fileName,content);
            }

        }
    }
}
