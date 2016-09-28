using System;

namespace Ataw.Workflow.Core.DataAccess
{
    public class WF_WORKFLOW_TRANSFER
    {
        public string WT_ID { get; set; }
        public string WT_TITLE { get; set; }
        public string WT_TO_USER { get; set; }
        public string WT_FROM_USER { get; set; }
        public Nullable<int> WT_TURN_TYPE { get; set; }
        public string WT_WI_ID { get; set; }
        public string WT_WD_NAME { get; set; }
        public Nullable<System.DateTime> WT_END_TIME { get; set; }
        public Nullable<int> WT_IS_SUCCESS { get; set; }
        public string WT_DATA { get; set; }
        public string WT_NOTE { get; set; }
        public string WT_CREATE_ID { get; set; }
        public Nullable<System.DateTime> WT_CREATE_DATE { get; set; }
        public string WT_UPDATE_ID { get; set; }
        public Nullable<System.DateTime> WT_UPDATE_DATE { get; set; }
        public string WT_WF_ID { get; set; }
        public Nullable<int> WT_IS_APPLY_WF { get; set; }
        public string WT_STEP_STATUS { get; set; }
        public string WT_STEP_NAME { get; set; }
        public Nullable<int> WT_WF_IS_END { get; set; }
        public string WT_WF_STATUS { get; set; }
        public Nullable<int> WT_UNUSER1 { get; set; }
        public Nullable<int> WT_UNUSER2 { get; set; }
        public string WT_UNUSER3 { get; set; }
        public string WT_UNUSER4 { get; set; }
        public string WT_UNUSER5 { get; set; }
    }
}
