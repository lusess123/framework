
namespace Ataw.Framework.Core
{
    [CodePlug("FormSingleSelector", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-4-11", Author = "sj", Description = "FormSingleSelector控件参数创建插件")]
    public class AtawFormSingleSelectorOptionCreator : AtawSelectorOptionCreator
    {
        protected FormSingleSelectorOptions fFormSingleSelectorOptions;
        public AtawFormSingleSelectorOptionCreator()
        {
            BaseOptions = fSelectorOptions = fFormSingleSelectorOptions = new FormSingleSelectorOptions();
           //BaseOptions = fSelectorOptions;
        }


        public override BaseOptions Create()
        {
            if(Config.Selector != null ){
                string _moduleXMl = Config.Selector.ModuleXml ;
                if (!_moduleXMl.IsEmpty())
                {
                    fFormSingleSelectorOptions.ModuleXml = _moduleXMl;
                }
            }
           

            return base.Create();
        }

      
    }
}
