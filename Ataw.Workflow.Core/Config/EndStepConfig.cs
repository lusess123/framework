using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;

namespace Ataw.Workflow.Core
{
    public class EndStepConfig : StepConfig
    {
        // private HistoryConfig fHistory;
        //  private ErrorConfig fError;


        public EndStepConfig()
        {
            NotifyActions = new List<string>();
        }

        public EndStepConfig(WorkflowConfig workflowConfig)
            : base(workflowConfig)
        {
        }

        public override StepType StepType
        {
            get
            {
                return StepType.End;
            }
        }

        public override string DisplayName
        {
            get
            {
                return "结束";
            }

        }

        public override bool HasOutStep
        {
            get
            {
                return false;
            }
        }

        [XmlAttribute]
        public string PlugRegName { get; set; }
        [XmlArrayItem("NotifyAction")]
        public List<string> NotifyActions { get; set; }

        [XmlAttribute]
        public bool EnableModify { get; set; }
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
        //[ObjectXmlElement]
        //public ErrorConfig Error
        //{
        //    get
        //    {
        //        if (fError == null)
        //            fError = new ErrorConfig();
        //        return fError;
        //    }
        //    public set
        //    {
        //        fError = value;
        //    }
        //}

        //[ObjectXmlElement]
        //public HistoryConfig History
        //{
        //    get
        //    {
        //        if (fHistory == null)
        //            fHistory = new HistoryConfig();
        //        return fHistory;
        //    }
        //    public set
        //    {
        //        fHistory = value;
        //    }
        //}

        public override Step CreateStep(Workflow workflow)
        {
            return new EndStep(workflow, this);
        }


        public override void Prepare(WF_WORKFLOW_INST workflowRow, IUnitOfData source)
        {
            try
            {
                INotifyAction notifyAction;
                foreach (var notify in NotifyActions)
                {
                    notifyAction = AtawIocContext.Current.FetchInstance<INotifyAction>(notify);
                    notifyAction.DoAction(workflowRow.WI_CREATE_USER, workflowRow, null, source);
                }
            }
            catch (Exception ex)
            {
                string str = "工作流触发异常：" + ex.Message;
                AtawAppContext.Current.Logger.Debug(str);
                AtawTrace.WriteFile(LogType.Error, str);
            }
           
        }
       

    }
}
