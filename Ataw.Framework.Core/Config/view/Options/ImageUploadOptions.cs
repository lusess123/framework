using System.Dynamic;
using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    public class ImageUploadOptions : BaseUploadOptions
    {
        /// <summary>
        ///图片裁剪后大小（长度）
        /// </summary>
        public int ImageSizeWidth { get; set; }

        /// <summary>
        /// 图片裁剪后大小（宽度）
        /// </summary>
        public int ImageSizeHeight { get; set; }
    }
}
