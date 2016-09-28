using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("DocumentSelector", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-12-3", Author = "ace", Description = "DocumentSelector")]
    public class AtawDocumentSelectorOptionCreator : AtawBaseOptionCreator
    {
        private AtawDocumentSelectorOptions fAtawDocumentSelectorOptions;

        public AtawDocumentSelectorOptionCreator()
        {
            fAtawDocumentSelectorOptions = new AtawDocumentSelectorOptions();
            BaseOptions = fAtawDocumentSelectorOptions;
        }
    }
}
