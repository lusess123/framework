
namespace Ataw.Framework.Core
{
    [CodePlug("Detail", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "Detail控件参数创建插件")]
    public class AtawDetailOptionCreator : AtawDecodeOptionCreator
    {
        private DetailOptions fDetailOptions;

        public AtawDetailOptionCreator()
        {
            fDetailOptions = new DetailOptions();
            BaseOptions = fDetailOptions;
        }
    }
}
