using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core
{
    [CodePlug("ArticleType", Author = "zgl", Description = "文章类型")]
    public enum ArticleType
    {
        [Description("原创")]
        Original = 0,
        [Description("转载")]
        Reprint = 1,
        [Description("翻译")]
        Translate = 2
    }
}
