
namespace Ataw.Framework.Core
{
    [CodePlug("ListBox", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2014-06-12", Author = "zgl", Description = "CheckBox控件参数创建插件")]
    public class AtawListBoxOptionCreator : AtawBaseOptionCreator
    {
        private ListBoxOptions fListBoxOptions;

        public AtawListBoxOptionCreator()
        {
            fListBoxOptions = new ListBoxOptions();
            BaseOptions = fListBoxOptions;
        }
    }
}
