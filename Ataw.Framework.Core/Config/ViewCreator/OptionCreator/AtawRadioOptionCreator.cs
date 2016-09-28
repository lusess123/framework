
namespace Ataw.Framework.Core
{
    [CodePlug("Radio", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "Radio控件参数创建插件")]
    public class AtawRadioOptionCreator : AtawBaseMultiOptionCreator
    {
        private RadioOptions fRadioOptions;

        public AtawRadioOptionCreator()
        {
            fRadioOptions = new RadioOptions();
            BaseOptions = fRadioOptions;
        }
    }
}
