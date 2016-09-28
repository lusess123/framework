
namespace Ataw.Framework.Core
{
    [CodePlug("DetailDate", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "DetailDate控件参数创建插件")]
    public class AtawDetailDateOptionCreator : AtawDetailOptionCreator
    {
        private DetailDateOptions fDetailDateOptions;

        public AtawDetailDateOptionCreator()
        {
            fDetailDateOptions = new DetailDateOptions();
            BaseOptions = fDetailDateOptions;
        }
    }
}
