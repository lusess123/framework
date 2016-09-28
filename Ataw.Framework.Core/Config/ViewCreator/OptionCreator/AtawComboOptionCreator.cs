
namespace Ataw.Framework.Core
{
    [CodePlug("Combo", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "Combo控件参数创建插件")]
    public class AtawComboOptionCreator : AtawBaseMultiOptionCreator
    {
        private ComboOptions fComboOptions;

        public AtawComboOptionCreator()
        {
            fComboOptions = new ComboOptions();
            BaseOptions = fComboOptions;
        }
    }
}
