using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
   public class AppLink
    {
       public AppLink()
       {
           Children = new List<AppLink>();
       }

	   public string Key { get; set; }
	   public string DisplayName { get; set; }
	   public string Prompt { get; set; }
	   public string ValPrompt { get; set; }
       public string Href { get; set; }
       public string Icon { get; set; }
        [XmlElement("AppLink")]
       public List<AppLink> Children { get; set; }
    }
}
