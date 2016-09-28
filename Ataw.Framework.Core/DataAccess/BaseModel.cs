
using System;

namespace Ataw.Framework.Core
{
    public class AtawBaseModel
    {
        public string FID { get; set; } // FID (Primary key). 地区信息

        public string CREATE_ID { get; set; } // CREATE_ID. 创建人
        public DateTime? CREATE_TIME { get; set; } // CREATE_TIME. 创建时间
        public string UPDATE_ID { get; set; } // UPDATE_ID. 最后编辑人
        public DateTime? UPDATE_TIME { get; set; } // UPDATE_TIME. 最后编辑时间
        public bool? ISDELETE { get; set; } // ISDELETE. 是否删除
        public string UDVARCHAR1 { get; set; } // UDVARCHAR1. 附加字符串字段
        public string UDVARCHAR2 { get; set; } // UDVARCHAR2. 附加字符串字段
        public int? UDINT1 { get; set; } // UDINT1. 附加整型字段1
        public DateTime? UDDATETIME { get; set; } // UDDATETIME. 附加时间字段8

        /// <summary>
        /// 时间戳
        /// </summary>
        public string TIMESSTAMP { get; set; }
        public string FControlUnitID { get; set; } // FControlUnitID. 组织机构
    }
}
