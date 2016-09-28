using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("TreeMultiNavi", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-4-16", Author = "wq", Description = "AtawTreeMultiNavi")]
    public class AtawTreeMultiNaviOptionCreator : AtawTreeNaviOptionCreator
    {
        private AtawTreeMultiNaviOptions fAtawTreeMultiNaviOptions;

        public AtawTreeMultiNaviOptionCreator()
        {
            fAtawTreeMultiNaviOptions = new AtawTreeMultiNaviOptions();
            BaseOptions = fAtawTreeMultiNaviOptions;
        }
    }
}
