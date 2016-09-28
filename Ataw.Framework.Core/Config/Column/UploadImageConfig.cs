using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    /// <summary>
    /// 上传图片实体类
    /// </summary>
    public class UploadImageConfig
    {
        /// <summary>
        /// 图片名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// 是否保存原尺寸
        /// </summary>
        public bool IsFullSize { get; set; }

        /// <summary>
        /// 等比缩放的尺寸集合
        /// </summary>
        public List<int> SizeList { get; set; }
    }
}
