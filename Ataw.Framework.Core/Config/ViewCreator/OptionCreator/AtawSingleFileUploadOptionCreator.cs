
namespace Ataw.Framework.Core
{
    [CodePlug("SingleFileUpload", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "SingleFileUpload控件参数创建插件")]
    public class AtawSingleFileUploadOptionCreator : AtawBaseUploadOptionCreator
    {

        public AtawSingleFileUploadOptionCreator()
        {
            fBaseUploadOptions = new SingleFileUploadOptions();
            BaseOptions = fBaseUploadOptions;
        }
    }
}
