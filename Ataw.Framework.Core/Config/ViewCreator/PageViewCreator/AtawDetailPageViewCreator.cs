using System.Linq;

namespace Ataw.Framework.Core
{
    [CodePlug("DetailPageView", BaseClass = typeof(AtawBasePageViewCreator),
       CreateDate = "2012-11-23", Author = "sj", Description = "DetailPageView创建插件")]
    public class AtawDetailPageViewCreator : AtawBasePageViewCreator
    {
        private AtawDetailPageConfigView fDetailPageConfigView;

        public AtawDetailPageViewCreator()
        {
            fDetailPageConfigView = new AtawDetailPageConfigView();
            BasePageView = fDetailPageConfigView;
            PageStyle = PageStyle.Detail;
        }
    }
}
