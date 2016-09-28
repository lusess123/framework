
using System.Data;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Ataw.Framework.Core
{
    public class ColumnGroupConfig
    {
        /// <summary>
        /// 排序编号
        /// </summary>
        [XmlAttribute]
        public int Order { get; set; }

        /// <summary>
        /// 显示格式，0或4表示占整行，1表示占1/4，2表示占一半，3表示占3/4
        /// </summary>
        [XmlAttribute]
        public int ShowType { get; set; }

        /// <summary>
        /// 组名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 组显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 组内的所有字段名，以逗号分隔
        /// </summary>
        public string ColumnNames { get; set; }

        [XmlIgnore]
        public List<ColumnConfig> Columns { get; set; }
    }
}