using System;

namespace Ataw.Workflow.Core.DataAccess
{
    public class WF_WORKFLOW_DEF
    {
        public string WD_ID { get; set; }
        public string WD_SHORT_NAME { get; set; }//标识名
        public string WD_NAME { get; set; }//名称
        public string WD_DESCRIPTION { get; set; }//描述
        public string WD_CONTENT { get; set; }//定义内容
        public Nullable<int> WD_IS_USED { get; set; }//是否使用
        public string WD_VERSION { get; set; }//版本号
        public string WD_VERSION_DESC { get; set; }//版本描述
        public string WD_CREATE_ID { get; set; }//创建人
        public System.DateTime WD_CREATE_DATE { get; set; }//创建时间
        public string WD_UPDATE_ID { get; set; }
        public System.DateTime WD_UPDATE_DATE { get; set; }
        public Nullable<int> WD_UNUSED1 { get; set; }
        public Nullable<int> WD_UNUSED2 { get; set; }
        public string WD_UNUSED3 { get; set; }
        public string WD_UNUSED4 { get; set; }
        public string WD_UNUSED5 { get; set; }
        public string FControlUnitID { get; set; }
    }
}
