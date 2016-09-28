using System.Collections.Generic;
using System.Linq;
namespace Ataw.Framework.Core
{
    public class DataSetSource : Dictionary<string, IListDataTable>
    {
        public void AddDataTable(IListDataTable table)
        {
            this.Add(table.RegName, table);
        }

        public void Aggregate(DataSetSource dest)
        {
            foreach (var d in dest)
            {
                if (this.ContainsKey(d.Key))
                {
                    this[d.Key].List.ToList().AddRange(d.Value.List);
                }
                else
                {
                    this.Add(d.Key, d.Value);
                }
            }
        }
    }
}
