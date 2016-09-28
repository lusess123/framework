using Ataw.Framework.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.FrameworkExtend.Core
{
    public abstract  class BaseSingleMethod : IMethod
    {
        public void After(int resInt, IDictionary<string, object> resultObj)
        {
            //throw new NotImplementedException();
        }

        public void Before()
        {
           // throw new NotImplementedException();
        }

        public abstract void Exe();
        

        public int Order
        {
            get;
            set;
        }

        public NameValueCollection Params
        {
            get;
            set;
        }

        public object ResultObj
        {
            get;
            set;
        }
    }
}
