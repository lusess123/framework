
namespace Ataw.Framework.Core
{
    [CodePlug("Password", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "Password控件参数创建插件")]
    public class AtawPasswordOptionCreator : AtawBaseOptionCreator
    {
        private PasswordOptions fPasswordOptions;

        public AtawPasswordOptionCreator()
        {
            fPasswordOptions = new PasswordOptions();
            BaseOptions = fPasswordOptions;
        }
    }
}
