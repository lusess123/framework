using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("BaseNavi", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-4-16", Author = "wq", Description = "AtawBaseNavi")]
    public class AtawBaseNaviOptionCreator: AtawBaseOptionCreator
    {
        private AtawBaseNaviOptions fAtawBaseNaviOptions;

        public AtawBaseNaviOptionCreator()
        {
            fAtawBaseNaviOptions = new AtawBaseNaviOptions();
            BaseOptions = fAtawBaseNaviOptions;
        }
    }
}
