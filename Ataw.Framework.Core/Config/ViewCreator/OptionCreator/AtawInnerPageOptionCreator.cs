
namespace Ataw.Framework.Core
{
	[CodePlug("InnerPage", BaseClass = typeof(AtawOptionCreator),
		CreateDate = "2013-10-23", Author = "wq", Description = "AtawInnerPage插件")]
	public class AtawInnerPageOptionCreator : AtawBaseOptionCreator
	{
		protected AtawInnerPageOptions fAtawInnerPageOptions;

        public AtawInnerPageOptionCreator()
        {
			fAtawInnerPageOptions = new AtawInnerPageOptions();
			BaseOptions = fAtawInnerPageOptions;
        }

		public override BaseOptions Create()
		{
			fAtawInnerPageOptions.ModuleXml = this.Config.AtawInnerPage.ModuleXml;
			fAtawInnerPageOptions.ForeignKey = this.Config.AtawInnerPage.ForeignKey;
			return base.Create();
		}
	}
}
