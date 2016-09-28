
using System.Collections.Generic;
namespace Ataw.Framework.Core
{
    public class UniqueRowArrange : List<UniqueRow>
    {

        public bool CheckInsert(UniqueRow row)
        {
            foreach (UniqueRow r in this)
            {
                if (r.EquelCheck(row))
                {
                    return false;
                }
            }

            this.Add(row);
            return true;
        }
    }
}
