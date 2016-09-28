using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
	public class AtawInnerPageConfig
	{
		[XmlAttribute]
		public string ModuleXml { get; set; }
		[XmlAttribute]
		public string ForeignKey { get; set; }
	}
}
