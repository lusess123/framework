using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ataw.Workflow.Core;
using Ataw.Workflow.Core.DataAccess;
using Ataw.Framework.Core;

namespace Ataw.Workflow.Core
{
    [CodePlug(ApproveYesConn.REG_NAME, BaseClass = typeof(IConnection),
     CreateDate = "2012-10-4", Author = "zhengyk", Description = "审批通过连接线")]
    public class ApproveYesConn : IConnection
    {
        public const string REG_NAME = "ApproveYesConn";
        public virtual bool Match(WF_WORKFLOW_INST mainRow, WorkflowContent content, IUnitOfData source)
        {
            //throw new NotImplementedException();
            if (mainRow.WI_CUSTOM_DATA == "0" || mainRow.WI_CUSTOM_DATA == "1")
            {
                return mainRow.WI_CUSTOM_DATA.Value<int>() == 1;
            }
            else
            {
                return mainRow.WI_APPROVE == 1;
            
            }
            
        }
    }
}
