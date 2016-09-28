using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Ataw.Framework.Core
{
     [CodePlug("ImageDetail", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-09-25", Author = "zhengyk", Description = "图片详情控件参数创建插件")]
    public class AtawImageDetailOptionsCreator : AtawBaseUploadOptionCreator
    {
          private AtawImageDetailOptions fTextOptions;

          public AtawImageDetailOptionsCreator()
        {
            fTextOptions = new AtawImageDetailOptions();
            BaseOptions = fTextOptions;
        }

    }
}
