using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;
using System.Linq;

namespace Ataw.Workflow.Core
{
    //public delegate void DoActionDelegate(string userId, WF_WORKFLOW_INST mainRow, WorkflowContent content, IUnitOfData source);
    public class ManualStepConfig : StepConfig
    {

        public ManualStepConfig()
        {
            ControlActions = new RegNameList<ControlActionConfig>();
            NotifyActions = new List<string>();
        }

        public ManualStepConfig(WorkflowConfig workflowConfig)
            : base(workflowConfig)
        {
        }

        public sealed override StepType StepType
        {
            get
            {
                return StepType.Manual;
            }
        }
        public ConfigChoice ContentChoice { get; set; }
        public bool HaveSave { get; set; }
        public bool HaveBack { get; set; }
        public bool HaveUnlock { get; set; }
        [XmlIgnore]
        private ErrorConfig error;
        [XmlIgnore]
        public ErrorConfig Error
        {
            get
            {
                if (error == null)
                    error = new ErrorConfig();
                return error;
            }
            set
            {
                error = value;
            }
        }
        public RegNameList<ControlActionConfig> ControlActions { get; set; }

        public string ContentXml { get; set; }

        [XmlArrayItem("NotifyAction")]
        public List<string> NotifyActions { get; set; }
        //public string UIProcessRegName
        //{
        //    get;
        //    set;
        //}

        public string ActorRegName
        {
            get;
            set;
        }

        public ProcessConfig Process
        {
            get;
            set;
        }

        public override Step CreateStep(Workflow workflow)
        {
            return new ManualStep(workflow, this);
        }

        public override void Prepare(WF_WORKFLOW_INST workflowRow, IUnitOfData source)
        {
            if (string.IsNullOrEmpty(workflowRow.WI_RECEIVE_LIST))
            {
                SetReceiveUserList(workflowRow, source);
            }
            //-----消息触发
            SendNotifys(workflowRow, source);

        }
        private void SendNotifys(WF_WORKFLOW_INST mainRow, IUnitOfData source)
        {
            try
            {
                INotifyAction notifyAction;
                if (!NotifyActions.IsNotEmpty())
                {
                    NotifyActions.Add("DefaultNotifyAction");
                }
                //if (NotifyActions.IndexOf("NotifyAction") < 0 )
                //{
                //    NotifyActions.Add("NotifyAction");
                //}

                    foreach (var notify in NotifyActions)
                    {
                        notifyAction = notify.CodePlugIn<INotifyAction>();
                        notifyAction.DoPush(QuoteIdList.LoadFromString(mainRow.WI_RECEIVE_LIST), null, mainRow, source);
                        //DoActionDelegate doActionDelegate = new DoActionDelegate(notifyAction.DoAction);
                        foreach (var userID in QuoteIdList.LoadFromString(mainRow.WI_RECEIVE_LIST))
                        {
                            notifyAction.DoAction(userID, mainRow, null, source);
                            //doActionDelegate.BeginInvoke(userID, mainRow, WorkflowInstUtil.CreateContent(mainRow), source, null, null);
                        }
                    }
               
                //----------

            }
            catch (Exception ex)
            {
                string str = "工作流触发异常：" + ex.Message;
                AtawAppContext.Current.Logger.Debug(str);
                AtawTrace.WriteFile(LogType.Error, str);
            }
        }
        private void SetReceiveUserList(WF_WORKFLOW_INST workflowRow, IUnitOfData source)
        {
            try
            {
                QuoteIdList list = new QuoteIdList();
                int count;
                IActor actorPlug = AtawIocContext.Current.FetchInstance<IActor>(ActorRegName);
                IEnumerable<string> actors = actorPlug.GetActors(workflowRow, source);
                list.Add(actors);
                workflowRow.WI_RECEIVE_LIST = list.ToString(out count);
                workflowRow.WI_RECEIVE_COUNT = count;
                if (count == 0)
                {
                    throw new NoActorException(this, Error, new Exception("找不到一个人"));
                }
            }
            catch (Exception ex)
            {
                throw new NoActorException(this, Error, ex);
            }

        }

    }
}

