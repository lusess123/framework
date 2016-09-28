using System.Linq;

namespace Ataw.Framework.Core
{
    [CodePlug("InsertPageView", BaseClass = typeof(AtawBasePageViewCreator),
       CreateDate = "2012-11-23", Author = "sj", Description = "InsertPageView创建插件")]
    public class AtawInsertPageViewCreator : AtawBasePageViewCreator
    {
        private AtawInsertPageConfigView fInsertPageConfigView;

        public AtawInsertPageViewCreator()
        {
            fInsertPageConfigView = new AtawInsertPageConfigView();
            BasePageView = fInsertPageConfigView;
            PageStyle = PageStyle.Insert;
        }

        public override void SetReturnUrl()
        {
            base.SetReturnUrl();
            if (!ModuleConfig.InsertReturnUrl.IsEmpty())
            {
                BasePageView.ReturnUrl = ModuleConfig.InsertReturnUrl;
            }
        }
    }
}
