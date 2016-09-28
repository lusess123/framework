
namespace Ataw.Framework.Core
{
    [CodePlug("PCAS", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "PCAS控件参数创建插件")]
    public class AtawPCASOptionCreator : AtawBaseOptionCreator
    {
        private PCASOptions fTextOptions;

        public AtawPCASOptionCreator()
        {
            fTextOptions = new PCASOptions();
            BaseOptions = fTextOptions;
        }
        public override BaseOptions Create()
        {
            var dv = base.Create();
            if (this.Config.PCAS == null)
            {
                this.Config.PCAS = new PCASConfig();
            }
                ((PCASOptions)dv).PCASRequiredCount = this.Config.PCAS.PCASRequiredCount;
                ((PCASOptions)dv).PCASDisplayItemsCount = this.Config.PCAS.PCASDisplayItemsCount;
            
            return dv;
        }
    }
}
