using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Ataw.Framework.Core
{
    public class ObjectUtil
    {


        public static void DisposeListObject<T>(ICollection<T> list) where T : IDisposable
        {
            if (list != null)
            {
                foreach (T obj in list)
                    obj.Dispose();
                list.Clear();
            }
        }


        public static void DisposeObject(object value)
        {
            IDisposable dispose = value as IDisposable;
            if (dispose != null)
                dispose.Dispose();
        }

        private static string GetMethodSign(object[] args)
        {
            StringBuilder builder = new StringBuilder();
            int i = 0;
            Array.ForEach(args,
                obj => StringUtil.JoinStringItem(builder, i++, obj.GetType().ToString()));
            return builder.ToString();
        }
        /// <summary>
        /// 获取枚举变量值的 Description 属性
        /// </summary>
        /// <param name="obj">枚举变量</param>
        /// <param name="isTop">是否改变为返回该类、枚举类型的头 Description 属性，而不是当前的属性或枚举变量值的 Description 属性</param>
        /// <returns>如果包含 Description 属性，则返回 Description 属性的值，否则返回枚举变量值的名称</returns>
        public static string GetDescription(object obj)
        {
            bool isTop = false;
            if (obj == null)
            {
                return string.Empty;
            }
            try
            {
                Type _enumType = obj.GetType();
                DescriptionAttribute dna = null;
                if (isTop)
                {
                    dna = (DescriptionAttribute)Attribute.GetCustomAttribute(_enumType, typeof(DescriptionAttribute));
                }
                else
                {
                    FieldInfo fi = _enumType.GetField(Enum.GetName(_enumType, obj));
                    dna = (DescriptionAttribute)Attribute.GetCustomAttribute(
                       fi, typeof(DescriptionAttribute));
                }
                if (dna != null && string.IsNullOrEmpty(dna.Description) == false)
                    return dna.Description;
            }
            catch
            {
            }
            return obj.ToString();
        }

        public static object CreateObject(Type type, params object[] args)
        {
            AtawDebug.AssertArgumentNull(type, "type", null);
            AtawDebug.AssertEnumerableArgumentNull(args, "args", null);
            try
            {
                return Activator.CreateInstance(type, args);
            }
            catch (MissingMethodException ex)
            {
                AtawDebug.ThrowAtawException(string.Format(ObjectUtil.SysCulture,
                    "没有找到类型{0}参数为{1}的构造函数，请确认", type, GetMethodSign(args)), ex, null);
            }
            catch (TargetInvocationException ex)
            {
                AtawDebug.ThrowAtawException(string.Format(ObjectUtil.SysCulture,
                    "调用类型{0}参数为{1}的构造函数时，发生例外，请调试你的代码",
                    type, GetMethodSign(args)), ex, null);
            }
            catch (MethodAccessException ex)
            {
                AtawDebug.ThrowAtawException(string.Format(ObjectUtil.SysCulture,
                    "类型{0}参数为{1}的构造函数访问权限不够",
                    type, GetMethodSign(args)), ex, null);
            }
            catch (MemberAccessException ex)
            {
                AtawDebug.ThrowAtawException(string.Format(ObjectUtil.SysCulture,
                    "类型{0}可能是一个抽象类型，无法创建", type), ex, null);
            }
            catch (Exception ex)
            {
                AtawDebug.ThrowAtawException(string.Format(ObjectUtil.SysCulture,
                    "调用类型{0}参数为{1}的构造函数是发生例外",
                    type, GetMethodSign(args)), ex, null);
            }
            return null;
        }

        public static object CreateObject(Type type)
        {
            AtawDebug.AssertArgumentNull(type, "type", null);

            try
            {
                return Activator.CreateInstance(type);
            }
            catch (MissingMethodException ex)
            {
                AtawDebug.ThrowAtawException(string.Format(ObjectUtil.SysCulture,
                    "没有找到类型{0}的默认构造函数，请确认", type), ex, null);
            }
            catch (TargetInvocationException ex)
            {
                AtawDebug.ThrowAtawException(string.Format(ObjectUtil.SysCulture,
                    "调用类型{0}的默认构造函数时，发生例外，请调试你的代码", type), ex, null);
            }
            catch (MethodAccessException ex)
            {
                AtawDebug.ThrowAtawException(string.Format(ObjectUtil.SysCulture,
                    "类型{0}的默认构造函数访问权限不够", type), ex, null);
            }
            catch (MemberAccessException ex)
            {
                AtawDebug.ThrowAtawException(string.Format(ObjectUtil.SysCulture,
                    "类型{0}可能是一个抽象类型，无法创建", type), ex, null);
            }
            catch (Exception ex)
            {
                AtawDebug.ThrowAtawException(string.Format(ObjectUtil.SysCulture,
                    "调用类型{0}的默认构造函数是发生例外", type), ex, null);
            }
            return null;
        }

        public static bool IsSubType(Type baseType, Type type)
        {
            //AtawDebug.AssertArgumentNull(baseType, "baseType", null);
            //AtawDebug.AssertArgumentNull(type, "type", null);

            if (baseType.IsInterface)
            {
                if (baseType.IsGenericType)
                {
                    Type[] interfaces = type.GetInterfaces();
                    foreach (Type intf in interfaces)
                    {
                        if (baseType.IsGenericTypeDefinition)
                        {
                            if (intf.Namespace == baseType.Namespace && intf.Name == baseType.Name)
                                return true;
                        }
                        else
                            if (intf == baseType)
                                return true;
                    }
                    return false;
                }
                else
                {
                    Type typeGetInterface = type.GetInterface(baseType.FullName);
                    if (typeGetInterface == null)
                        return false;
                    return typeGetInterface == baseType;
                }
            }
            else
                return type.IsSubclassOf(baseType) || baseType == type;
        }

        public static IEnumerable<T> Convert<T>(T value)
        {
            //AtawDebug.AssertArgumentNull(value, "value", null);

            yield return value;
        }

        public static IEnumerable<T> Convert<T>(T value, IEnumerable<T> values)
        {
            // AtawDebug.AssertArgumentNull(value, "value", null);
            // AtawDebug.AssertArgumentNull(values, "values", null);

            yield return value;
            foreach (T item in values)
                yield return item;
        }

        public static IEnumerable<TDest> Convert<TDest, TSource>(TSource value,
            IEnumerable<TSource> values) where TSource : TDest
        {
            // AtawDebug.AssertArgumentNull(value, "value", null);
            // AtawDebug.AssertArgumentNull(values, "values", null);

            yield return value;
            foreach (TDest item in values)
                if (item != null)
                    yield return item;
        }

        public static IEnumerable<T> Convert<T>(params IEnumerable<T>[] values)
        {
            //AtawDebug.AssertArgumentNull(values, "values", null);

            foreach (var items in values)
                if (items != null)
                {
                    foreach (T item in items)
                        if (item != null)
                            yield return item;
                }
        }

        public static IEnumerable<T> Convert<T>(params IEnumerable[] values) where T : class
        {
            //AtawDebug.AssertArgumentNull(values, "values", null);

            foreach (var items in values)
            {
                if (items != null)
                    foreach (var item in items)
                    {
                        if (item != null)
                            yield return item as T;
                    }
            }
        }

        public static T TryGetValue<T>(Dictionary<string, T> dictionary, string key)
        {
            T result;
            return dictionary.TryGetValue(key, out result) ? result : default(T);
        }

        public static T[] CopyArray<T>(T[] source)
        {
            if (source == null)
                return null;
            T[] result = new T[source.Length];
            Array.Copy(source, result, source.Length);
            return result;
        }

        #region 本地化操作
        public static CultureInfo SysCulture
        {
            get
            {
                return CultureInfo.CurrentCulture;
            }
        }
        #endregion


        #region XML
        public static void SetValue(PropertyInfo info, object obj, object value)
        {
            AtawDebug.AssertArgumentNull(info, "info", null);
            AtawDebug.AssertArgumentNull(obj, "obj", null);

            try
            {
                info.SetValue(obj, value, null);
            }
            catch (System.ArgumentException ex)
            {
                AtawDebug.ThrowAtawException(string.Format(ObjectUtil.SysCulture,
                    "对象{0}未找到属性{1}的 set 访问器或者数组不包含所需类型的参数",
                    obj.GetType(), info.Name), ex, null);

            }
            catch (TargetException ex)
            {
                AtawDebug.ThrowAtawException(string.Format(ObjectUtil.SysCulture,
                    "对象{0}中可能不存在属性{1}，请检查",
                    obj.GetType(), info.Name), ex, null);
            }
            catch (MethodAccessException ex)
            {
                AtawDebug.ThrowAtawException(string.Format(ObjectUtil.SysCulture,
                    "无法设置对象{0}中属性{1}的值，请检查set的访问权限",
                    obj.GetType(), info.Name), ex, null);

            }
        }
        #endregion

        public static Dictionary<string, string> GetEnumFields(Type Enumtype)
        {
            AtawDebug.AssertArgumentNull(Enumtype, "Enumtype", null);

            if (!Enumtype.IsEnum) throw new AtawException("参数类型不正确", Enumtype);

            var dict = new Dictionary<string, string>();
            FieldInfo[] fieldinfo = Enumtype.GetFields();
            foreach (FieldInfo item in fieldinfo)
            {
                Object[] obj = item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (obj != null && obj.Length != 0)
                {
                    DescriptionAttribute des = (DescriptionAttribute)obj[0];
                    dict.Add(item.Name, des.Description);
                }
            }
            return dict;
        }
    }
}
