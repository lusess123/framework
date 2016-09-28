
namespace Ataw.Framework.Core
{
    public class BaseUploadOptions : BaseOptions
    {
        //public string HttpPath { get; set; }

        //public string FileName { get; set; }

        public int FileSize { get; set; }

        public string FileExtension { get; set; }

        public string StorageName { get; set; }

        public string UploadName { get; set; }

        public int ImageSizeHeight { get; set; }
        
        public int ImageSizeWidth { get; set; }
        //public UploadConfig Upload { get; set; }
        public bool HasDocumentCenter { get; set; }
    }
}
