using System;
using System.IO;
using System.Xml.Serialization;
namespace Ataw.Framework.Core
{
    [Serializable]
	public class ThemeListConfig : FileXmlConfigBase
	{
		public ThemeListConfig()
        {
			ThemeLists = new RegNameList<ThemeListConfigItem>();
        }

		[XmlArrayItem("ThemeList")]
		public RegNameList<ThemeListConfigItem> ThemeLists { get; set; }

		public static ThemeListConfig ReadConfig(string binPath)
		{
			string fpath = Path.Combine(binPath, AtawApplicationConfig.REAL_PATH, "ThemeList.xml");

			return XmlUtil.ReadFromFile<ThemeListConfig>(fpath);
		}
	}
}
