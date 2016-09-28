
namespace Ataw.Framework.Core
{
    public class AtawListPageConfigView : AtawPageConfigView
    {
        public string SearchFormName { get; set; }

        public string ListFormName { get; set; }

        public PageSelector PageSelector { get; set; }
        //public bool HasPager { get; set; }
    }
}
