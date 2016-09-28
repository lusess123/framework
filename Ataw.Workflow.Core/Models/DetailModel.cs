using System.Collections.Generic;
using Ataw.Workflow.Core;
using System;

namespace Ataw.Workflow.Web
{
    public class DetailModel
    {
        public string Title
        {
            get;
            set;
        }
        public string WorkflowInstId
        {
            get;
            set;
        }

        public string WorkflowContent { get; set; }

        public List<ControlActionConfig> TabControlActions { get; set; }
        public List<ControlActionConfig> TileControlActions { get; set; }
        public MapModel MapModel { get; set; }
        //附加信息(上一人工步骤的开接收和处理时间)
        public DateTime? ReceiveTime { get; set; }
        public DateTime? ProcessTime { get; set; }
    }
}