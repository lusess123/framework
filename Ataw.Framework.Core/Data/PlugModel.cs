
using System;
namespace Ataw.Framework.Core
{
    public class PlugModel : ObjectData,IRegName
    {
        public string RegName { get; set; }
        public string BassClassName { get; set; }
        public string InstanceClassName { get; set; }
        public string DllPath { get; set; }
        public string Desc { get; set; }

        public string Author { get; set; }
        public string CreateDate { get; set; }
    }
}
