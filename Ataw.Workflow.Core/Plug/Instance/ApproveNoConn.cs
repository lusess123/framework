using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ataw.Workflow.Core;
using Ataw.Workflow.Core.DataAccess;
using Ataw.Framework.Core;

namespace Ataw.Workflow.Core
{
    [CodePlug(ApproveNoConn.REG_NAME, BaseClass = typeof(IConnection),
        CreateDate="2012-10-4", Author="zhengyk",Description="审批不通过连接线")]
    public class ApproveNoConn : ApproveYesConn
    {
        public new const string REG_NAME = "ApproveNoConn";
        public override bool Match(WF_WORKFLOW_INST mainRow, WorkflowContent content, IUnitOfData source)
        {
            return !base.Match(mainRow, content, source);
        }
    }
}
