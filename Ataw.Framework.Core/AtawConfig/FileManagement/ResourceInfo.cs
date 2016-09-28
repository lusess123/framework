using System.Collections.Generic;
namespace Ataw.Framework.Core
{
    public class ResourceInfo
    {
        public ResourceInfo()
        {
            ExtendList = new Dictionary<string, string>();
        }

        public Dictionary<string, string> ExtendList
        {
            get;
            set;
        }

        public string DocumentViewId { get; set; }

        public bool CanDocumentView { get; set; }

        public bool IsDocument { get; set; }

        public bool IsCover { get; set; }

        /// <summary>
        /// 区分文件类型
        /// </summary>
        public bool IsImage { get; set; }

        public ResourceInfoType InfoType { get; set; }
        public string Url { get; set; }

        public string SiteInUrl { get; set; }

        /// <summary>
        /// 原文件存储配置的名称
        /// </summary>
        public string StorageConfigName { get; set; }

        public string FileId { get; set; }
        /// <summary>
        /// FileId的一部分，一般为8位时间格式
        /// </summary>
        public int PathID { get; set; }
        public string ExtName { get; set; }


        /// <summary>
        /// 原文件名
        /// </summary>
        public string FileNameTitle { get; set; }
        /// <summary>
        /// 原文件大小
        /// </summary>
        public int FileSizeK { get; set; }


        private string fHttpPath;
        public string HttpPath
        {
            get;
            set;
        }

        private string fPhysicalPath;
        public string PhysicalPath
        {
            get;
            set;
        }

        public string GetAndSetUrl()
        {
            switch (InfoType)
            {
                case ResourceInfoType.WebSite:
                    Url = AtawAppContext.Current.WebRootPath + SiteInUrl;
                    break;
                case ResourceInfoType.Config:
                    AtawDebug.AssertNotNullOrEmpty(StorageConfigName, "资源保存的配置名不能为空", this);
                    AtawDebug.AssertNotNullOrEmpty(FileId, "资源的编号不能为空", this);
                    GetHttp();
                    break;
                default:
                    break;
            }

            return Url;
        }

        public string GetHttp()
        {
            if (StorageConfigName.IsEmpty())
                return HttpPath;
            if (PathID <= 0)
            {
                PathID = FileId.GuidSafeSub();

            }
            string str = FileManagementUtil.GetFullPath(StorageConfigName, FilePathScheme.Http, PathID, FileId, ExtName);
            Url = str.Replace(@"\", "/");
            return Url;
        }
    }

    public enum ResourceInfoType
    {
        None = 0,
        Link = 1,
        WebSite = 2,
        Config = 3
    }
}
