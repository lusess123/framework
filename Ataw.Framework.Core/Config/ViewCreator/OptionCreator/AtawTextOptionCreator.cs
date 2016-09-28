
namespace Ataw.Framework.Core
{
    [CodePlug("Text", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "Text控件参数创建插件")]
    public class AtawTextOptionCreator : AtawDecodeOptionCreator
    {
        private TextOptions fTextOptions;

        public AtawTextOptionCreator()
        {
            fTextOptions = new TextOptions();
            BaseOptions = fTextOptions;
        }
    }
}
