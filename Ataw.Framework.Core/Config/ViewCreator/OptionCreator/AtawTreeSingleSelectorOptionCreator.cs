
namespace Ataw.Framework.Core
{
    [CodePlug("TreeSingleSelector", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "TreeSingleSelector控件参数创建插件")]
    public class AtawTreeSingleSelectorOptionCreator : AtawSelectorOptionCreator
    {
        //protected TreeSingleSelectorOptions fTreeSelectorOptions;

        public AtawTreeSingleSelectorOptionCreator()
        {
            fSelectorOptions = new TreeSingleSelectorOptions();
            BaseOptions = fSelectorOptions;
        }

        public override BaseOptions Create()
        {
            var options = base.Create();
            (options as TreeSingleSelectorOptions).TreeConfig = Config.TreeConfig;
            return options;
        }
    }
}
