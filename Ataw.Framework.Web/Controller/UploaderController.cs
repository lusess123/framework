using System;
using System.IO;
using System.Web;
using Ataw.Framework.Core;
namespace Ataw.Framework.Web
{
    public class UploaderController : AtawBaseController
    {
        private const string TH_PATH = "/Images/Thumbs/";
        // private const string TH_PATH = "/Files/Thumbs/";

        private readonly string ThumbsPath = GlobalVariable.Context.Server.MapPath(TH_PATH);


        private ResourceInfo SaveFileByStorageName(HttpPostedFileBase file, string fileStorageName)
        {
            ResourceInfo info = new ResourceInfo();

            string fileExtension = Path.GetExtension(file.FileName);

            if (fileStorageName.IsEmpty() || fileStorageName.ToUpper(ObjectUtil.SysCulture) == "NULL")
            {
                fileStorageName = "Temp";
            }
            string fileID = AtawAppContext.Current.UnitOfData.GetUniId();
            int pathID = fileID.GuidSafeSub();
            string fullPath = FileManagementUtil.GetFullPath(fileStorageName, FilePathScheme.Physical, pathID, fileID, fileExtension);
            fullPath = new Uri(fullPath).LocalPath;
            FileManagementUtil.ForeDirectories(FileManagementUtil.GetParentDirectory(fullPath));
            file.SaveAs(fullPath);

            //判断是否可以转换
            bool isCan = FileManagementUtil.CanDocumentView(fileExtension);
            if (isCan)
            {
                //放到图片预览区
                string fileName = Path.GetFileName(fullPath);
                string _dvPath = Path.Combine(AtawAppContext.Current.XmlPath, AtawAppContext.Current.DocViewerXml.DocViewerPath, "~Cache_Document", fileName);
                if (!System.IO.File.Exists(_dvPath))
                {
                    //FileUtil.VerifySaveFile(
                    string path = Path.GetDirectoryName(_dvPath);
                   // ConfirmPath(path);
                    FileUtil.ConfirmPath(path);
                    file.SaveAs(_dvPath);
                }
            }
            string _httpPath = FileManagementUtil.GetFullPath(fileStorageName, FilePathScheme.Http, pathID, fileID, fileExtension);
            info.CanDocumentView = isCan;
            info.StorageConfigName = fileStorageName;
            info.ExtName = fileExtension;
            info.FileId = fileID;
            info.DocumentViewId = fileID + fileExtension;
            info.FileNameTitle = Path.GetFileNameWithoutExtension(file.FileName);
            info.FileSizeK = file.ContentLength / 1024;
            info.InfoType = ResourceInfoType.Config;
            info.PathID = pathID;
            info.HttpPath = _httpPath;
            info.PhysicalPath = fullPath;

            return info;
        }

        public class UploadInfo
        {
            public int error;
            public string title;
            public string url;
            public string message;
        }

        public string UploadKindFile()
        {
            var result = new UploadInfo();
            //定义允许上传的文件扩展名
            //var extTable = new Dictionary<string, string>();
            //extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            //extTable.Add("flash", "swf,flv");
            //extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            //extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");
            //var dirName = Request.QueryString["dir"];
            //if (string.IsNullOrEmpty(dirName))
            //    dirName = "image";

            try
            {
                var res = SaveFileByStorageName(Request.Files.Get("imgFile"), "KindEditorFileStorage");
                result.error = 0;
                result.title = res.FileNameTitle + res.ExtName;
                result.url = res.HttpPath;
                //FastJson(new XhUploadInfoDetail() { url = res.HttpPath, localname = res.FileNameTitle });
                //return ReturnJson(res);
            }
            catch (Exception e)
            {
                result.error = 1;
                result.message = e.Message;
            }
            return FastJson(result);
        }

        public string UploadFile(string fileStorageName)
        {
            //保存临时文件
            HttpPostedFileBase file = Request.Files[0];

            var res = SaveFileByStorageName(file, fileStorageName);

            return ReturnJson(res);
        }

