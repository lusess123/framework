using System;
using System.Collections.Generic;
using System.IO;

namespace Ataw.Framework.Core
{
    public class ResourceArrange
    {
        public ResourceArrange()
        {
            ResourceInfoList = new List<ResourceInfo>();
        }

        public List<ResourceInfo> ResourceInfoList
        {
            get;
            set;
        }

        public int CoverIndex
        {
            get;
            set;
        }

        private void MoveFile(string sPath, string tPath)
        {
            //sPath = new Uri(sPath).LocalPath;
            // tPath = new Uri(tPath).LocalPath;
            if (File.Exists(sPath))
            {
                if (File.Exists(tPath))
                {
                    File.Delete(tPath);

                }
                FileUtil.ConfirmPath(Path.GetDirectoryName(tPath));
                File.Move(sPath, tPath);

                //--------------------修改数据
            }
        }

        private void MoveKeyPathByIndex(int index, string newGuid, string storageName, bool isOnly = false)
        {

            ResourceInfo info = ResourceInfoList[index];
            if (!isOnly)
            {
                info.StorageConfigName = storageName;
            }
            string storage = storageName;
            string fileExtension = info.ExtName;
            int newPathID = newGuid.GuidSafeSub();
            string newPath = FileManagementUtil.GetFullPath(storage, FilePathScheme.Physical, newPathID, newGuid, fileExtension);
            newPath = new Uri(newPath).LocalPath;
            int oldPathId = info.PathID;
            string oldFileId = info.FileId;
            string oldPath = FileManagementUtil.GetFullPath(info.StorageConfigName, FilePathScheme.Physical, oldPathId, oldFileId, fileExtension);
            oldPath = new Uri(oldPath).LocalPath;
            if (oldPath != newPath)
            {
                MoveFile(oldPath, newPath);



                foreach (var cut in info.ExtendList)
                {
                    string _oldPathExt = Path.GetFileNameWithoutExtension(oldPath);
                    string _oldpath = FileUtil.ChangeFileName(oldPath, "{0}_{1}".AkFormat(_oldPathExt, cut.Value));

                    string _newPathExt = Path.GetFileNameWithoutExtension(newPath);
                    string _newpath = FileUtil.ChangeFileName(newPath, "{0}_{1}".AkFormat(_newPathExt, cut.Value));

                    MoveFile(_oldpath, _newpath);
                }

                //---------------改变了
                info.FileId = newGuid;
                info.PathID = newPathID;
                info.PhysicalPath = newPath;
                info.HttpPath = FileManagementUtil.GetFullPath(info.StorageConfigName,
                    FilePathScheme.Http, newPathID, newGuid, fileExtension);
            }

        }

        public void MoveKeyPath(string newGuid, string storageName, bool isOnly = false)
        {
            if (ResourceInfoList.Count > 0)
            {
                for (int i = 0; i < ResourceInfoList.Count; i++)
                {
                    var _bean = ResourceInfoList[i];
                    if (_bean.FileId == newGuid && i != CoverIndex)
                    {
                        var _guid = AtawAppContext.Current.UnitOfData.GetUniId();
                        MoveKeyPathByIndex(i, _guid, storageName, isOnly);
                    }
                }
                //多张图片，如果删除最后一张作为封面的图片，CoverIndex应当-1 added by sj
                CoverIndex = ResourceInfoList.Count == CoverIndex ? CoverIndex - 1 : CoverIndex;
                MoveKeyPathByIndex(CoverIndex, newGuid, storageName, isOnly);
            }
        }

        public string GetListImg()
        {
            //if(files)
            if (this.CoverIndex >= 0 && ResourceInfoList.Count > 0)
            {
                var rouseInfo = ResourceInfoList[this.CoverIndex];
                string _http = rouseInfo.GetHttp();
                string _str = FileUtil.ChangeFileNameAddSize(_http, "38-38").Replace(@"http:\", "http://");
                return _str.Replace("\\", "/");
            }
            else
            {
                return "/Content/PlatformThemes/SapphireBlue/images/default-photo.jpg";
            }

        }
    }
}
