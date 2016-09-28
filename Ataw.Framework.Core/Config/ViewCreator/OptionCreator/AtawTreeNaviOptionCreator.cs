using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("AtawTreeNavi", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-4-16", Author = "wq", Description = "AtawTreeNavi")]
    public class AtawTreeNaviOptionCreator:AtawBaseNaviOptionCreator
    {
        private AtawTreeNaviOptions fAtawTreeNaviOptions;

         public AtawTreeNaviOptionCreator()
        {
            fAtawTreeNaviOptions = new AtawTreeNaviOptions();
            BaseOptions = fAtawTreeNaviOptions;
        }
    }
}
