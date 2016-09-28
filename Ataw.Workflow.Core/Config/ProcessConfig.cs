using System.Collections.Generic;
using System.Data;
using Ataw.Framework.Core;

namespace Ataw.Workflow.Core
{
    public sealed class ProcessConfig
    {
        //private readonly List<NonUIOperationConfig> fNonUIOperations;

        public ProcessConfig()
        {
            NonUIOperations = new RegNameList<NonUIOperationConfig>();
        }

        public string ManualPageXml { get; set; }

        public UIOperationConfig UIOperation { get; set; }


        public RegNameList<NonUIOperationConfig> NonUIOperations
        {
            get;
            set;
        }

        internal void AddNonUIOperationConfig(NonUIOperationConfig config)
        {
            NonUIOperations.Add(config);
        }


    }
}
