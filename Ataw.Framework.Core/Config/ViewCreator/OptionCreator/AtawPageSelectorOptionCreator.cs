using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("PageSelector", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-12-3", Author = "sj", Description = "PageSelector")]
    public class AtawPageSelectorOptionCreator : AtawDecodeOptionCreator
    {
        private AtawPageSelectorOptions fAtawPageSelectorOptions;

        public AtawPageSelectorOptionCreator()
        {
            fAtawPageSelectorOptions = new AtawPageSelectorOptions();
            BaseOptions = fAtawPageSelectorOptions;
        }
    }
}
