
namespace Ataw.Framework.Core
{
    [CodePlug("Link", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-1-10", Author = "sj", Description = "Link控件参数创建插件")]
    public class AtawLinkOptionCreator : AtawBaseOptionCreator
    {
        private LinkOptions fLinkOptions;

        public AtawLinkOptionCreator()
        {
            fLinkOptions = new LinkOptions();
            BaseOptions = fLinkOptions;
        }
    }
}
