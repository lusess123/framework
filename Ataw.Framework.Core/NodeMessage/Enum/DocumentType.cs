using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core
{
    [CodePlug("DocumentType", Author = "zgl", Description = "文件类型")]
    public enum DocumentType
    {
        [Description("图片")]
        Inside = 1,
        [Description("文章")]
        Public = 2,
        [Description("文件")]
        Notice = 3
    }
}
