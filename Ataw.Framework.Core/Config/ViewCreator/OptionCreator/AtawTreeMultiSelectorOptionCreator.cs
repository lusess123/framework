
namespace Ataw.Framework.Core
{
    [CodePlug("TreeMultiSelector", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "TreeMultiSelector控件参数创建插件")]
    public class AtawTreeMultiSelectorOptionCreator : AtawTreeSingleSelectorOptionCreator
    {
        public AtawTreeMultiSelectorOptionCreator()
        {
            fSelectorOptions = new TreeMultiSelectorOptions();
            BaseOptions = fSelectorOptions;
        }
    }
}
