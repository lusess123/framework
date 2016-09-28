using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    /// <summary>
    /// 宏表达式解析类
    /// </summary>
    public class AtawExpressionParser
    {
        private readonly List<AtawExpressionParameter> fParameters;
        private readonly char fOpen;
        private readonly char fClose;
        private AtawExpressionParser(char open, char close)
        {
            fOpen = open;
            fClose = close;
            fParameters = new List<AtawExpressionParameter>();
        }

        public string FormatString { get; private set; }

        public IEnumerable<AtawExpressionParameter> Parameters { get { return fParameters; } }

        public void ParseExpr(string format)
        {
            FormatString = format;

            var ptr = 0;
            var length = format.Length;

            while (ptr < length)
            {
                var c = format[ptr++];
                if (c == fOpen)
                {
                    // check for escaped open bracket

                    if (format[ptr] == fOpen)
                    {
                        ptr++;
                        continue;
                    }

                    var marcoStart = ptr;

                    while (format[ptr++] != fClose)
                        if (ptr >= length)
                            break;

                    AtawDebug.Assert(ptr <= length, string.Format(ObjectUtil.SysCulture,
                        "扫描字符串{0}发现，存在只有{1}而没有与之匹配的{2}结束的字符串", format, fOpen, fClose), format);

                    string marco = format.Substring(marcoStart, ptr - marcoStart - 1);
                    AtawDebug.Assert(!string.IsNullOrEmpty(marco), string.Format(ObjectUtil.SysCulture,
                        "字符串{0}中存在{1}与{2}之间没有任何宏名称，空宏是不被允许的", format, fOpen, fClose), format);
                    fParameters.Add(new AtawExpressionParameter() { ParameterName = marco, StartIndex = marcoStart - 1 });

                    //result.Append("{" + (marcoCount++) + "}");
                }
                else if (c == fClose && ptr < length && format[ptr] == fClose)
                {
                    //result.Append(format, start, ptr - start - 1);
                    ptr++;
                    //result.Append("}");
                }
                else if (c == fClose)
                {
                    AtawDebug.Assert(false, string.Format(ObjectUtil.SysCulture,
                        "扫描字符串{0}发现，存在单独的{2}，如果要显示{2}，请写两个{2}，否则请与{1}配对", format, fOpen, fClose), format);
                }
            }
        }

        public static AtawExpressionParser ParseExpression(string expression, char open = char.MinValue, char close = char.MinValue)
        {
            if (close == char.MinValue)
                close = open;
            if (open == char.MinValue)
            {
                open = '{';
                close = '}';
            }

            AtawExpressionParser parser = new AtawExpressionParser(open, close);
            parser.ParseExpr(expression);
            return parser;
        }
    }
}
