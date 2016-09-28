using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    [Serializable]
    public class FileManagementConfig : FileXmlConfigBase, IReadXmlCallback
    {
        private const string CORE_SIGN = "Core";

        public FileManagementConfig()
        {
            ImageCutGroups = new List<ImageCutGroup>();
        }
        [XmlArrayItem("ImageCutGroup")]
        public List<ImageCutGroup> ImageCutGroups { get; set; }

        [XmlArrayItem("FileRoot")]
        public List<FileRootConfig> FileRoots { get; set; }

        [XmlArrayItem("FileUpload")]
        public List<FileUploadConfig> FileUploads { get; set; }

        [XmlArrayItem("FileStorage")]
        public List<FileStorageConfig> FileStorages { get; set; }

        public static FileManagementConfig ReadConfig(string binPath)
        {
            string fpath = Path.Combine(binPath, AtawApplicationConfig.REAL_PATH, "FileManagementConfig.xml");
            var _config = XmlUtil.ReadFromFile<FileManagementConfig>(fpath);
            return MergeFileManagementConfig(_config);
        }

        public static FileManagementConfig MergeFileManagementConfig(FileManagementConfig root)
        {
            //string fpath = Path.Combine(binPath, AtawApplicationConfig.REAL_PATH, "FileManagementConfig","FileManagementConfig.xml");
            var _list = ReadConfigFromDir();
            foreach(var config in _list){
                root.FileRoots.AddRange(config.FileRoots);
                root.FileStorages.AddRange(config.FileStorages);
                root.FileUploads.AddRange(config.FileUploads);
                root.ImageCutGroups.AddRange(config.ImageCutGroups);
            }
            return root;
        }
        public static IList<FileManagementConfig> ReadConfigFromDir()
        {
            string fpath = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH,
                "FileManagement");
            FileUtil.ConfirmPath(fpath);
            string[] xmls = Directory.GetFiles(fpath, "*.xml", SearchOption.AllDirectories);
            IList<FileManagementConfig> dictXmlList = new List<FileManagementConfig>();
            foreach (var dict in xmls)
            {
                dictXmlList.Add(XmlUtil.ReadFromFile<FileManagementConfig>(dict));
            }
            return dictXmlList;
        }

        public void OnReadXml()
        {
            ImageCutGroup group = ImageCutGroups.FirstOrDefault(a => a.Name == CORE_SIGN);
            if (group != null)
            {
                ImageCutGroups.ForEach(a =>
                {
                    if (a.Name != CORE_SIGN)
                    {
                        foreach (ImageCut cut in group.ImageCuts)
                        {
                            bool isNoImg = a.ImageCuts.Exists(c => c.ImageSizeHeight != cut.ImageSizeHeight && c.ImageSizeWidth != cut.ImageSizeWidth);
                            if (isNoImg)
                            {
                                a.ImageCuts.Add(cut);
                            }
                        }
                    }
                });
            }
        }
    }
}
