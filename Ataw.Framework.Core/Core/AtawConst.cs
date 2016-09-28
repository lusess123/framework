using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace Ataw.Framework.Core
{
    public class AtawConst
    {
        public const string DEBUG = "DEBUG";

        public static readonly string TOOLKIT = "Ataw";

        public static readonly string ROOT_NODE_NAME = "Ataw";

        public static readonly string DATE_FMT_STR = "yyyy-MM-dd";

        public static readonly string DATETIME_FMT_STR = "yyyy-MM-dd HH:mm:ss";

        public static readonly string DATE_MOBILE_STR = "yy/MM/dd HH:mm";

   

#if DEBUG
        /// <summary>
        /// 查看xml源文件的QueryString的Tag
        /// </summary>
        public static readonly string VIEW_TAG = "_ataw";
        /// <summary>
        /// 查看xml源文件的QueryString的Value
        /// </summary>
        public static readonly string VIEW_VALUE = "xml";
#else
		public static readonly string VIEW_TAG = "__toolkit";
		public static readonly string VIEW_VALUE = "xml";
#endif


        internal static readonly Encoding UTF8 = new UTF8Encoding(false);

        #region web path
        public static readonly string XML_PATH = "..\\XML";
        public static readonly string LOG_PATH = "..\\XML\\LOG";

        #endregion

    }
}
