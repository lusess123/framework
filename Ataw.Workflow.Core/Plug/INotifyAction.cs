using System.Collections.Generic;
using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;
namespace Ataw.Workflow.Core
{
    public interface INotifyAction
    {
        void DoAction(string userId, WF_WORKFLOW_INST mainRow, WorkflowContent content, IUnitOfData source);
        //消息推送
        void DoPush(IEnumerable<string> userIds, NodeRequest nodeRequest, WF_WORKFLOW_INST mainRow, IUnitOfData source);

        void ManualCompletePush(string manualUserId, NodeRequest nodeRequest, WF_WORKFLOW_INST mainRow, IUnitOfData source);
    }
}
