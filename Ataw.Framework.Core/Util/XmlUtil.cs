using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public static class XmlUtil
    {

        internal const BindingFlags BIND_ATTRIBUTE =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public static string ReadVersion(XmlReader reader)
        {
            AtawDebug.AssertArgumentNull(reader, "reader", null);

            if (reader.ReadToFollowing("Ataw"))
                return reader.GetAttribute("version");
            return string.Empty;
        }

        private static void WriteXmlUseXmlWriter(XmlReader reader, XmlWriter writer)
        {
            while (reader.Read())
                writer.WriteNode(reader, true);
            writer.Flush();
        }

        public static MemoryStream GetXmlStream(XmlReader reader)
        {
            AtawDebug.AssertArgumentNull(reader, "reader", null);

            MemoryStream stream = new MemoryStream();
            XmlWriterSettings setting = new XmlWriterSettings
            {
                Encoding = AtawConst.UTF8,
                Indent = true
            };
            XmlWriter writer = XmlWriter.Create(stream, setting);
            using (writer)
            {
                WriteXmlUseXmlWriter(reader, writer);
            }
            return stream;
        }

        public static string GetXml(XmlReader reader)
        {
            AtawDebug.AssertArgumentNull(reader, "reader", null);

            using (MemoryStream stream = GetXmlStream(reader))
            {
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        public static void AddExceptionInfo(XElement parent, Exception ex)
        {
            AtawDebug.AssertArgumentNull(parent, "parent", null);
            AtawDebug.AssertArgumentNull(ex, "ex", null);

            XElement element = new XElement("Exception",
                new XElement("Message", ex.Message),
                new XElement("ErrorSource", ex.Source),
                new XElement("StackTrace", ex.StackTrace),
                new XElement("Type", ex.GetType().ToString()));
            if (ex.TargetSite != null)
            {
                XElement target = new XElement("TargetSite", ex.TargetSite.ToString());
                element.Add(target);
            }
            AtawException tkEx = ex as AtawException;
            if (tkEx != null && tkEx.ErrorObject != null)
            {
                XElement error = new XElement("ErrorObjType",
                    tkEx.ErrorObject.GetType().ToString());
                element.Add(error);
                error = new XElement("ErrorObj", tkEx.ErrorObject.ToString());
                element.Add(error);
            }
            AtawArgumentException argEx = ex as AtawArgumentException;
            if (argEx != null)
            {
                XElement error = new XElement("Argument", argEx.Argument);
                element.Add(error);
            }
            parent.Add(element);
            if (ex.InnerException != null)
                AddExceptionInfo(parent, ex.InnerException);
        }


        #region ValueUtil

        public static bool IsDefaultValue(object defaultValue, object value)
        {
            if (defaultValue != null)
            {
                if (defaultValue == value)
                    return true;
            }
            else
            {
                if (value == null)
                    return true;
                Type type = value.GetType();
                if (type.IsValueType)
                {
                    try
                    {
                        object nullValue = Convert.ChangeType(0, type, ObjectUtil.SysCulture);
                        if (value.Equals(nullValue))
                            return true;
                    }
                    catch
                    {
                    }
                }
            }
            return false;
        }

        public static object GetFirstEnumValue(Type enumType)
        {
            AtawDebug.AssertArgumentNull(enumType, "enumType", null);

            Array values = Enum.GetValues(enumType);
            AtawDebug.Assert(values.Length > 0, string.Format(ObjectUtil.SysCulture,
                "枚举类型{0}中没有枚举值", enumType), null);
            return (values as IList)[0];
        }

        public static object GetValue(object sender, Type type, string strValue)
        {
            return GetValue(sender, type, strValue, null);
        }

        public static object GetValue(object sender, Type type, string strValue,
            object defaultValue)
        {
            AtawDebug.AssertArgumentNull(type, "type", sender);

            TypeConverter converter = TypeDescriptor.GetConverter(type);
            AtawDebug.AssertNotNull(converter, string.Format(ObjectUtil.SysCulture,
                "无法获取类型{0}的TypeConverter，请确认是否为其配置TypeConverterAttribute",
                type), sender);
            try
            {
                return strValue == null ? GetDefaultValue(type, defaultValue, converter)
                    : converter.ConvertFromString(strValue);
            }
            catch
            {
                return GetDefaultValue(type, defaultValue, converter);
            }
        }

        internal static T InternalGetDefaultValue<T>(string strValue, T defaultValue,
            bool throwException)
        {
            Type type = typeof(T);
            TypeConverter converter = TypeDescriptor.GetConverter(type);
            if (throwException)
                AtawDebug.AssertNotNull(converter, string.Format(ObjectUtil.SysCulture,
                    "无法获取类型{0}的TypeConverter，请确认是否为其配置TypeConverterAttribute",
                    type), null);
            else
            {
                if (converter == null)
                    return default(T);
            }
            try
            {
                return strValue == null ? InternalGetDefaultValue(defaultValue)
                    : (T)converter.ConvertFromString(strValue);
            }
            catch
            {
                return InternalGetDefaultValue(defaultValue);
            }
        }

        public static T GetDefaultValue<T>(string strValue, T defaultValue)
        {
            return InternalGetDefaultValue<T>(strValue, defaultValue, true);
        }

        public static T GetDefaultValue<T>(string strValue)
        {
            Type type = typeof(T);
            if (type.IsEnum)
                return GetDefaultValue(strValue, (T)GetFirstEnumValue(type));
            else
                return GetDefaultValue(strValue, default(T));
        }

        private static T InternalGetDefaultValue<T>(T defaultValue)
        {
            if (defaultValue != null)
                return defaultValue;
            else
            {
                Type type = typeof(T);
                if (type.IsEnum)
                    return (T)GetFirstEnumValue(type);
                else
                    return default(T);
            }
        }

        private static object GetDefaultValue(Type type, object defaultValue,
            TypeConverter converter)
        {
            if (defaultValue != null)
            {
                if (ObjectUtil.IsSubType(type, defaultValue.GetType()))
                    return defaultValue;
                else
                {
                    try
                    {
                        string value = defaultValue.ToString();
                        return converter.ConvertFromString(value);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            else
            {
                if (type.IsEnum)
                {
                    return XmlUtil.GetFirstEnumValue(type);
                }
                else if (type.IsValueType)
                {
                    return ObjectUtil.CreateObject(type);
                }
                else
                    return null;
            }
        }

        #endregion

        #region XmlReadUtil

        internal static readonly XmlReaderSettings ReaderSetting = InitReading();

        private static XmlReaderSettings InitReading()
        {
            XmlReaderSettings result = new XmlReaderSettings { CloseInput = true };
            return result;
        }

        public static XmlReader GetXmlReader(string path)
        {
            AtawDebug.AssertArgumentNullOrEmpty(path, "path", null);

            XmlReader reader = XmlReader.Create(new Uri(path).ToString(), ReaderSetting);
            return reader;
        }

        public static XmlReader GetXmlReader(Stream stream)
        {
            AtawDebug.AssertArgumentNull(stream, "stream", null);

            XmlReader reader = XmlReader.Create(stream, ReaderSetting);
            return reader;
        }

        internal static readonly XmlWriterSettings WriterSetting = InitWriting();

        internal static readonly XmlWriterSettings WriterSetting2 = InitWriting2();

        private static XmlWriterSettings InitWriting()
        {
            XmlWriterSettings result = new XmlWriterSettings
            {
                CloseOutput = true
            };
            return result;
        }

        private static XmlWriterSettings InitWriting2()
        {
            XmlWriterSettings result = new XmlWriterSettings
            {
                CloseOutput = false
            };
            return result;
        }

        public static string ToString(object value)
        {
            if (value == null)
                return string.Empty;

            if (value is bool)
                return value.ToString().ToLower(ObjectUtil.SysCulture);
            TypeConverter converter = TypeDescriptor.GetConverter(value.GetType());
            if (converter == null)
                return value.ToString();
            else
                return converter.ConvertToString(value);
        }

        internal static void WriteBinaryNullable(BinaryWriter writer, bool nullable)
        {
            writer.Write(nullable);
        }
        #endregion

        #region 序列化

        private static void CallBackSerialed(Object obj)
        {
            var callBack = obj as IReadXmlCallback;
            if (callBack != null)
            {
                callBack.OnReadXml();
            }
        }

        public static T ReadFromString<T>(string xmlString)
            where T : XmlConfigBase, new()
        {
            using (Stream xmlStream = new MemoryStream(Encoding.Unicode.GetBytes(xmlString)))
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    Object obj = xmlSerializer.Deserialize(xmlReader);
                    CallBackSerialed(obj);
                    return obj as T;
                }
            }
        }

        public static object ReadFromFile(string filePath, Type type)
        {

            AtawDebug.Assert(type.IsSubclassOf(typeof(XmlConfigBase)), string.Format("类型{0}不是XmlConfigBase的子类不是xml插件", type.Name), type);
            XmlReader xmlReader = XmlUtil.GetXmlReader(filePath);
            using (xmlReader)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                Object obj = xmlSerializer.Deserialize(xmlReader);
                CallBackSerialed(obj);
                return obj;
            }
        }


        public static T ReadFromFile<T>(string filePath)
             where T : XmlConfigBase, new()
        {
            XmlReader xmlReader = XmlUtil.GetXmlReader(filePath);
            using (xmlReader)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                Object obj = xmlSerializer.Deserialize(xmlReader);
                CallBackSerialed(obj);
                return obj as T;
            }
        }

        public static byte[] SaveBinaryFormat<T>(T obj)
        {
            MemoryStream ms = new MemoryStream();
            using (ms)
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T SaveToFile<T>(string filePath, T XMLObj)
             where T : XmlConfigBase, new()
        {
            XmlSerializer x = new XmlSerializer(typeof(T));
            TextWriter writer = new StreamWriter(filePath);
            using (writer)
            {
                x.Serialize(writer, XMLObj as T);
                return null;
            }
        }
        #endregion

        #region 取完整路径和删除xml

        public static string GetFPath(string fileName)
        {
            return Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH, fileName);
        }

        public static bool DelXml(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            return true;
        }
        #endregion
    }
}
