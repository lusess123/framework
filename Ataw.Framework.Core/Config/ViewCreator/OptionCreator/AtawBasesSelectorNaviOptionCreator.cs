using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("BasesSelectorNavi", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-4-16", Author = "wq", Description = "AtawCheckBoxNavi和AtawRadioNavi基类")]
    public class AtawBasesSelectorNaviOptionCreator : AtawBaseNaviOptionCreator
    {
        private AtawBasesSelectorNaviOptions fAtawBasesSelectorNaviOptions;

        public AtawBasesSelectorNaviOptionCreator()
        {
            fAtawBasesSelectorNaviOptions = new AtawBasesSelectorNaviOptions();
            BaseOptions = fAtawBasesSelectorNaviOptions;
        }
    }
}
