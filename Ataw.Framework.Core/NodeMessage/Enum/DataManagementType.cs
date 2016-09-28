using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core
{
    [CodePlug("DataManagementType", Author = "zgl", Description = "资源类型")]
    public enum DataManagementType
    {
        [Description("文章")]
        Articles = 1,
        [Description("图片")]
        Pictures = 2,
        [Description("文件")]
        Documents = 3
    }
}
