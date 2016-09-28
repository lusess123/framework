
namespace Ataw.Framework.Core
{
    [CodePlug("DetailArea", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-1-10", Author = "sj", Description = "DetailArea控件参数创建插件")]
    public class AtawDetailAreaOptionCreator : AtawTextAreaOptionCreator
    {
        private DetailAreaOptions fDetailAreaOptions;

        public AtawDetailAreaOptionCreator()
        {
            fDetailAreaOptions = new DetailAreaOptions();
            BaseOptions = fDetailAreaOptions;
        }
    }
}
