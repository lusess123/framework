using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core
{
     [CodePlug("MaskDown", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2015-07-14", Author = "zyk", Description = "MaskDown编辑器控件参数创建插件")]
    public class MarkDownOptionCreator : AtawBaseOptionCreator
    {
         private MarkDownOptions fMarkDownOptions;
          //---------//---------------
           public MarkDownOptionCreator()
          {
              BaseOptions = fMarkDownOptions = new MarkDownOptions();
          }
    }
}
