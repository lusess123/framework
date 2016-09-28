
namespace Ataw.Framework.Core
{
    [CodePlug("MultiFileUpload", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "FileUpload控件参数创建插件")]
    public class AtawMultiFileUploadOptionCreator : AtawSingleFileUploadOptionCreator
    {

        public AtawMultiFileUploadOptionCreator()
        {
            fBaseUploadOptions = new MultiFileUploadOptions();
            BaseOptions = fBaseUploadOptions;
        }
    }
}
