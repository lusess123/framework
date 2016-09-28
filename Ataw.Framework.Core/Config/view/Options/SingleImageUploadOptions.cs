
namespace Ataw.Framework.Core
{
    public class SingleImageUploadOptions : BaseUploadOptions
    {

        /// <summary>
        ///图片裁剪后大小（长度）
        /// </summary>
        public int ImageSizeWidth { get; set; }

        /// <summary>
        /// 图片裁剪后大小（宽度）
        /// </summary>
        public int ImageSizeHeight { get; set; }

        /// <summary>
        /// 是否可裁剪
        /// </summary>
        public bool IsCut { get; set; }
    }
}
