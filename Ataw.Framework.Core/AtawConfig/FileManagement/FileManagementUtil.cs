using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ataw.Framework.Core
{

    public static class FileManagementUtil
    {
        public static FileManagementConfig FileManagementConfig
        {
            get
            {
                return AtawAppContext.Current.FileManagementConfigXml;
            }
        }

        #region FileRoot
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootID">文件根编号</param>
        /// <param name="scheme">文件存储类型</param>
        /// <returns></returns>
        public static string GetRootPath(int rootID, FilePathScheme scheme)
        {
            var fileRoot = FileManagementConfig.FileRoots.FirstOrDefault(a => a.ID == rootID);
            if (fileRoot != null)
            {
                return fileRoot.GetPath(scheme);
            }
            return "";
        }

        /// <summary>
        /// 返回文件根路径
        /// </summary>
        /// <param name="fileStorageName">文件存储名称</param>
        /// <param name="scheme">文件存储类型</param>
        /// <returns></returns>
        public static string GetRootPath(string fileStorageName, FilePathScheme scheme)
        {
            return GetRootPath(GetRootID(fileStorageName), scheme);
        }

        #endregion

        #region FileUpload
        /// <summary>
        /// 检验文件大小
        /// </summary>
        /// <param name="file">上传的文件</param>
        /// <param name="fileUploadName">上传文件类型名</param>
        /// <returns></returns>
        public static bool CheckFileSize(int fileSize, string fileUploadName)
        {
            var fileUpload = FileManagementConfig.FileUploads.FirstOrDefault(a => a.Name == fileUploadName);
            int maxSize = fileUpload == null ? 0 : fileUpload.MaxSize;
            return maxSize == 0 || fileSize <= maxSize;
        }
        ///// <summary>
        ///// 检验文件扩展名
        ///// </summary>
        ///// <param name="file">上传的文件</param>
        ///// <param name="fileUploadName">上传文件类型名</param>
        ///// <returns></returns>
        //public static bool CheckFileExtension(string fileName, string fileUploadName)
        //{
        //    return CheckFileExtension(fileName, fileUploadName);
        //}
        /// <summary>
        /// 检验文件扩展名
        /// </summary>
        /// <param name="filePath">文件所在路径</param>
        /// <param name="fileUploadName">上传文件类型名</param>
        /// <returns></returns>
        public static bool CheckFileExtension(string filePath, string fileUploadName)
        {
            var fileUpload = FileManagementConfig.FileUploads.FirstOrDefault(a => a.Name == fileUploadName);
            if (fileUpload == null)
                return true;
            string pattern = fileUpload.Extensions;
            string extension = Path.GetExtension(filePath);
            return Regex.IsMatch(extension, pattern, RegexOptions.IgnoreCase);
        }

        #endregion

        #region FileStorage
        /// <summary>
        /// 返回文件根编号
        /// </summary>
        /// <param name="fileStorageName">文件存储名称</param>
        /// <returns></returns>
        public static int GetRootID(string fileStorageName)
        {
            var fileStorage = FileManagementConfig.FileStorages.FirstOrDefault(a => a.Name == fileStorageName);
            return fileStorage == null ? -1 : fileStorage.FileRootID;
        }
        /// <summary>
        /// 返回文件名
        /// </summary>
        /// <param name="fileStorageName">文件存储名称</param>
        /// <param name="paramFile">文件存储类型</param>
        /// <param name="paramExt">文件扩展名</param>
        /// <returns></returns>
        public static string GetFileName(string fileStorageName, object paramFile, string paramExt)
        {
            var fileStorage = FileManagementConfig.FileStorages.FirstOrDefault(a => a.Name == fileStorageName);
            if (fileStorage == null)
                return string.Format("{0}{1}", paramFile, paramExt);
            return string.Format(fileStorage.FileNameFormat,
                paramFile, paramExt);
        }
        public static string GetFileName<T>(string fileStorageName, T paramFile, string paramExt)
        {
            var fileStorage = FileManagementConfig.FileStorages.FirstOrDefault(a => a.Name == fileStorageName);
            if (fileStorage == null)
                return string.Format("{0}{1}", paramFile, paramExt);
            return string.Format(fileStorage.FileNameFormat,
                paramFile, paramExt);

        }
        /// <summary>
        /// 返回相对路径
        /// </summary>
        /// <param name="fileStorageName">文件存储名称</param>
        /// <param name="paramPath">路径编号</param>
        /// <returns></returns>
        public static string GetRelativePath(string fileStorageName, object paramPath)
        {
            var fileStorage = FileManagementConfig.FileStorages.FirstOrDefault(a => a.Name == fileStorageName);
            if (fileStorage == null)
                return paramPath.ToString();
            return string.Format(fileStorage.FilePathFormat, paramPath);
        }
        public static string GetRelativePath<T>(string fileStorageName, T paramPath)
        {
            var fileStorage = FileManagementConfig.FileStorages.FirstOrDefault(a => a.Name == fileStorageName);
            if (fileStorage == null)
                return paramPath.ToString();
            return string.Format(fileStorage.FilePathFormat, paramPath);
        }
        /// <summary>
        /// 返回完整路径
        /// </summary>
        /// <param name="fileStorageName">文件存储名称</param>
        /// <param name="scheme">文件存储类型</param>
        /// <param name="paramPath">路径编号</param>
        /// <param name="paramFile">文件编号</param>
        /// <param name="paramExt">文件扩展名</param>
        /// <returns></returns>
        public static string GetFullPath(string fileStorageName, FilePathScheme scheme, object paramPath, object paramFile, string paramExt)
        {
            string result = GetRootPath(fileStorageName, scheme);
            result = Path.Combine(result, GetRelativePath(fileStorageName, paramPath));
            result = Path.Combine(result, GetFileName(fileStorageName, paramFile, paramExt));

            return new Uri(result).ToString();
        }
        public static string GetFullPath<T1, T2>(string fileStorageName, FilePathScheme scheme, T1 paramPath, T2 paramFile, string paramExt)
        {
            string result = GetRootPath(fileStorageName, scheme);
            result = Path.Combine(result, GetRelativePath<T1>(fileStorageName, paramPath));
            result = Path.Combine(result, GetFileName<T2>(fileStorageName, paramFile, paramExt));

            return new Uri(result).ToString();
        }

        public static string GetPhysicalPath(string fileStorageName, string fileExtension, string guid )
        {
            string fileID = guid;
            int pathID = fileID.GuidSafeSub();
           return  FileManagementUtil.GetFullPath(fileStorageName, FilePathScheme.Physical, pathID, fileID, fileExtension);
        
        }

        /// <summary>
        /// 保存临时文件的方法
        /// </summary>
        /// <param name="file">上传的文件</param>
        /// <param name="fileID">文件编号</param>
        /// <param name="fileStorageName">文件存储名称</param>
        public static void FileSaveAs(string file, int fileID, string fileStorageName, string tempPath)
        {
            FileSaveAs(file, fileID, fileID, fileStorageName, tempPath);
        }
        /// <summary>
        /// 保存临时文件的方法
        /// </summary>
        /// <param name="file">上传的文件</param>
        /// <param name="pathID">路径编号,默认为文件编号</param>
        /// <param name="fileID">文件编号</param>
        /// <param name="fileStorageName">文件类型在配置文件中的名称，如"Music2006_Original"</param>
        public static void FileSaveAs(string file, int pathID, int fileID, string fileStorageName, string tempPath)
        {
            string fullPath = new Uri(GetFullPath(fileStorageName, FilePathScheme.Physical, pathID, fileID, Path.GetExtension(file))).LocalPath.Replace(GetRootPath(fileStorageName, FilePathScheme.Physical), tempPath);

            ForeDirectories(GetParentDirectory(fullPath));
            File.Copy(file, fullPath);
        }

        /// <summary>
        /// 保存临时文件的方法
        /// </summary>
        /// <param name="file">上传的文件</param>
        /// <param name="tempPath">上传的文件路径</param>
        public static void FileSaveAs(string file, string tempPath)
        {
            string fullPath = new Uri(tempPath).LocalPath;
            ForeDirectories(GetParentDirectory(fullPath));
            File.Copy(file, fullPath);
        }

        /// <summary>
        /// 保存文件的方法
        /// </summary>
        /// <param name="file">上传的文件</param>
        /// <param name="fileID">文件编号</param>
        /// <param name="fileStorageName">文件存储名称</param>
        public static void FileSaveAs(string file, int fileID, string fileStorageName)
        {
            FileSaveAs(file, fileID, fileID, fileStorageName);
        }
        /// <summary>
        /// 保存文件的方法
        /// </summary>
        /// <param name="file">上传的文件</param>
        /// <param name="pathID">路径编号</param>
        /// <param name="fileID">文件编号</param>
        /// <param name="fileStorageName">文件存储名称</param>
        public static void FileSaveAs(string file, int pathID, int fileID, string fileStorageName)
        {
            string fullPath = new Uri(GetFullPath(fileStorageName, FilePathScheme.Physical, pathID, fileID, Path.GetExtension(file))).LocalPath;
            ForeDirectories(GetParentDirectory(fullPath));
            File.Copy(file, fullPath, true);
        }

        /// <summary>
        /// 验证并确保文件路径存在
        /// </summary>
        /// <param name="path">所检验的文件路径</param>
        public static void ForeDirectories(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        /// <summary>
        /// 返回指定路径的上级目录
        /// </summary>
        /// <param name="path">所指定的文件路径</param>
        /// <returns></returns>
        public static string GetParentDirectory(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            return info.Parent.FullName;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch
                {
                }
            }
        }

        //public static string getFileUploadInfo(string fileUploadName)
        //{
        //    var fileUpload = FileManagementConfig.FileUploads.FirstOrDefault(a => a.Name == fileUploadName);
        //    int fileMaxSize = fileUpload.MaxSize;
        //    string Extensions = fileUpload.Extensions;
        //    string filePath = fileUpload.FilePath;
        //    if (fileUploadName == "ImageUpload")
        //    {
        //        int ImageSizeWidth = fileUpload.ImageSizeWidth;
        //        int ImageSizeHeight = fileUpload.ImageSizeHeight;
        //        return fileMaxSize + " " + Extensions + " " + filePath + " " + ImageSizeWidth + " " + ImageSizeHeight;
        //    }
        //    return fileMaxSize + " " + Extensions + " " + filePath;
        //}

        public static FileUploadConfig GetFileUploadConfig(string uploadName)
        {
            var _res = FileManagementConfig.FileUploads.FirstOrDefault(a => a.Name == uploadName);
            AtawDebug.AssertArgumentNull(_res,
                string.Format(ObjectUtil.SysCulture, "名称为{0}的上传配置节点不能为空", uploadName),
                AtawAppContext.Current);
            return _res;
        }

        public static IList<ImageCut> GetImageCutsByUploadName(string uploadName)
        {
            FileUploadConfig config = GetFileUploadConfig(uploadName);
            AtawDebug.AssertArgumentNullOrEmpty(config.Name,
                 string.Format(ObjectUtil.SysCulture, "名称为{0}的上传配置节点不存在截图配置", uploadName),
                AtawAppContext.Current);

            return GetImageCutsByCutConfigName(config.ImageCutGroupName);
        }

        public static IList<ImageCut> GetImageCutsByCutConfigName(string cutGroupName)
        {
            ImageCutGroup cut = FileManagementConfig.ImageCutGroups.Find(a => a.Name == cutGroupName);

            AtawDebug.Assert(cut != null && cut.ImageCuts.Count > 0, string.Format(ObjectUtil.SysCulture,
                "名称为{0}的截图配置不存在,或者没有截图配置项", cutGroupName), AtawAppContext.Current);

            return cut.ImageCuts;
        }
        #endregion

        #region 根据guid 获取
        /// <summary>
        /// 根据guid,尺寸获取单张图片,如果size为空，返回原图路径
        /// </summary>
        /// <param name="config"></param>
        /// <param name="guid"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string GetHttpPath(string config, string guid, string size)
        {
            if (guid.Length < 8)
                return "";
            int pathID = guid.GuidSafeSub();
            string fullPath = FileManagementUtil.GetFullPath(config, FilePathScheme.Http, pathID, guid, "");

            string _str = FileUtil.ChangeFileNameAddSize(fullPath, size).Replace(@"http:\", "http://");
            return _str.Replace("\\", "/");

        }

        public static List<string> GetImageHttpList(string arrangeString, string config, string size)
        {
            List<string> https = new List<string>();
            ResourceArrange arrange = arrangeString.SafeJsonObject<ResourceArrange>();
            if (arrange != null && arrange.ResourceInfoList != null && arrange.ResourceInfoList.Count > 0 )
            {
                if (arrange.CoverIndex >= 0)
                {
                    https.Add(GetHttpPath(config, arrange.ResourceInfoList[arrange.CoverIndex].FileId, size));
                }
                for (int i = 0; i < arrange.ResourceInfoList.Count; i++)
                {
                    if (i != arrange.CoverIndex)
                        https.Add(GetHttpPath(config, arrange.ResourceInfoList[i].FileId, size));
                }
            }

            return https;
        }
        #endregion

        public static bool CanDocumentView(string ext)
        {
            ext = ext.ToLower();
            string _exts = ".doc|.docx|.xls|.xlsx|.ppt|.pptx|.pdf|.txt|.log|.js|.css|.xml|.html|.chtml|.tt|.config|.java|.xaml|.php";
            return _exts.IndexOf(ext + "|") >= 0;


        }

    }
}
