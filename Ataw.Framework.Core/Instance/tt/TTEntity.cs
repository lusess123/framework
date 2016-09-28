
using System;
namespace Ataw.Framework.Core
{
    public class TTableEntity
    {

        public string FID { get; set; }
        public string PID { get; set; }
        public bool ISLAYER { get; set; }
        public string CREATE_ID { get; set; }
        public string UPDATE_ID { get; set; }
        public DateTime CREATE_TIME { get; set; }

        public DateTime UPDATE_TIME { get; set; }
        public bool ISDELETE { get; set; }
        public string FControlUnitID { get; set; }
        public string TT_NAME { get; set; }

    }
}
