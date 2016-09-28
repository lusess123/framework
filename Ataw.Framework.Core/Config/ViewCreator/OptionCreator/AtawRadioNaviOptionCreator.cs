using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("RadioNavi", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-4-16", Author = "wq", Description = "AtawRadioNavi")]
    public class AtawRadioNaviOptionCreator : AtawBasesSelectorNaviOptionCreator
    {
        private AtawRadioNaviControlOptions fAtawRadioNaviOptionCreators;

        public AtawRadioNaviOptionCreator()
        {
            fAtawRadioNaviOptionCreators = new AtawRadioNaviControlOptions();
            BaseOptions = fAtawRadioNaviOptionCreators;
        }
    }
}
