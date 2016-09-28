using System;
using System.IO;
using System.Text;

namespace Ataw.Framework.Core
{
    public static class FileUtil
    {

        public static Uri FileNameToUri(string fileName)
        {
            AtawDebug.AssertArgumentNullOrEmpty(fileName, "fileName", null);

            Uri uri = new Uri(fileName);
            return uri;
        }

        /// <summary>
        /// 将数据保存到相应的文件中
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="content">需要保存的数据</param>
        /// <param name="encoding">文件的编码形式</param>
        public static void SaveFile(string fileName, string content, Encoding encoding)
        {
            AtawDebug.AssertArgumentNullOrEmpty(fileName, "fileName", null);
            AtawDebug.AssertArgumentNull(content, "content", null);
            AtawDebug.AssertArgumentNull(encoding, "encoding", null);

            FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read);
            using (TextWriter writer = new StreamWriter(file, encoding))
            {
                writer.Write(content);
            }
        }

        public static void SaveFile(string fileName, string content)
        {
            SaveFile(fileName, content, Encoding.Default);
        }

        public static void ConfirmPath(string path)
        {
            AtawDebug.AssertArgumentNullOrEmpty(path, "path", null);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static void VerifySaveFile(string fileName, string content, Encoding encoding)
        {
            AtawDebug.AssertArgumentNullOrEmpty(fileName, "fileName", null);

            string path = Path.GetDirectoryName(fileName);
            ConfirmPath(path);
            SaveFile(fileName, content, encoding);
        }

        public static void VerifySaveFile(string fileName, string content)
        {
            VerifySaveFile(fileName, content, Encoding.Default);
        }

        internal static string GetXmlFilePattern(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
                return "*.xml";
            AtawDebug.Assert(pattern.EndsWith(".xml", StringComparison.OrdinalIgnoreCase),
                string.Format(ObjectUtil.SysCulture, "{0}应该是以.xml结束，而现在不是", pattern), null);
            return pattern;
        }

        public static string ChangeFileName(string path, string newFileNameTitle)
        {
            string dirPath = Path.GetDirectoryName(path);
            string oldFileNameTitle = Path.GetFileNameWithoutExtension(path);

            string _ext = Path.GetExtension(path);

            return Path.Combine(dirPath, newFileNameTitle + _ext);
        }

        public static string ChangeFileNameAddSize(string path, string addSize)
        {
            if (!addSize.IsEmpty())
            {
                string _name = Path.GetFileNameWithoutExtension(path);
                string newName = "{0}_{1}".AkFormat(_name, addSize);
                return ChangeFileName(path, newName);
            }
            else
                return path;
        }

        public static string ReadStringByFile(string fileName)
        {
            var en = GetFileEncodeType(fileName);
            using (FileStream myStream = new FileStream(fileName, FileMode.Open))
            {
                using (StreamReader myStreamReader = new StreamReader(myStream, en))  
                {

                    string fileContent = myStreamReader.ReadToEnd();
                    //  Encoding.GetEncoding("GB18030").GetString(buff)
                    myStreamReader.Close();
                    return fileContent;
                }
            }
        }

        public static System.Text.Encoding GetFileEncodeType(string filename)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                using (System.IO.BinaryReader br = new System.IO.BinaryReader(fs))
                {
                    Byte[] buffer = br.ReadBytes(2);
                    if (buffer[0] >= 0xEF)
                    {
                        if (buffer[0] == 0xEF && buffer[1] == 0xBB)
                        {
                            return System.Text.Encoding.UTF8;
                        }
                        else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                        {
                            return System.Text.Encoding.BigEndianUnicode;
                        }
                        else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                        {
                            return System.Text.Encoding.Unicode;
                        }
                        else
                        {
                            return System.Text.Encoding.Default;
                        }
                    }
                    else
                    {
                        return System.Text.Encoding.Default;
                    }
                }
            }
        }

        public static void CopyDirectory(String sourcePath, String destinationPath)
        {
            DirectoryInfo info = new DirectoryInfo(sourcePath);
            Directory.CreateDirectory(destinationPath);
            foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
            {
                String destName = Path.Combine(destinationPath, fsi.Name);

                if (fsi is System.IO.FileInfo)          //如果是文件，复制文件
                    File.Copy(fsi.FullName, destName);
                else                                    //如果是文件夹，新建文件夹，递归
                {
                    Directory.CreateDirectory(destName);
                    CopyDirectory(fsi.FullName, destName);
                }
            }
        }
    }
}
