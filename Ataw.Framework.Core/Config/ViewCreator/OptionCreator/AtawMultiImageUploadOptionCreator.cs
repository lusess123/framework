
namespace Ataw.Framework.Core
{
    [CodePlug("MultiImageUpload", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "MultiImageUpload控件参数创建插件")]
    public class AtawMultiImageUploadOptionCreator : AtawSingleImageUploadOptionCreator
    {

        public AtawMultiImageUploadOptionCreator()
        {
            fBaseUploadOptions = new MultiImageUploadOptions();
            BaseOptions = fBaseUploadOptions;
        }

        public override BaseOptions Create()
        {
            var dv = base.Create();
            ((MultiImageUploadOptions)dv).IsCut = false;
            ((MultiImageUploadOptions)dv).MinUploadCount = this.Config.Upload.MinUploadCount;
            return dv;
        }
    }
}
