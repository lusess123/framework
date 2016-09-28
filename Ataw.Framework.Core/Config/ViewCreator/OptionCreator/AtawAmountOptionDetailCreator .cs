using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core
{
    [CodePlug("AmountDetail", BaseClass = typeof(AtawOptionCreator),
       CreateDate = "2015-04-16", Author = "sj", Description = "金额详情控件参数创建插件")]
    public class AtawAmountDetailOptionCreator : AtawBaseOptionCreator
    {
        private AtawAmountDetailOptions fAtawAmountOptions;
        public AtawAmountDetailOptionCreator()
        {
            fAtawAmountOptions = new AtawAmountDetailOptions();
            BaseOptions = fAtawAmountOptions;
        }

     
    }
}
