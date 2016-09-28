using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class FileRootConfig
    {

        private int indexHttp = 0;
        private int indexFtp = 0;
        private int indexMMS = 0;

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public int ID { get; set; }

        [XmlAttribute]
        public string PhysicalPath { get; set; }

        [XmlArrayItem("FtpPath")]
        public List<FtpPathConfig> FtpPathes { get; set; }

        [XmlArrayItem("HttpPath")]
        public List<HttpPathConfig> HttpPathes { get; set; }

        [XmlArrayItem("MMSPath")]
        public List<MMSPathConfig> MMSPathes { get; set; }

        public string GetPath(FilePathScheme scheme)
        {
            switch (scheme)
            {
                case FilePathScheme.Ftp:
                    return FtpPath;
                case FilePathScheme.Http:
                    return HttpPath;
                case FilePathScheme.MMS:
                    return MMSPath;
                case FilePathScheme.Physical:
                    return PhysicalPath;
                default:
                    return "";
            }
        }

        public string HttpPath
        {
            get
            {
                return HttpPathes[indexHttp].Value;
            }
        }

        public string FtpPath
        {
            get
            {
                return FtpPathes[indexFtp].Value;
            }
        }

        public string MMSPath
        {
            get
            {
                return MMSPathes[indexMMS].Value;
            }
        }
    }
}
