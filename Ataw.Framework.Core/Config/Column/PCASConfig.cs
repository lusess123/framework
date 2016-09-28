using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class PCASConfig
    {
        public PCASConfig()
        {
            PCASRequiredCount = 3;
            PCASDisplayItemsCount = 3;
        }

        /// <summary>
        /// PCAS控件的必填项个数
        /// </summary>
        [XmlAttribute]
        public int PCASRequiredCount { get; set; }

        /// <summary>
        /// PCAS控件显示筛选项的个数
        /// </summary>
        [XmlAttribute]
        public int PCASDisplayItemsCount { get; set; }


    }
}