        public string UploadCkeditorFile(string CKEditor, string CKEditorFuncNum)
        {
            HttpPostedFileBase file = Request.Files[0];

            var res = SaveFileByStorageName(file, "CkeditorFileStorage");

            return "<html><body><script type=\"text/javascript\">window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ",\"" + res.HttpPath + "\" );</script></body></html>";
        }

        /// <summary>
        /// 菜鸟前台上传图片需要的方法,返回格式化json
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileStorageName"></param>
        ///  <param name="uploadName"></param>
        /// <returns></returns>
        public string UploadWebFile(HttpPostedFileBase file, string fileStorageName, string uploadName)
        {
            var info = SaveFileByStorageName(file, fileStorageName);
            string oldPath = info.PhysicalPath;
            string fileName = Path.GetFileName(oldPath);
            string oldTitle = Path.GetFileNameWithoutExtension(oldPath);
            if (!uploadName.IsEmpty())
            {
                var _cuts = FileManagementUtil.GetImageCutsByUploadName(uploadName);
                foreach (ImageCut cut in _cuts)
                {
                    string newFile = FileUtil.ChangeFileName(oldPath, cut.GetNewFileTitle(oldTitle));
                    ImageUtil.SavePictureInFile(oldPath, fileName, cut.ImageSizeWidth, cut.ImageSizeHeight, newFile,cut.Quality);
                    string _ff = "{0}-{1}".AkFormat(cut.ImageSizeWidth, cut.ImageSizeHeight);
                    info.ExtendList.Add(_ff, _ff);
                }
            }
            return FastJson(info);
        }

        public class UploadClass
        {
            public string FileName { get; set; }
            public string FileExtension { get; set; }
            public string HttpPath { get; set; }
            public int FileSize { get; set; }
        }

        public string CutImageByConfig(string fileStorageName, string SourcePath, string width, string height)
        {
            return "";
        }

        public string UploadImage(string fileStorageName, string width, string height, bool isCut, string uploadName)
        {
            //保存临时文件
            HttpPostedFileBase file = Request.Files[0];

            ResourceInfo info = SaveFileByStorageName(file, fileStorageName);
            if (!isCut)
            {
                string oldPath = info.PhysicalPath;
                string fileName = Path.GetFileName(oldPath);
                string oldTitle = Path.GetFileNameWithoutExtension(oldPath);

                if (uploadName.IsEmpty())
                {
                    if (!height.IsEmpty() && !width.IsEmpty())
                    {
                        string newFile = FileUtil.ChangeFileName(oldPath, ImageCut.GetNewFileTitle(oldTitle, width.Value<int>(), height.Value<int>()));
                        ImageUtil.SavePictureInFile(oldPath, fileName, width.Value<int>(), height.Value<int>(), newFile);
                        string _ff = "{0}-{1}".AkFormat(width, height);
                        info.ExtendList.Add(_ff, _ff);
                    }
                }
                else
                {
                    var _cuts = FileManagementUtil.GetImageCutsByUploadName(uploadName);
                    foreach (ImageCut cut in _cuts)
                    {
                        string newFile = FileUtil.ChangeFileName(oldPath, cut.GetNewFileTitle(oldTitle));
                        ImageUtil.SavePictureInFile(oldPath, fileName, cut.ImageSizeWidth, cut.ImageSizeHeight, newFile,cut.Quality);
                        string _ff = "{0}-{1}".AkFormat(cut.ImageSizeWidth, cut.ImageSizeHeight);
                        info.ExtendList.Add(_ff, _ff);
                    }
                }
                //ImageUtil.SavePictureInFile(oldPath, fileName, width.Value<int>(), height.Value<int>(), fullPath);

                //System.IO.File.Delete(tempPath);
            }
            else
            {

            }

            return ReturnJson(info);

            //获取文件夹物理路径
            //string path = ThumbsPath;
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            ////获取文件后缀
            //string fileExtension = Path.GetExtension(file.FileName);
            //string fileExtensionButDot = Path.GetExtension(file.FileName).Substring(1);

            //string fileID = AtawAppContext.Current.UnitOfData.GetUniId();
            //int pathID = fileID.Substring(0, 8).Value<int>();
            //AtawDebug.Assert(pathID > 0, "文件夹字符串不能为空", this);
            //string relativePath = Path.Combine(FileManagementUtil.GetRelativePath(fileStorageName, pathID.Value<int>()),
            //    FileManagementUtil.GetFileName(fileStorageName, fileID, fileExtension));
            //string fullPath = new Uri(Path.Combine(FileManagementUtil.GetRootPath(fileStorageName, FilePathScheme.Physical), relativePath)).LocalPath;
            //FileManagementUtil.ForeDirectories(FileManagementUtil.GetParentDirectory(fullPath));
            //string newFileName = fileID + fileExtension;
            //string tempPath = path + newFileName;
            //file.SaveAs(tempPath);
            //string httpPath = "";
            //if (!isCut)
            //{
            //    httpPath = FileManagementUtil.GetFullPath(fileStorageName, FilePathScheme.Http, pathID, fileID, fileExtensionButDot);
            //    ImageUtil.SavePictureInFile(tempPath, newFileName, width.Value<int>(), height.Value<int>(), fullPath);
            //    System.IO.File.Delete(tempPath);
            //}
            //else
            //{
            //    httpPath = TH_PATH + newFileName;
            //}

            //string res = FastJson(
            //    new
            //    {
            //        FileName = Path.GetFileNameWithoutExtension(file.FileName),
            //        FileExtension = fileExtension,
            //        HttpPath = httpPath, //当图片需要裁减时，该值是临时路径，不需要裁减则值为当前图片保存在服务器上的路径
            //        FileSize = file.ContentLength / 1024,
            //    }
            //    );
            //return res;
        }


