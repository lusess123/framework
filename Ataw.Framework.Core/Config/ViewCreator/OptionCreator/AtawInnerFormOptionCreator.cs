
namespace Ataw.Framework.Core
{
	[CodePlug("InnerForm", BaseClass = typeof(AtawOptionCreator),
		CreateDate = "2013-12-25", Author = "zhengyk", Description = "AtawInnerForm插件")]
	public class AtawInnerFormOptionCreator : AtawBaseOptionCreator
	{
		protected AtawInnerFormOptions fAtawInnerFormOptions;

        public AtawInnerFormOptionCreator()
        {
            fAtawInnerFormOptions = new AtawInnerFormOptions();
            BaseOptions = fAtawInnerFormOptions;
        }

		public override BaseOptions Create()
		{
            fAtawInnerFormOptions.InnerFormName = this.Config.InnerFormName;
			return base.Create();
		}
	}
}
