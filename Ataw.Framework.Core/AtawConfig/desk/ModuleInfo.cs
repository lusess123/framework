
using System.Collections.Generic;
namespace Ataw.Framework.Core
{
    /// <summary>
    /// 导航
    /// </summary>
    public class ModuleInfo
    {
        public string FID { get; set; }
        public string PID { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } //1:权限菜单,2:桌面快捷方式
        public string RightValue { get; set; }//$$这些
		public string Icon { get; set; }


        public virtual List<ModuleInfo> GetData()
        {
            return null;
        }
    }
}
