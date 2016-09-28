
namespace Ataw.Framework.Core
{
    [CodePlug("TextArea", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "TextArea控件参数创建插件")]
    public class AtawTextAreaOptionCreator : AtawBaseOptionCreator
    {
        private TextAreaOptions fTextAreaOptions;

        public AtawTextAreaOptionCreator()
        {
            fTextAreaOptions = new TextAreaOptions();
            BaseOptions = fTextAreaOptions;
        }
    }
}
