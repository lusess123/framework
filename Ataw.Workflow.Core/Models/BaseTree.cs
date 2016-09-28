using System;
using Ataw.Workflow.Core;

namespace Ataw.Workflow.Web
{
    public class BaseTree
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string _parentId { get; set; }
        public string LastName { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public string WDName { get; set; }
        public string LastManualName { get; set; }

        public string CurrentStep { get; set; }
        public string CurrentProcessUserID { get; set; }
        public FinishType EndStatus { get; set; }
        public string EndUser { get; set; }

    }
}