using System;

namespace Ataw.Workflow.Core.DataAccess
{
    public class WF_APPROVE_HISTORY
    {
        public string AH_ID { get; set; }
        public string AH_WORKFLOW_ID { get; set; }
        public string AH_STEP_NAME { get; set; }
        public string AH_STEP_DISPLAY_NAME { get; set; }
        public string AH_OPERATOR { get; set; }
        public Nullable<int> AH_APPROVE { get; set; }
        public string AH_NOTE { get; set; }
        public string AH_CREATE_ID { get; set; }
        public Nullable<System.DateTime> AH_CREATE_DATE { get; set; }
    }
}
