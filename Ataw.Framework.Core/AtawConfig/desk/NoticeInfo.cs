using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    /// <summary>
    /// 公告
    /// </summary>
    public class NoticeInfo
    {
        public string NoticeID { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
        public bool ReadFlag { get; set; }

    }
}
