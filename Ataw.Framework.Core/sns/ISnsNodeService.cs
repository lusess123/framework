using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Ataw.Framework.Core
{
    public abstract class SnsNodeService
    {
        public string Items
        {
            get;
            set;
        }

        public void Initialization(string items)
        {
            Items = items;
        }

        public  abstract IEnumerable FetchData();
        
   }
    
}
