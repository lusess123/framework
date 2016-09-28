
namespace Ataw.Framework.Core
{
    [CodePlug("ImageUpload", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "ImageUpload控件参数创建插件")]
    public class AtawImageUploadOptionCreator : AtawBaseUploadOptionCreator
    {

        public AtawImageUploadOptionCreator()
        {
            fBaseUploadOptions = new ImageUploadOptions();
            BaseOptions = fBaseUploadOptions;
        }
    }
}
