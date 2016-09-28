using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class UploadConfig
    {
        public UploadConfig()
        {
            HasKey = true;
           // HasDocumentCenter = true;
        }
         [XmlAttribute]
        public bool HasDocumentCenter { get; set; }
        /// <summary>
        ///  对应于FileManagementConfig.xml的FileUpload节点的Name值
        /// </summary>
        public string UploadName { get; set; }

        /// <summary>
        ///  对应于FileManagementConfig.xml的FileStorage节点的Name值
        /// </summary>
        public string StorageName { get; set; }

        /// <summary>
        /// 图片上传是否可裁剪
        /// </summary>
        [XmlAttribute]
        public bool IsCut { get; set; }

        /// <summary>
        /// 图片上传是否拥有主键访问
        /// </summary>
        [XmlAttribute]
        public bool HasKey { get; set; }

        /// <summary>
        /// 最小上传数
        /// </summary>
        [XmlAttribute]
        public int MinUploadCount { get; set; }

        ///// <summary>
        ///// 文件路径
        ///// </summary>
        //public string FilePath { get; set; }

        ///// <summary>
        ///// 文件扩展名
        ///// </summary>
        //public string FileExtension { get; set; }

        ///// <summary>
        ///// 文件大小
        ///// </summary>
        //public long FileLength { get; set; }

        ///// <summary>
        /////图片裁剪后大小（长度）
        ///// </summary>
        //public int ImageSizeWidth { get; set; }

        ///// <summary>
        ///// 图片裁剪后大小（宽度）
        ///// </summary>
        //public int ImageSizeHeight { get; set; }
    }
}
