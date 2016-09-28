using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("AtawGPSMapInputOpenIframe", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-4-19", Author = "wq", Description = "AtawGPSMapInputOpenIframe")]
    class AtawGPSMapInputOpenIframeOptionCreator : AtawBaseOptionCreator
    {
         private AtawGPSMapInputOpenIframeOptions fAtawGPSMapInputOpenIframeOptions;

         public AtawGPSMapInputOpenIframeOptionCreator()
        {
            fAtawGPSMapInputOpenIframeOptions = new AtawGPSMapInputOpenIframeOptions();
            BaseOptions = fAtawGPSMapInputOpenIframeOptions;
        }
    }
}
