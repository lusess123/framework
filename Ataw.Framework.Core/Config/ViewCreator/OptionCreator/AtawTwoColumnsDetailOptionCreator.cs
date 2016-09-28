using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core
{
    [CodePlug("TwoColumnsDetail", BaseClass = typeof(AtawOptionCreator),
       CreateDate = "2015-04-16", Author = "sj", Description = "2列控件参数创建插件")]
    public class AtawTwoColumnsDetailOptionCreator : AtawBaseOptionCreator
    {
        private AtawTwoColumnsDetailOptions fAtawTwoColumnsOptions;
        public AtawTwoColumnsDetailOptionCreator()
        {
            fAtawTwoColumnsOptions = new AtawTwoColumnsDetailOptions();
            BaseOptions = fAtawTwoColumnsOptions;
        }
    }
}
