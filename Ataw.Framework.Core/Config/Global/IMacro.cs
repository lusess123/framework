﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
   public interface IMacro
    {
        [XmlAttribute]
         bool NeedParse { get; set; }

        string ExeValue();

    }
}
