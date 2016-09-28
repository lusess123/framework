using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Workflow.Core
{
    public class ErrorConfig
    {
        private const int DEFAULT_TIMES = 10;
        private const string DEFAULT_INTERVAL = "1.00:00:00";

        public ErrorConfig()
        {
            ProcessType = ErrorProcessType.Retry;
            RetryTimes = DEFAULT_TIMES;
            Interval = TimeSpan.Parse(DEFAULT_INTERVAL);
        }
        [XmlAttribute()]
        public ErrorProcessType ProcessType { get; set; }

        [XmlAttribute()]
        public int RetryTimes { get; protected set; }

        [XmlAttribute()]
        public TimeSpan Interval { get; protected set; }
    }
}
