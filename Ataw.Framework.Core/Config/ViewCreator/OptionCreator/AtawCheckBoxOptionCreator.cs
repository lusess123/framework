
namespace Ataw.Framework.Core
{
    [CodePlug("CheckBox", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "CheckBox控件参数创建插件")]
    public class AtawCheckBoxOptionCreator : AtawBaseMultiOptionCreator
    {
        private CheckBoxOptions fCheckBoxOptions;

        public AtawCheckBoxOptionCreator()
        {
            fCheckBoxOptions = new CheckBoxOptions();
            BaseOptions = fCheckBoxOptions;
        }
    }
}
