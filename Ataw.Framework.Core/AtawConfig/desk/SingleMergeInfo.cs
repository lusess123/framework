
using System;
namespace Ataw.Framework.Core
{
    public abstract class SingleMergeInfo
    {
        public string Sign { get; set; }
        public string Title { get; set; }
        public string Count { get; set; }
        public string Data { get; set; }
        public string LinkHref { get; set; }
        public DateTime ShowTime { get; set; }
        public abstract void GetData();

    }
}
