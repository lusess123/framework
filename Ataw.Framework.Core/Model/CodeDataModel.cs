
namespace Ataw.Framework.Core
{
    public class CodeDataModel : ObjectData
    {
        public string CODE_VALUE { get; set; }
        private string code_text;
        public string CODE_TEXT { get { return htmlDecode(code_text); } set { code_text = value; } }
        public bool IsSelect { get; set; }
        private string htmlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            str = str.Replace("&amp;", "&");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace(" &nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            str = str.Replace("&#39;", "\'");
            return str;
        }

    }
}
