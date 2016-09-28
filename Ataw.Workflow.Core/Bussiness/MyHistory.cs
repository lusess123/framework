using System;
using System.Linq.Expressions;
using Ataw.Workflow.Core;
using Ataw.Workflow.Core.DataAccess;

namespace Ataw.Workflow.Web.Bussiness
{
    public class MyHistory : MyWorkflowHisINST
    {
        protected override Action<BaseTree> ResultForEach
        {
            get
            {
                return (m => m.EndUser = string.IsNullOrEmpty(m.EndUser) ? "" : MRP.BussinessLogic.CommonRight.Ataw_UsersGet(m.EndUser).NickName);
            }
        }

        protected override Expression<Func<WorkflowInstHisView, BaseTree>> Selector
        {
            get
            {
                return (
                    workFlowInst => new BaseTree()
                    {
                        ID = workFlowInst.WF_WORKFLOW_INST_HIS.WI_ID,
                        Name = workFlowInst.WF_WORKFLOW_INST_HIS.WI_NAME,
                        CreateTime = workFlowInst.WF_WORKFLOW_INST_HIS.WI_CREATE_DATE,
                        EndStatus = (FinishType)workFlowInst.WF_WORKFLOW_INST_HIS.WI_END_STATE,
                        EndUser = workFlowInst.WF_WORKFLOW_INST_HIS.WI_END_USER,
                        EndTime = workFlowInst.WF_WORKFLOW_INST_HIS.WI_END_DATE,
                        WDName = workFlowInst.WF_WORKFLOW_DEF.WD_NAME,
                        _parentId = workFlowInst.WF_WORKFLOW_DEF.WD_SHORT_NAME
                    }
                    );
            }
        }

        protected override Expression<Func<WF_WORKFLOW_INST_HIS, bool>> InstWhereExpress
        {
            get
            {

                return (m => true);
            }

        }
        //private WorkflowDbContext context = new WorkflowDbContext(PlugAreaRegistration.CONN);
        //public IList<MyHistoryTree> QueryData(string name, string begin, string end, string flowType)
        //{
        //    IList<MyHistoryTree> myHistoryTrees = new List<MyHistoryTree>();
        //    MyHistoryTree myHistoryTree;
        //    var workFlowDefs = context.WF_WORKFLOW_DEF.ToList<WF_WORKFLOW_DEF>();
        //    IList<WF_WORKFLOW_INST_HIS> workFlowInsts;
        //    if (workFlowDefs != null)
        //    {
        //        var list = context.WF_WORKFLOW_INST_HIS.AsQueryable();
        //    //    Expression<Func<WF_WORKFLOW_INST_HIS, bool>> where;
        //        foreach (var workFlowDef in workFlowDefs)
        //        {

        //            //where = LinqUtil.True<WF_WORKFLOW_INST_HIS>();
        //           // where = where.And<WF_WORKFLOW_INST_HIS>(m => m.WI_REF_LIST != null && m.WI_REF_LIST.Contains(GlobalVariable.UserId.ToString()));
        //           // where = where.And<WF_WORKFLOW_INST_HIS>(m => m.WI_WD_NAME == workFlowDef.WD_SHORT_NAME);
        //           // where = where.And<WF_WORKFLOW_INST_HIS>(m => m.WI_CREATE_USER == GlobalVariable.UserId.ToString());  //只取当前的登录用户创建的
        //            string userid = GlobalVariable.UserId.ToString();
        //            list = list.Where(m => m.WI_REF_LIST != null && m.WI_REF_LIST.Contains(userid)&&m.WI_WD_NAME == workFlowDef.WD_SHORT_NAME);

        //            if (!string.IsNullOrEmpty(name))
        //            {
        //                list = list.Where(m => m.WI_NAME.Contains(name));
        //            }
        //            if (!string.IsNullOrEmpty(begin))
        //            {
        //                DateTime btime= Convert.ToDateTime(begin);
        //              list=list.Where(m => m.WI_CREATE_DATE >=btime);
        //            }
        //            if (!string.IsNullOrEmpty(end))
        //            {
        //               DateTime endtime=  Convert.ToDateTime(end).AddDays(1);
        //                list   =list.Where(m => m.WI_CREATE_DATE <endtime );
        //            }
        //            if (!string.IsNullOrEmpty(flowType))
        //            {
        //                if (flowType == "1")//我参与的历史流程
        //                {
        //                    list = list.Where(m => m.WI_REF_LIST.Contains(userid));
        //                }
        //                else if (flowType == "2")//我发起的历史流程
        //                {
        //                    list = list.Where(m => m.WI_CREATE_USER == userid);
        //                }
        //            }

        //            workFlowInsts =list.ToList();
        //            if (workFlowInsts.Count() > 0)
        //            {
        //                myHistoryTree = new MyHistoryTree();
        //                myHistoryTree.ID = workFlowDef.WD_SHORT_NAME;
        //                myHistoryTree.Name = workFlowDef.WD_NAME;
        //                myHistoryTree._parentId = "";
        //                myHistoryTrees.Add(myHistoryTree);
        //                foreach (var workFlowInst in workFlowInsts)
        //                {
        //                    myHistoryTree = new MyHistoryTree();
        //                    myHistoryTree.ID = workFlowInst.WI_ID;
        //                    myHistoryTree.Name = workFlowInst.WI_NAME;
        //                    myHistoryTree.CreateTime = workFlowInst.WI_CREATE_DATE;
        //                    myHistoryTree.EndStatus = (FinishType)workFlowInst.WI_END_STATE;
        //                    myHistoryTree.EndUser = string.IsNullOrEmpty(workFlowInst.WI_END_USER) ? "" :  MRP.BussinessLogic .CommonRight .Ataw_UsersGet  (workFlowInst.WI_END_USER).NickName;
        //                //    myHistoryTree.EndUser = string.IsNullOrEmpty(myHistoryTree.EndUser) ? "" : Ataw_Users.Get(myHistoryTree.EndUser).NickName;
        //                    myHistoryTree.EndTime = workFlowInst.WI_END_DATE;
        //                    myHistoryTree._parentId = workFlowDef.WD_SHORT_NAME;
        //                    myHistoryTrees.Add(myHistoryTree);
        //                }
        //            }
        //        }
        //    }
        //    return myHistoryTrees;
        //}
    }
}