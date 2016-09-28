
namespace Ataw.Framework.Core
{
    [CodePlug("Custom", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "Text控件参数创建插件")]
    public class AtawCustomOptionCreator : AtawBaseOptionCreator
    {
        private CustomOptions fCustomOptions;

        public AtawCustomOptionCreator()
        {
            fCustomOptions = new CustomOptions();
            BaseOptions = fCustomOptions;
        }
    }
}
