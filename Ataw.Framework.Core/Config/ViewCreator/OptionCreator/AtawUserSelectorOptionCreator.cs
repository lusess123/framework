using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("UserSelector", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-12-3", Author = "sj", Description = "DocumentSelector")]
    public class AtawUserSelectorOptionCreator : AtawDecodeOptionCreator
    {
        private AtawUserSelectorOptions fAtawUserSelectorOptions;

        public AtawUserSelectorOptionCreator()
        {
            fAtawUserSelectorOptions = new AtawUserSelectorOptions();
            BaseOptions = fAtawUserSelectorOptions;
        }
    }
}
