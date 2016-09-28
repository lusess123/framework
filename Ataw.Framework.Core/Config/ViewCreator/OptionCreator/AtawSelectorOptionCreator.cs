using System.Data;

namespace Ataw.Framework.Core
{
    [CodePlug("Selector", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-15", Author = "sj", Description = "选择器控件参数创建插件")]
    public class AtawSelectorOptionCreator : AtawDecodeOptionCreator
    {
        protected SelectorOptions fSelectorOptions;

        public AtawSelectorOptionCreator()
        {
            fSelectorOptions = new SelectorOptions();
            BaseOptions = fSelectorOptions;
        }
    }
}
