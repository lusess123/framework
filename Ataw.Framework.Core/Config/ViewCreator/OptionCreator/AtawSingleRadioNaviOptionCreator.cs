
namespace Ataw.Framework.Core
{
    [CodePlug("SingleRadioNavi", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-11-29", Author = "ace", Description = "SingleRadioNavi")]
    public class AtawSingleRadioNaviOptionCreator : AtawRadioNaviOptionCreator
    {
        private AtawSingleRadioNaviOptions fAtawSingleRadioNaviOptions;

        public AtawSingleRadioNaviOptionCreator()
        {
            fAtawSingleRadioNaviOptions = new AtawSingleRadioNaviOptions();
            BaseOptions = fAtawSingleRadioNaviOptions;
        }
    }
}
