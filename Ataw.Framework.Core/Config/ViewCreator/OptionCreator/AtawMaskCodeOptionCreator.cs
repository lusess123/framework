using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core
{
     [CodePlug("MaskCode", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2015-01-08", Author = "zyk", Description = "MaskCode控件参数创建插件")]
    public class AtawMaskCodeOptionCreator : AtawBaseOptionCreator
    {
         private AtawMaskCodeOptions fAtawMaskCodeOptions;
         public AtawMaskCodeOptionCreator()
        {
            fAtawMaskCodeOptions = new AtawMaskCodeOptions();
            BaseOptions = fAtawMaskCodeOptions;
        }
    }
}
