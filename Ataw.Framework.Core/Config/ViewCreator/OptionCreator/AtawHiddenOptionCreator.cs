
namespace Ataw.Framework.Core
{
    [CodePlug("Hidden", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "Hidden控件参数创建插件")]
    public class AtawHiddenOptionCreator : AtawBaseMultiOptionCreator
    {
        private HiddenOptions fHiddenOptions;

        public AtawHiddenOptionCreator()
        {
            fHiddenOptions = new HiddenOptions();
            BaseOptions = fHiddenOptions;
        }
    }
}
