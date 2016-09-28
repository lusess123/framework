using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
  public  abstract class BaseMacro:IMacro
    {
        [XmlAttribute]
      public bool NeedParse
      {
          get;
          set;
      }

        private string TempExpress
        {
            get;
            set;
        }

      protected string Exe(string val)
      {
          if (NeedParse)
          {
              TempExpress = val;
              return MacroExpression.Execute(val);
          }
          return val;
      }

      public string ForceExeValue()
      {
          if (NeedParse)
          {
              if (TempExpress.IsEmpty())
              {
                  ExeValue();
              }
              //TempExpress = val;
              return MacroExpression.Execute(TempExpress);
          }
          return TempExpress;
      }

      public abstract string ExeValue();
        
    }
}
