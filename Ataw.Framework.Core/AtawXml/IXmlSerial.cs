using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ataw.Framework.Core
{
    public interface IXmlSerial
    {
        void SaveStringBuilder(StringBuilder sb, Formatting formatting );
        void SaveFile(string file);
        string SaveString(Formatting formatting );
    }
}
