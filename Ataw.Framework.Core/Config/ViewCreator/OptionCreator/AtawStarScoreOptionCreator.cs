using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("StarScore", BaseClass = typeof(AtawOptionCreator),
           CreateDate = "2013-4-20", Author = "wq", Description = "星星评分")]
    class AtawStarScoreOptionCreator : AtawBaseOptionCreator
    {
        private AtawStarScoreOptions fAtawStarScoreOptions;

        public AtawStarScoreOptionCreator()
        {
            fAtawStarScoreOptions = new AtawStarScoreOptions();
            BaseOptions = fAtawStarScoreOptions;
        }
        public override BaseOptions Create()
        {
            base.Create();
			if (this.Config.Score != null)
			{
				fAtawStarScoreOptions.average = this.Config.Score.average; 
				fAtawStarScoreOptions.length = this.Config.Score.length;
				fAtawStarScoreOptions.rateMax = this.Config.Score.rateMax;
				fAtawStarScoreOptions.step = this.Config.Score.step;
			}
			else {
				fAtawStarScoreOptions.average = 5;
				fAtawStarScoreOptions.length = 5;
				fAtawStarScoreOptions.rateMax = 5;
				fAtawStarScoreOptions.step = true;
			}
            return fAtawStarScoreOptions;
        }
    }
}
