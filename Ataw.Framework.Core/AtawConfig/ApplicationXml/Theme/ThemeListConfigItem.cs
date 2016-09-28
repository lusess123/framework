using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
	[XmlRoot("ThemeList")]
	public class ThemeListConfigItem : IRegName
	{
		[XmlAttribute]
		public string Name { get; set; }
		[XmlAttribute]
		public string Title { get; set; }
		[XmlAttribute]
		public string Icon { get; set; }
		[XmlIgnore]
		public string RegName
		{
			get { return Name; }
		}
	}
}
