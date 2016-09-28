using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    public static class StringUtil
    {
        private readonly static Encoding fGBEncoding = Encoding.GetEncoding("gb2312");
        private static PinYinSql fPySql;
        private const int BUFFER_SIZE = 1024;

        internal static readonly char[] WideChars = { '%', '_' };


        public static string RemoveEnd(this string str, string end) {
            if (str.Length >= end.Length) {
                var _index = str.LastIndexOf(end);
                if (_index >= 0)
                {
                    return str.Remove(_index);
                }
            }
            return str;
        }

        public static int GuidSafeSub(this string str, int count = 8){
            int _count = str.Length ;
            if (_count < count)
            {
                for (int i = _count; i < count; i++)
                {
                    str = str + "0";
                }
            }
           // if(_count > 8 ){
                string _date = str.Substring(0,8);
                int _int = _date.Value<int>();
                if (_int == 0)
                {
                    return 19000101;
                }
                    else
                    return _int ;
            //}
        }

        internal static PinYinSql PySql
        {
            get
            {
                if (fPySql == null)
                {
                    fPySql = new PinYinSql();
                    fPySql.Load();
                }
                return fPySql;
            }
        }

        public static void JoinStringItem(StringBuilder builder, int index, string value, string joinStr)
        {
            AtawDebug.AssertArgumentNull(builder, "builder", null);
            AtawDebug.AssertArgumentNullOrEmpty(value, "value", null);
            AtawDebug.AssertArgumentNull(joinStr, "joinStr", null);
            AtawDebug.AssertArgument(index >= 0, "index", string.Format(ObjectUtil.SysCulture,
                "index的值不能为负数，现在是{0}", index), null);

            if (index > 0)
                builder.Append(joinStr);
            builder.Append(value);
        }

        public static void JoinStringItem(StringBuilder builder, int index, string value)
        {
            JoinStringItem(builder, index, value, ", ");
        }

        public static string JoinString(this IEnumerable<string> strs, string joinStr)
        {
            StringBuilder builder = new StringBuilder();
            int index = 0;
            foreach (string value in strs)
                JoinStringItem(builder, index++, value, joinStr);
            return builder.ToString();
        }

        public static string JoinString(this IEnumerable<string> strs)
        {
            return strs.JoinString(", ");
        }

        /// <summary>
        /// 格式化SQL语句中的WHERE条件
        /// </summary>
        /// <param name="sql">SQL字符串</param>
        /// <param name="whereClause">where子句</param>
        /// <returns>被替换where子句的sql</returns>
        internal static string ReplaceWhereClause(string sql, string whereClause)
        {
            AtawDebug.AssertArgumentNullOrEmpty(sql, "sql", null);
            AtawDebug.AssertArgumentNull(whereClause, "whereClause", null);

            int index = sql.IndexOf("WHERE", StringComparison.OrdinalIgnoreCase);
            string result;
            if (index > 0)
                result = sql.Substring(0, index - 1);
            else
                result = sql;
            result += " " + whereClause;
            return result;
        }

        private static string[] fStartChars = {"啊", "芭", "擦","搭","蛾","发","噶","哈","击","击","喀","垃","妈","拿","哦", 
                                                 "啪","期","然", "撒","塌","挖","挖","挖","昔","压","匝"};
        private static string[] fEndChars = {"澳", "怖", "错","堕","贰","咐","过","祸","啊","骏","阔","络","穆","诺","沤", 
                                               "瀑", "群","弱", "所","唾","啊","啊","误","迅","孕","座"};

        private static int[] fStartBytes = {45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062,
                                              49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698,
                                              52698, 52980, 53689, 54481};
        private static int[] fEndBytes = {45252, 45760, 46317, 46825, 47009, 47296, 47613, 48118, 45217, 49061, 49323, 
                                            49895, 50370, 50613, 50621, 50905, 51386, 51445, 52217, 52697, 45217, 45217,
                                            52979, 53688, 54480, 55289};
        /// <summary>
        /// 获得汉字的拼音
        /// </summary>
        /// <param name="hz">汉字</param>
        /// <returns>汉字拼音</returns>
        private static char GetCharPY(char hz)
        {
            byte[] arr = Encoding.Default.GetBytes(hz.ToString());
            if (arr.Length > 1)
            {
                int temp = arr[0];
                temp <<= 8; // * 256
                temp += arr[1];
                for (int i = 0; i < fStartBytes.Length; ++i)
                {
                    if (temp < fStartBytes[i])
                        break;
                    if (temp >= fStartBytes[i] && temp <= fEndBytes[i])
                        return (char)(i + 'a');
                }
            }
            return '\0';
        }

        /// <summary>
        /// 获得字符串的拼音
        /// </summary>
        /// <param name="hz">汉字字符串</param>
        /// <returns>字符串的拼音</returns>
        public static string GetPY(string hz)
        {
            if (string.IsNullOrEmpty(hz))
                return String.Empty;

            StringBuilder result = new StringBuilder();
            foreach (char c in hz)
            {
                char py = GetCharPY(c);
                if (py != '\0')
                    result.Append(py);
            }
            return result.ToString();
        }

        public static string EscapeString(string source, string[] replaceString, Func<char, int> escapeFunc)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            AtawDebug.AssertArgumentNull(escapeFunc, "escapeFunc", null);
            AtawDebug.AssertEnumerableArgumentNull<string>(replaceString, "replaceString", null);

            StringBuilder cachedStringBuilder = null;
            int start = 0;
            int pos = 0;
            int count = source.Length;
            for (int i = 0; i < count; ++i)
            {
                pos = escapeFunc(source[i]);
                if (pos < 0)
                    continue;
                if (cachedStringBuilder == null)
                    cachedStringBuilder = new StringBuilder();
                cachedStringBuilder.Append(source.Substring(start, i - start));
                AtawDebug.Assert(pos < replaceString.Length, string.Format(ObjectUtil.SysCulture,
                    "参数escapeFunc返回的值有误，replaceString的长度为{0}，但是返回值却是{1}，越界了",
                    replaceString.Length, pos), null);
                cachedStringBuilder.Append(replaceString[pos]);
                start = i + 1;
            }
            if (start == 0)
                return source;
            else if (start < count)
                cachedStringBuilder.Append(source.Substring(start, count - start));
            string s = cachedStringBuilder.ToString();
            return s;
        }

        private static string[] fXmlReplacements = new string[] { "&amp;", "&lt;", "&gt;", "&apos;" };
        public static string EscapeXmlString(string source)
        {
            return EscapeString(source, fXmlReplacements, c =>
            {
                switch (c)
                {
                    case '&':
                        return 0;
                    case '<':
                        return 1;
                    case '>':
                        return 2;
                    case '\'':
                        return 3;
                    default:
                        return -1;
                }
            });
        }

        private static string[] SqlReplacements = new string[] { @"\\", @"\%", @"\_" };
        public static string EscapeSqlString(string source)
        {
            return EscapeString(source, SqlReplacements, c =>
            {
                switch (c)
                {
                    case '\\':
                        return 0;
                    case '%':
                        return 1;
                    case '_':
                        return 2;
                    default:
                        return -1;
                }
            });
        }

        private static string[] AposReplacement = new string[] { "''" };
        public static string EscapeAposString(string source)
        {
            return EscapeString(source, AposReplacement, c =>
            {
                switch (c)
                {
                    case '\'':
                        return 0;
                    default:
                        return -1;
                }
            });
        }

        /// <summary>
        /// Determines whether the string is all white space. Empty string will return false.
        /// </summary>
        /// <param name="str">The string to test whether it is all white space.</param>
        /// <returns>
        /// 	<c>true</c> if the string is all white space; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWhiteSpace(string str)
        {
            AtawDebug.AssertArgumentNull(str, "str", null);

            if (str.Length == 0)
                return false;

            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsWhiteSpace(str[i]))
                    return false;
            }

            return true;
        }

        public static StringWriter CreateStringWriter(int capacity)
        {
            StringBuilder sb = new StringBuilder(capacity);
            StringWriter sw = new StringWriter(sb, ObjectUtil.SysCulture);

            return sw;
        }

        private static char IntToHex(int n)
        {
            if (n <= 9)
            {
                return (char)(n + 48);
            }
            return (char)((n - 10) + 97);
        }

        public static string AsUnicode(this char ch)
        {
            char h1 = IntToHex((ch >> 12) & '\x000f');
            char h2 = IntToHex((ch >> 8) & '\x000f');
            char h3 = IntToHex((ch >> 4) & '\x000f');
            char h4 = IntToHex(ch & '\x000f');

            return new string(new[] { '\\', 'u', h1, h2, h3, h4 });
        }

        public static string TruncString(string value, int length)
        {
            if (value == null)
                return string.Empty;

            if (value.Length > (length >> 1))
            {
                byte[] bytes = fGBEncoding.GetBytes(value);
                if (bytes.Length > length)
                {
                    string result = fGBEncoding.GetString(bytes, 0, length);
                    return result.Substring(0, result.Length - 1);
                }
            }
            return value;
        }

        public static bool IsNotEmpty(params string[] strs)
        {
            if (strs == null || strs.Length == 0)
                return false;
            return strs.All(str => !string.IsNullOrEmpty(str));
        }

        public static string StrToHex(string str)
        {
            string s = "";
            for (int i = 0; i < str.Length; i++)
            {
                s += Convert.ToInt64(Convert.ToChar(str[i].ToString())) + ",";
            }
            return s.TrimEnd(',');
        }

        public static string hexToString(string hex)
        {
            string s = "";
            if (hex != null)
            {
                for (int i = 0; i < hex.Split(',').Length; i++)
                {
                    try
                    {
                        // 每两个字符是一个 byte。 
                        s += Convert.ToChar(Convert.ToInt64(hex.Split(',')[i]));
                    }
                    catch
                    {
                        s = hex;
                    }
                }
            }
            return s;
        }
    }
}
