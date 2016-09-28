
using System.Xml.Serialization;
namespace Ataw.Framework.Core
{
    public class ReportConfig
    {
        public ReportConfig()
        {
            Enable = true;
        }

        public string FormatText
        {
            get;
            set;
        }

        [XmlAttribute]
        public bool Enable
        {
            get;
            set;
        }
    }
}
