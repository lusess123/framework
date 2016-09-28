
namespace Ataw.Framework.Core
{
    [CodePlug("DownLink", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-1-10", Author = "sj", Description = "DownLink控件参数创建插件")]
    public class AtawDownLinkOptionCreator : AtawLinkOptionCreator
    {
        private DownLinkOptions fLinkOptions;

        public AtawDownLinkOptionCreator()
        {
            fLinkOptions = new DownLinkOptions();
            BaseOptions = fLinkOptions;
        }
    }
}
