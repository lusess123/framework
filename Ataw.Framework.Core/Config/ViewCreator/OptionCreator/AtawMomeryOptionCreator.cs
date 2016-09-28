using Ataw.Framework.Core.Config.view.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core.Config.ViewCreator.OptionCreator
{
      [CodePlug("Momery", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2015-06-10", Author = "zyk", Description = "Momery控件参数创建插件")]
    public class AtawMomeryOptionCreator : AtawBaseOptionCreator
    {
          private MomeryOptions fMomeryOptions;
          //---------//---------------
          public AtawMomeryOptionCreator()
          {
              BaseOptions =  fMomeryOptions = new MomeryOptions();
          }
    }
}
