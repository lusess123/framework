using Ataw.Framework.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.FrameworkExtend.Core
{
    public class BaseBusMethod : IMethod
    {
        

        public virtual void Before()
        {
           
        }

        public virtual void Exe()
        {
           
        }

        public virtual int Order
        {
            get;
            set;
        }

        public virtual NameValueCollection Params
        {
            get;
            set;
        }

        public virtual object ResultObj
        {
            get;
            set;
        }

        public void After(int resInt, IDictionary<string, object> resultObj)
        {
            //throw new NotImplementedException();
        }
    }
}
