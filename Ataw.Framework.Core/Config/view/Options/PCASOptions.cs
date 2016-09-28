using System.Dynamic;
using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    public class PCASOptions : BaseTextOptions
    {
        /// <summary>
        /// PCAS控件的必填项个数
        /// </summary>
        public int PCASRequiredCount { get; set; }

        /// <summary>
        /// PCAS控件显示筛选项的个数
        /// </summary>
        public int PCASDisplayItemsCount { get; set; }
    }
}