        //删除文件
        public void RemoveImage(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                string[] paths = url.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var obj in paths)
                {
                    string path = Server.MapPath(obj);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
            }
        }

        //上传控件 包括图片和文件
        public string UploadFileAndImage(string fileStorageName, string uploadName, string width = "100", string height = "100", bool isCut = false)
        {
            //保存临时文件
            HttpPostedFileBase file = Request.Files[0];
            ResourceInfo info = SaveFileByStorageName(file, fileStorageName);
            
            if (file.ContentType == "image/jpeg")
            {
                //上传图片
                info.IsImage = true;
                if (!isCut)
                {
                    string oldPath = info.PhysicalPath;
                    string fileName = Path.GetFileName(oldPath);
                    string oldTitle = Path.GetFileNameWithoutExtension(oldPath);

                    if (uploadName.IsEmpty())
                    {
                        if (!height.IsEmpty() && !width.IsEmpty())
                        {
                            string newFile = FileUtil.ChangeFileName(oldPath, ImageCut.GetNewFileTitle(oldTitle, width.Value<int>(), height.Value<int>()));
                            ImageUtil.SavePictureInFile(oldPath, fileName, width.Value<int>(), height.Value<int>(), newFile);
                            string _ff = "{0}-{1}".AkFormat(width, height);
                            info.ExtendList.Add(_ff, _ff);
                        }
                    }
                    else
                    {
                        var _cuts = FileManagementUtil.GetImageCutsByUploadName(uploadName);
                        foreach (ImageCut cut in _cuts)
                        {
                            string newFile = FileUtil.ChangeFileName(oldPath, cut.GetNewFileTitle(oldTitle));
                            ImageUtil.SavePictureInFile(oldPath, fileName, cut.ImageSizeWidth, cut.ImageSizeHeight, newFile, cut.Quality);
                            string _ff = "{0}-{1}".AkFormat(cut.ImageSizeWidth, cut.ImageSizeHeight);
                            info.ExtendList.Add(_ff, _ff);
                        }
                    }
                }
            }
            else
            {
                //上传文件
                info.IsImage = false;
            }

            return ReturnJson(info);
        }
    }
}
