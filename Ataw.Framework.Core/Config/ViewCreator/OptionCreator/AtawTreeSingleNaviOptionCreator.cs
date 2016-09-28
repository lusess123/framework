using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("TreeSingleNavi", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-4-16", Author = "wq", Description = "AtawTreeSingleNavi")]
    public class AtawTreeSingleNaviOptionCreator : AtawTreeNaviOptionCreator
    {
        private AtawTreeSingleNaviOptions fAtawTreeSingleNaviOptions;

        public AtawTreeSingleNaviOptionCreator()
        {
            fAtawTreeSingleNaviOptions = new AtawTreeSingleNaviOptions();
            BaseOptions = fAtawTreeSingleNaviOptions;
        }
    }
}
