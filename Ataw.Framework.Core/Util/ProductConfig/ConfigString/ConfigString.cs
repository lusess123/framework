using System.Linq;

namespace Ataw.Framework.Core
{
    public class ConfigString
    {
        /// <summary>
        /// 取控制单元下，指定key的值
        /// </summary>
        /// <param name="FControlUnitID"></param>
        /// <param name="ConfigKey"></param>
        /// <returns></returns>
        public static string GetAtawControlUnitsValue(string fControlUnitID, string configKey)
        {
            var xml = AtawAppContext.Current.ProductsXml.ControlUnits;

            var q = from p in xml where p.FControlUnitID == fControlUnitID select p;
            string res = "";
            foreach (var item in q)
            {
                foreach (var item2 in item.ControlUnitConfigs)
                {
                    if (item2.Key == configKey)
                    {
                        res = item2.Value;
                        return res;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 返回全局指定key的值 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAtawConfigsValue(string key)
        {
            return AtawAppContext.Current.ProductsXml.BaseConfigs
                .Where(i => i.Key == key)
                .Select(item => item.Value)
                .FirstOrDefault();

        }
    }
}
