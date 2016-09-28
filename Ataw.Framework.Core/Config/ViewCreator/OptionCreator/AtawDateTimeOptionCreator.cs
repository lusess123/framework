
namespace Ataw.Framework.Core
{
    [CodePlug("DateTime", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "DateTime控件参数创建插件")]
    public class AtawDateTimeOptionCreator : AtawBaseOptionCreator
    {
        private DateTimeOptions fDateTimeOptions;

        public AtawDateTimeOptionCreator()
        {
            fDateTimeOptions = new DateTimeOptions();
            BaseOptions = fDateTimeOptions;
        }
    }
}
