
namespace Ataw.Framework.Core
{
	[CodePlug("Time", BaseClass = typeof(AtawOptionCreator),
		CreateDate = "2013-8-30", Author = "wq", Description = "Time控件参数创建插件")]
	public class AtawTimeOptionCreator : AtawBaseOptionCreator
    {
		private TimeOptions fTimeOptions;

		public AtawTimeOptionCreator()
        {
			fTimeOptions = new TimeOptions();
			BaseOptions = fTimeOptions;
        }
    }
}
