
using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    public class AtawColumnGroupConfigView
    {
        public int ShowType { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public List<AtawColumnConfigView> Columns { get; set; }

        public bool IsHidden { get; set; }


    }
}
