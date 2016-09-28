using System.Data;

namespace Ataw.Framework.Core
{
    [CodePlug("MultiSelector", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-15", Author = "sj", Description = "多选选择器控件参数创建插件")]
    public class AtawMultiSelectorOptionCreator : AtawSelectorOptionCreator
    {
        public AtawMultiSelectorOptionCreator()
        {
            fSelectorOptions = new MultiSelectorOptions();
            BaseOptions = fSelectorOptions;
        }
    }
}
