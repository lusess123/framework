using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Ataw.Framework.Core
{
    public static class AtawDebug
    {
        [Conditional(AtawConst.DEBUG)]
        public static void AssertArgumentNull(object argValue, string argument, object obj)
        {
            if (argValue == null)
                throw new AtawArgumentNullException(argument, obj);
        }

        [Conditional(AtawConst.DEBUG)]
        public static void AssertEnumerableArgumentNull(IEnumerable<object> argValues,
            string argument, object obj)
        {
            AssertEnumerableArgumentNull(argValues, argument, obj, true);
        }

        [Conditional(AtawConst.DEBUG)]
        public static void AssertEnumerableArgumentNull(IEnumerable<object> argValues,
            string argument, object obj, bool throwOnNoElements)
        {
            AssertEnumerableArgumentNull<object>(argValues, argument, obj, throwOnNoElements);
        }

        [Conditional(AtawConst.DEBUG)]
        public static void AssertEnumerableArgumentNull<T>(IEnumerable<T> argValues,
            string argument, object obj) where T : class
        {
            AssertEnumerableArgumentNull<T>(argValues, argument, obj, true);
        }

        [Conditional(AtawConst.DEBUG)]
        public static void AssertEnumerableArgumentNull<T>(IEnumerable<T> argValues,
            string argument, object obj, bool throwOnNoElements) where T : class
        {
            if (argValues == null)
                throw new AtawArgumentNullException(argument, obj);
            int i = 0;
            foreach (T item in argValues)
            {
                if (item == null)
                    throw new AtawArgumentException(argument, string.Format(ObjectUtil.SysCulture,
                        "参数{0}的第{1}个元素是NULL，这是不允许的，请确认", argument, i), null);
                ++i;
            }
            if (throwOnNoElements && i == 0)
                throw new AtawArgumentException(argument, string.Format(ObjectUtil.SysCulture,
                    "集合参数{0}的至少需要有一个元素，现在一个都没有", argument), null);
        }


        [Conditional(AtawConst.DEBUG)]
        public static void AssertArgumentNullOrEmpty(string argValue, string argument, object obj)
        {
            if (string.IsNullOrEmpty(argValue))
                throw new AtawArgumentNullException(argument, obj);
        }

        [Conditional(AtawConst.DEBUG)]
        public static void AssertEnumerableArgumentNullOrEmpty(IEnumerable<string> argValues,
            string argument, object obj)
        {
            AssertEnumerableArgumentNullOrEmpty(argValues, argument, obj, true);
        }

        [Conditional(AtawConst.DEBUG)]
        public static void AssertEnumerableArgumentNullOrEmpty(IEnumerable<string> argValues,
            string argument, object obj, bool throwOnNoElements)
        {
            if (argValues == null)
                throw new AtawArgumentNullException(argument, obj);
            int i = 0;
            foreach (string item in argValues)
            {
                if (string.IsNullOrEmpty(item))
                    throw new AtawArgumentException(argument, string.Format(ObjectUtil.SysCulture,
                        "参数{0}的第{1}个元素是NULL，这是不允许的，请确认", argument, i), null);
                ++i;
            }
            if (throwOnNoElements && i == 0)
                throw new AtawArgumentException(argument, string.Format(ObjectUtil.SysCulture,
                    "集合参数{0}的至少需要有一个元素，现在一个都没有", argument), null);
        }

        [Conditional(AtawConst.DEBUG)]
        public static void AssertArgument(bool condition, string argument, string message, object obj)
        {
            if (!condition)
                throw new AtawArgumentException(argument, message, obj);
        }

        [Conditional(AtawConst.DEBUG)]
        public static void Assert(bool condition, string message, object obj)
        {
            if (!condition)
                throw new AtawAssertException(message, obj);
        }

        [Conditional(AtawConst.DEBUG)]
        public static void AssertNotNull(object value, string message, object obj)
        {
            if (value == null)
                throw new AtawAssertException(message, obj);
        }

        [Conditional(AtawConst.DEBUG)]
        public static void AssertNotNullOrEmpty(string value, string message, object obj)
        {
            if (string.IsNullOrEmpty(value))
                throw new AtawAssertException(message, obj);
        }

        [Conditional(AtawConst.DEBUG)]
        public static void ThrowAtawException(string message, object obj)
        {
            throw new AtawException(message, obj);
        }

        [Conditional(AtawConst.DEBUG)]
        public static void ThrowAtawException(string message, Exception innerException, object obj)
        {
            throw new AtawException(message, innerException, obj);
        }

        [Conditional(AtawConst.DEBUG)]
        public static void ThrowIfNoAppSetting()
        {
            //if (AbstractAppSetting.InternalCurrent == null)
            throw new AtawException("AppSetting没有被初始化，请检查", null);
        }

        [Conditional(AtawConst.DEBUG)]
        public static void ThrowIfNoGlobalVariable()
        {
            // if (AbstractGlobalVariable.Current == null)
            throw new AtawException("GlobalVariable没有被初始化，请检查", null);
        }

        [Conditional(AtawConst.DEBUG)]
        public static void ThrowImpossibleCode(object obj)
        {
            throw new AtawException("代码不可能执行到这里", obj);
        }
    }
}
