using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
   public abstract class BaseFunFetch:IFunFetch
    {
        public  virtual void Init(string regName, string rightKey, string param1, string param2, string param3)
        {
           // throw new NotImplementedException();
            RegName = regName;
            RightKey = rightKey;
            Param1 = param1;
            Param2 = param2;
            Param3 = param3;
        }

        public virtual string RegName
        {
            get;
            set;
        }

        public virtual string RightKey
        {
            get;
            set;
        }

        public virtual string Param1
        {
            get;
            set;
        }

        public virtual string Param2
        {
            get;
            set;
        }

        public virtual string Param3
        {
            get;
            set;
        }

        public abstract int Fetch();
        
    }
}
