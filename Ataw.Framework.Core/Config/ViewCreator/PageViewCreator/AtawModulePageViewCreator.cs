using System.Linq;

namespace Ataw.Framework.Core
{
    [CodePlug("ModulePageView", BaseClass = typeof(AtawBasePageViewCreator),
       CreateDate = "2012-11-19", Author = "sj", Description = "ModulePageView创建插件")]
    public class AtawModulePageViewCreator : AtawBasePageViewCreator
    {

        public AtawModulePageViewCreator()
        {
            AtawAppContext.Current.PageFlyweight.PageItems.Add("ATAW_IS_MODULEPAGE",true);
            BasePageView = new AtawPageConfigView();
            PageStyle = PageStyle.None;
        }

        //protected override void CreateColumns(AtawFormConfigView formView, DataFormConfig dataformConfig, FormConfig formConfig)
        //{
        //    PageStyle = formConfig.Action == PageStyle.None ? PageStyle.List : formConfig.Action;
        //    base.CreateColumns(formView, dataformConfig, formConfig);
        //}
    }
}
