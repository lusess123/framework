
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ataw.Framework.Core.CMS
{
    public class BaseControl
    {
        public string RegName { get; set; }
        [XmlIgnore]
        public IEnumerable<CodeDataModel> CodeDataModels { get; set; }
    }
}
