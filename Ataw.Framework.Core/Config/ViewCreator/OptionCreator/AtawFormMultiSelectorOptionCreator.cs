
namespace Ataw.Framework.Core
{
    [CodePlug("FormMultiSelector", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2013-4-11", Author = "sj", Description = "FormMultiSelector控件参数创建插件")]
    public class AtawFormMultiSelectorOptionCreator : AtawFormSingleSelectorOptionCreator
    {
        protected FormMultiSelectorOptions fFormMultiSelectorOptions;
         public AtawFormMultiSelectorOptionCreator()
        {
            BaseOptions = fSelectorOptions = fFormSingleSelectorOptions = fFormMultiSelectorOptions = new FormMultiSelectorOptions();
           //BaseOptions = fSelectorOptions;
        }
        

      
    }
}
