using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    public class AtawSeaFormConfigView : AtawBaseFormConfigView
    {
        public SeaInfo SeaInformation { get; set; }
      
    }

    public class SeaInfo
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
