using System.Xml.Serialization;
using System.Collections.Generic;
namespace Ataw.Framework.Core
{
    public class CustomScriptSet
    {

        public IList<string> CommonScripts { get; set; }

        public IDictionary<string, string> SeaScripts { get; set; }
    }
}
