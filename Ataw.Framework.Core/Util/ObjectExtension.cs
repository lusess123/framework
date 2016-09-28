using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Ataw.Framework.Core
{
    public static class ObjectExtension
    {

        public static string  ExcepString(this Exception ex, StringBuilder sb = null){
            if (sb == null)
            {
                sb = new StringBuilder();
            }
            sb.AppendLine();
            sb.AppendLine(ex.Message);
            sb.AppendLine(ex.Source);
            sb.AppendLine(ex.StackTrace);

            if (ex.InnerException != null)
            {
                ex.InnerException.ExcepString(sb);
            }


            return sb.ToString();
        }

        public static string Encode(this string str)
        {
            string htext = "";

            for (int i = 0; i < str.Length; i++)
            {
                htext = htext + (char)(str[i] + 10 - 1 * 2);
            }
            return htext;
        }

        public static string Decode(this string str)
        {
            string dtext = "";

            for (int i = 0; i < str.Length; i++)
            {
                dtext = dtext + (char)(str[i] - 10 + 1 * 2);
            }
            return dtext;
        }


        public static string StringToHex(this string str)
        {
            byte[] ByteFoo = System.Text.Encoding.Unicode.GetBytes(str);
            StringBuilder strbuilder = new StringBuilder();
            foreach (byte b in ByteFoo)
            {
                strbuilder.Append(b.ToString("X")); //X表示十六进制显示
                strbuilder.Append(",");
            }
            return strbuilder.ToString();
        }

        public static string GetLen(this string str, int len)
        {
            if (str.Length > len)
                return str.Substring(0, len);
            return str;
        }

        public static bool FFun(this string regName)
        {
            return AtawAppContext.Current.FetchFun(regName);
        }

        public static T AppKv<T>(this string regName, T def)
        {
            T result = default(T);
            if (def != null)
            {
                result = def;
            }
            var _obj = AtawAppContext.Current.ApplicationXml.AppSettings[regName];
            if (_obj != null)
            {
                try
                {
                    string _exeStr = _obj.ForceExeValue() ?? _obj.Value;
                    return _exeStr.Value<T>(def);
                }
                catch (Exception ex){
                    AtawTrace.WriteFile(LogType.AppKvError,
                        "注册表达式为{0}的弘插件在Key为{1}的配置执行上发生错误{2}{3}".AkFormat(_obj.Value,regName,Environment.NewLine,ex.Message+ ex.StackTrace));
                    result = def; 
                }
            }

            return result;
        }

        public static string AkFormat(this string format, params object[] args)
        {
            return string.Format(ObjectUtil.SysCulture, format, args);
        }
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        public static bool IsAkEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static T XmlConfig<T>(this string fileName) where T : XmlConfigBase, new()
        {
            string path = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH, fileName);
            return XmlUtil.ReadFromFile<T>(path);
        }

        public static T InstanceByPage<T>(this string classRegName, string instanceName)
        {
            var flyweight = AtawAppContext.Current.PageFlyweight;
            return flyweight.GetInstance<T>(classRegName, instanceName);
        }

        public static T SingletonByPage<T>(this string classRegName)
        {
            var flyweight = AtawAppContext.Current.PageFlyweight;
            return flyweight.GetSingleton<T>(classRegName);
        }

        public static T CodePlugIn<T>(this string str)
        {
            return AtawIocContext.Current.FetchInstance<T>(str);
        }

        public static T SafeJsonObject<T>(this string str)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch (Exception ex) {
                AtawTrace.WriteStringFile("JsonConvert",ex.Message);
                return default(T);
            }
        }
        public static string ToJson<T>(this T obj) {
            return AtawAppContext.Current.FastJson.ToJSON(obj);
        }

        public static T PlugGet<T>(this string name)
        {
          //  name = 
            if (name.ToUpper().IndexOf(".XML") > 0)
            {
                name = name.ToUpper();
               
                
                if (name.IndexOf("@") >= 0)
                {
                    name = name.Replace(".XML", "");
                    string[] arr = name.Split('@');
                    string xml = arr[0];
                    AtawAppContext.Current.PageFlyweight.PageItems["XmlString"] = arr[1];
                    name = xml + ".XML";
                }

                string path = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH, name);
                object obj = XmlUtil.ReadFromFile(path, typeof(T));
                return (T)obj;
            }
            else
            {
                return name.CodePlugIn<T>();
            }
        }


        public static T Value<T>(this object objValue)
        {
            if (objValue == null || objValue == DBNull.Value)
                return default(T);
            Type destType = typeof(T);
            if (objValue.GetType() == destType)
                return (T)objValue;
            try
            {
                return (T)System.Convert.ChangeType(objValue, destType);
            }
            catch
            {
                return XmlUtil.GetDefaultValue<T>(objValue.ToString());
            }
        }

        public static T Value<T>(this object objValue, T defaultValue)
        {
            if (objValue == null || objValue == DBNull.Value)
                return defaultValue;
            Type destType = typeof(T);
            if (objValue.GetType() == destType)
                return (T)objValue;
            try
            {
                return (T)System.Convert.ChangeType(objValue, destType);
            }
            catch
            {
                return XmlUtil.GetDefaultValue<T>(objValue.ToString(), defaultValue);
            }
        }

        public static T Convert<T>(this object objValue) where T : class
        {
            AtawDebug.AssertArgumentNull(objValue, "objValue", null);

            T result = objValue as T;
            AtawDebug.AssertNotNull(result, string.Format(ObjectUtil.SysCulture,
                "将类型{0}转换为类型{1}失败，请确认代码", objValue.GetType(), typeof(T)), objValue);
            return result;
        }

        public static bool IsNotEmpty<T>(this T list) where T : ICollection
        {
            return list != null && list.Count > 0;
        }

        public static string GetDescription(this object obj)
        {
            return ObjectUtil.GetDescription(obj);
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static T ToEnum<T>(this string value, bool ignoreCase)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static void ObjectClone<T>(this T source, T t)
        {
            foreach (var pS in source.GetType().GetProperties())
            {
                foreach (var pT in t.GetType().GetProperties())
                {
                    if (pT.Name != pS.Name) continue;
                    (pT.GetSetMethod()).Invoke(t, new object[] { pS.GetGetMethod().Invoke(source, null) });
                }
            };
        }
    }
}
