using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("NaviFilter", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2014-11-1", Author = "zgl", Description = "NaviFilter导航")]
    public class AtawNaviFilterOptionCreator : AtawBaseNaviOptionCreator
    {
        private AtawNaviFilterOptions fAtawNaviFilterOptions;

        public AtawNaviFilterOptionCreator()
        {
            fAtawNaviFilterOptions = new AtawNaviFilterOptions();
            BaseOptions = fAtawNaviFilterOptions;
        }
    }
}
