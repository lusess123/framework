using System;

namespace Ataw.Workflow.Core.DataAccess
{
    public class WF_STEP_INST_HIS
    {
        public string SI_ID { get; set; }
        public string SI_WI_ID { get; set; }
        public Nullable<int> SI_STATUS { get; set; }
        public string SI_LAST_STEP { get; set; }
        public string SI_LAST_STEP_NAME { get; set; }
        public string SI_LAST_MANUAL { get; set; }
        public string SI_LAST_MANUAL_NAME { get; set; }
        public string SI_CURRENT_STEP { get; set; }
        public string SI_CURRENT_STEP_NAME { get; set; }
        public Nullable<int> SI_STEP_TYPE { get; set; }
        public Nullable<int> SI_PRIORITY { get; set; }
        public string SI_RECEIVE_ID { get; set; }
        public Nullable<System.DateTime> SI_RECEIVE_DATE { get; set; }
        public string SI_SEND_ID { get; set; }
        public Nullable<System.DateTime> SI_SEND_DATE { get; set; }
        public string SI_PROCESS_ID { get; set; }
     
        
        public Nullable<System.DateTime> SI_PROCESS_DATE { get; set; }
        public Nullable<System.DateTime> SI_START_DATE { get; set; }
        public Nullable<System.DateTime> SI_END_DATE { get; set; }
        public Nullable<double> SI_TIME_SPAN { get; set; }
        public Nullable<bool> SI_IS_TIMEOUT { get; set; }
        public Nullable<System.DateTime> SI_TIMEOUT_DATE { get; set; }
        public string SI_NOTE { get; set; }
        public Nullable<int> SI_UNUSED1 { get; set; }
        public Nullable<System.DateTime> SI_UNUSED2 { get; set; }
        public Nullable<System.DateTime> SI_UNUSED3 { get; set; }
        public string SI_UNUSED4 { get; set; }
        public string SI_UNUSED5 { get; set; }
        public Nullable<int> SI_FLOW_TYPE { get; set; }
        public Nullable<int> SI_VALID_FLAG { get; set; }
        public Nullable<int> SI_INDEX { get; set; }
    }
}
