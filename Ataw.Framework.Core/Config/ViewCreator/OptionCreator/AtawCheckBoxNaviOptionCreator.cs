using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("CheckBoxNavi", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-4-16", Author = "wq", Description = "AtawCheckBoxNavi")]
    public class AtawCheckBoxNaviOptionCreator : AtawBasesSelectorNaviOptionCreator
    {
        private AtawCheckBoxNaviControlOptions fAtawCheckBoxNaviOptionCreators;

         public AtawCheckBoxNaviOptionCreator()
        {
            fAtawCheckBoxNaviOptionCreators = new AtawCheckBoxNaviControlOptions();
            BaseOptions = fAtawCheckBoxNaviOptionCreators;
        }
    }
}
