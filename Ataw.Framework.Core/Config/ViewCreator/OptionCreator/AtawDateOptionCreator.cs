
namespace Ataw.Framework.Core
{
    [CodePlug("Date", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "Date控件参数创建插件")]
    public class AtawDateOptionCreator : AtawBaseOptionCreator
    {
        private DateOptions fDateOptions;

        public AtawDateOptionCreator()
        {
            fDateOptions = new DateOptions();
            BaseOptions = fDateOptions;
        }
    }
}
