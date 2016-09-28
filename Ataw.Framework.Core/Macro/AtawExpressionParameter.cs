namespace Ataw.Framework.Core
{
    public class AtawExpressionParameter
    {
        public string ParameterName { get; set; }
        public int StartIndex { get; set; }
        public int Count { get { return ParameterName.Length + 2; } }//记得包含open和close两个字符
        public string Value { get; set; }
    }
}