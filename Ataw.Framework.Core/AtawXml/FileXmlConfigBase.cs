using System.IO;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class FileXmlConfigBase : XmlConfigBase
    {
        protected FileXmlConfigBase()
        {
            //BasePath = string.Empty;
            BasePath = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH);
        }
        [XmlIgnore]
        public string BasePath { get; set; }
        [XmlIgnore]
        public string FileName { get; protected set; }
        [XmlIgnore]
        public string FilePath { get; private set; }

        private string GetPath(string fileName)
        {
            string path = string.IsNullOrEmpty(BasePath) ? fileName
                : Path.Combine(BasePath, fileName);
            FileName = fileName;
            FilePath = path;
            return path;
        }

        public virtual void SaveFileByName()
        {
            AtawDebug.AssertArgumentNullOrEmpty(FileName, "FileName", this);
            SaveFileByName(FileName);
        }

        public virtual void SaveFileByName(string fileName)
        {
            string filePath = GetPath(fileName);
            SaveFile(filePath);
        }

        protected virtual void LoadConfig()
        {

        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(FilePath) ? base.ToString() :
                string.Format(ObjectUtil.SysCulture, "Xml文件名为{0}的配置类", FilePath);
        }
    }
}
