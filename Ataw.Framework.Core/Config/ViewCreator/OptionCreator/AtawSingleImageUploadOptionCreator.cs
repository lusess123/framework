
namespace Ataw.Framework.Core
{
    [CodePlug("SingleImageUpload", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "SingleImageUpload控件参数创建插件")]
    public class AtawSingleImageUploadOptionCreator : AtawBaseUploadOptionCreator
    {

        public AtawSingleImageUploadOptionCreator()
        {
            var _temp = new SingleImageUploadOptions();
            // _temp.IsCut = false;
            fBaseUploadOptions = _temp;
            // fBaseUploadOptions.isc
            BaseOptions = fBaseUploadOptions;
            //BaseOptions
        }


    }
}
