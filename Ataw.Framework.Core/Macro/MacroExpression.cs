using System;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    public class MacroExpression
    {
        public static string Execute(string expressionString, char open = char.MinValue, char close = char.MinValue, object input = null)
        {
            var parser = AtawExpressionParser.ParseExpression(expressionString, open, close);
            if (!parser.Parameters.Any())
                return parser.FormatString;
            var count = 0;
            var sb = new StringBuilder();
            var index = 0;
            parser.Parameters.ToList().ForEach(a =>
            {
                var temp = a.ParameterName.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (temp.Length > 1)
                {
                    //带参的表达式
                    var item = temp[0].CodePlugIn<IParamExpression>();
                    a.Value = item.Execute(temp[1], input);
                }
                else
                {
                    //无参表达式
                    var item = a.ParameterName.CodePlugIn<IExpression>();
                    a.Value = item.Execute();
                }
            });
            parser.Parameters.ToList().ForEach(parameter =>
            {
                sb.Append(parser.FormatString.Substring(index, parameter.StartIndex - index));
                sb.Append(parameter.Value ?? string.Empty);
                index = parameter.StartIndex + parameter.Count;
            });
            if (parser.FormatString.Length > index)
                sb.Append(parser.FormatString.Substring(index));
            return sb.ToString();
        }
    }
}
