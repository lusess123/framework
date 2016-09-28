using System;

namespace Ataw.Workflow.Core.DataAccess
{
    public class WF_WORKFLOW_INST
    {
        public string WI_ID { get; set; }
        public string WI_WD_NAME { get; set; }
        public string WI_NAME { get; set; }
        public string WI_CONTENT_XML { get; set; }
        public Nullable<System.DateTime> WI_CREATE_DATE { get; set; }
        public string WI_CREATE_USER { get; set; }
        public Nullable<System.DateTime> WI_END_DATE { get; set; }
        public string WI_END_USER { get; set; }
        public string WI_LAST_STEP { get; set; }
        public string WI_LAST_STEP_NAME { get; set; }
        public string WI_LAST_MANUAL { get; set; }
        public string WI_LAST_MANUAL_NAME { get; set; }
        public string WI_CURRENT_STEP { get; set; }
        public string WI_CURRENT_STEP_NAME { get; set; }
        public Nullable<System.DateTime> WI_CURRENT_CREATE_DATE { get; set; }
        public Nullable<int> WI_STEP_TYPE { get; set; }
        public string WI_CUSTOM_DATA { get; set; }
        public string WI_SEND_ID { get; set; }
        public Nullable<System.DateTime> WI_SEND_DATE { get; set; }
        public Nullable<int> WI_RECEIVE_COUNT { get; set; }
        public string WI_RECEIVE_LIST { get; set; }
        public string WI_RECEIVE_ID { get; set; }
        public Nullable<System.DateTime> WI_RECEIVE_DATE { get; set; }
        public string WI_PROCESS_ID { get; set; }
        public Nullable<System.DateTime> WI_PROCESS_DATE { get; set; }
        public string WI_REF_LIST { get; set; }
        public Nullable<int> WI_ERROR_TYPE { get; set; }
        public Nullable<int> WI_STATUS { get; set; }
        public Nullable<int> WI_PRIORITY { get; set; }
        public Nullable<bool> WI_IS_TIMEOUT { get; set; }
        public Nullable<System.DateTime> WI_TIMEOUT_DATE { get; set; }
        public string WI_ADMIN_DATA { get; set; }
        public Nullable<int> WI_RETRY_TIMES { get; set; }
        public Nullable<System.DateTime> WI_NEXT_EXE_DATE { get; set; }
        public Nullable<int> WI_MAX_RETRY_TIMES { get; set; }
        public string WI_INFO1 { get; set; }
        public string WI_INFO2 { get; set; }
        public Nullable<int> WI_WE_ID { get; set; }
        public string WI_NOTE { get; set; }
        public Nullable<int> WI_UNUSED1 { get; set; }
        public Nullable<int> WI_UNUSED2 { get; set; }
        public string WI_UNUSED3 { get; set; }
        public string WI_UNUSED4 { get; set; }
        public string WI_UNUSED5 { get; set; }
        public Nullable<int> WI_PC_FLAG { get; set; }
        public Nullable<int> WI_PARENT_ID { get; set; }
        public Nullable<int> WI_ENABLE_TIMELIMIT { get; set; }
        public Nullable<int> WI_IS_NOTIFY { get; set; }
        public string WI_LAST_PROCESS_ID { get; set; }
        public Nullable<int> WI_RETRIEVABLE { get; set; }
        public Nullable<int> WI_APPROVE { get; set; }
        public string WI_APPROVE_NOTE { get; set; }
        public string WI_PREV_STEP_NAME { get; set; }
        public Nullable<int> WI_INDEX { get; set; }
        public Nullable<int> WI_NEXT_INDEX { get; set; }
        public string FControlUnitID { get; set; }
    }
}
