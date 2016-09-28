using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
     [CodePlug("AllImageShow", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-09-25", Author = "zhengyk", Description = "图片显示控件参数创建插件")]
   public class AtawAllImageShowOptionCreator : AtawBaseOptionCreator
    {
        private AtawAllImageShowOptions fTextOptions;

        public AtawAllImageShowOptionCreator()
        {
            fTextOptions = new AtawAllImageShowOptions();
            BaseOptions = fTextOptions;
        }
    }
}
