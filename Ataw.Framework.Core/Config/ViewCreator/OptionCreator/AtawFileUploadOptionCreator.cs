
namespace Ataw.Framework.Core
{
    [CodePlug("FileUpload", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "FileUpload控件参数创建插件")]
    public class AtawFileUploadOptionCreator : AtawBaseUploadOptionCreator
    {
        //private FileUploadOptions fFileUploadOptions;

        public AtawFileUploadOptionCreator()
        {
            //fFileUploadOptions = new FileUploadOptions();
            //BaseOptions = fFileUploadOptions;
            fBaseUploadOptions = new FileUploadOptions();
            BaseOptions = fBaseUploadOptions;
        }
    }
}
