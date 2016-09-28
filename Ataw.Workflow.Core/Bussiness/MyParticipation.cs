using System;
using System.Linq.Expressions;
using Ataw.Workflow.Core;
using Ataw.Workflow.Core.DataAccess;

namespace Ataw.Workflow.Web.Bussiness
{
    public class MyParticipation : MyWorkflowINST
    {
        protected override Action<BaseTree> ResultForEach
        {
            get
            {
                return (m => m.CurrentProcessUserID = string.IsNullOrEmpty(m.CurrentProcessUserID) ? "" : MRP.BussinessLogic.CommonRight.Ataw_UsersGet(m.CurrentProcessUserID).NickName);
            }
        }
        protected override Expression<Func<WF_WORKFLOW_INST, bool>> InstWhereExpress
        {
            get
            {
                return (m =>
                     m.WI_STEP_TYPE == (int)StepType.Manual
                    && m.WI_REF_LIST != null
                    && m.WI_REF_LIST.Contains(UserId)
                    );
            }
        }

        protected override Expression<Func<WorkflowInstView, BaseTree>> Selector
        {
            get
            {
                return (
                    workFlowInst => new BaseTree()
                    {
                        ID = workFlowInst.WF_WORKFLOW_INST.WI_ID,
                        Name = workFlowInst.WF_WORKFLOW_INST.WI_NAME,
                        CreateTime = workFlowInst.WF_WORKFLOW_INST.WI_CREATE_DATE,
                        CurrentStep = workFlowInst.WF_WORKFLOW_INST.WI_CURRENT_STEP_NAME,
                        EndTime = workFlowInst.WF_WORKFLOW_INST.WI_END_DATE,
                        ReceiveTime = workFlowInst.WF_WORKFLOW_INST.WI_RECEIVE_DATE,
                        _parentId = workFlowInst.WF_WORKFLOW_INST.WI_WD_NAME,
                        LastName = workFlowInst.WF_WORKFLOW_INST.WI_LAST_STEP_NAME,
                        WDName = workFlowInst.WF_WORKFLOW_DEF.WD_NAME,
                        LastManualName = workFlowInst.WF_WORKFLOW_INST.WI_LAST_MANUAL_NAME,
                        CurrentProcessUserID=workFlowInst.WF_WORKFLOW_INST.WI_PROCESS_ID,
                    }
                    );
            }
        }
        //private WorkflowDbContext context = new WorkflowDbContext(PlugAreaRegistration.CONN);
        //public IList<MyWorkTree> QueryData(string name, string begin, string end, string flowType)
        //{
        //    IList<MyWorkTree> myWorkTrees = new List<MyWorkTree>();
        //    MyWorkTree myWorkTree;
        //    var workFlowDefs = context.WF_WORKFLOW_DEF.ToList<WF_WORKFLOW_DEF>();
        //    IQueryable<WF_WORKFLOW_INST> workFlowInsts;
        //    if (workFlowDefs != null)
        //    {
        //        var list = context.WF_WORKFLOW_INST.AsQueryable();
        //        string userid = GlobalVariable.UserId.ToString();
        //        foreach (var workFlowDef in workFlowDefs)
        //        {

        //            list = list.Where(m => m.WI_WD_NAME == workFlowDef.WD_SHORT_NAME);
        //            if (!string.IsNullOrEmpty(name))
        //            {
        //                list = list.Where(m => m.WI_NAME.Contains(name));

        //            }
        //            if (!string.IsNullOrEmpty(begin))
        //            {
        //                DateTime btime = Convert.ToDateTime(begin);
        //                list = list.Where(m => m.WI_CREATE_DATE >= btime);
        //            }
        //            if (!string.IsNullOrEmpty(end))
        //            {
        //                DateTime endtime = Convert.ToDateTime(end).AddDays(1);
        //                list = list.Where(m => m.WI_CREATE_DATE < endtime);

        //            }
        //            if (!string.IsNullOrEmpty(flowType))
        //            {
        //                if (flowType == "1")//我参与的流程
        //                {
        //                    list = list.Where(m => m.WI_REF_LIST != null && m.WI_REF_LIST.Contains(userid) && m.WI_CREATE_USER != userid);
        //                }
        //                else if (flowType == "2")//我发起的流程
        //                {
        //                    list = list.Where(m => m.WI_CREATE_USER == userid);

        //                }

        //            }
        //            workFlowInsts = list;
        //            if (workFlowInsts.Count() > 0)
        //            {
        //                myWorkTree = new MyWorkTree();
        //                myWorkTree.ID = workFlowDef.WD_SHORT_NAME;
        //                myWorkTree.Name = workFlowDef.WD_NAME;
        //                myWorkTree._parentId = "";
        //                myWorkTrees.Add(myWorkTree);
        //                foreach (var workFlowInst in workFlowInsts)
        //                {
        //                    myWorkTree = new MyWorkTree();
        //                    myWorkTree.ID = workFlowInst.WI_ID;
        //                    myWorkTree.Name = workFlowInst.WI_NAME;
        //                    myWorkTree.CreateTime = workFlowInst.WI_CREATE_DATE;
        //                    myWorkTree.CurrentStep = workFlowInst.WI_CURRENT_STEP_NAME;
        //                    myWorkTree.EndTime = workFlowInst.WI_END_DATE;
        //                    myWorkTree._parentId = workFlowDef.WD_SHORT_NAME;
        //                    myWorkTrees.Add(myWorkTree);
        //                    //myWorkTree.children.Add(new MyWorkFlowInstTree
        //                    //{
        //                    //    ID = workFlowInst.WI_ID,
        //                    //    Name = workFlowInst.WI_NAME,
        //                    //    CreateTime = workFlowInst.WI_CREATE_DATE,
        //                    //    CurrentStep = workFlowInst.WI_CURRENT_STEP_NAME,
        //                    //    EndTime = workFlowInst.WI_END_DATE
        //                    //});
        //                }

        //            }
        //        }
        //    }
        //    return myWorkTrees;
        //}

    }
}