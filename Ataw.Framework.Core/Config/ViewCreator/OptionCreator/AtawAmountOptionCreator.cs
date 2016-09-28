using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core
{
    [CodePlug("Amount", BaseClass = typeof(AtawOptionCreator),
       CreateDate = "2015-04-16", Author = "sj", Description = "金额控件参数创建插件")]
    public class AtawAmountOptionCreator : AtawBaseOptionCreator
    {
        private AtawAmountOptions fAtawAmountOptions;
        public AtawAmountOptionCreator()
        {
            fAtawAmountOptions = new AtawAmountOptions();
            BaseOptions = fAtawAmountOptions;
        }

        public override BaseOptions Create()
        {
            if (this.Config.Amount != null)
            {
                fAtawAmountOptions.Unit = this.Config.Amount.Unit;
            }
            return base.Create();
        }
    }
}
