using System;

namespace Ataw.Framework.Core
{
    internal sealed class CodeRegItem : BaseRegItem
    {
        /// <summary>
        /// Initializes a new instance of the CodeRegItem class.
        /// </summary>
        internal CodeRegItem(string regName, Type regType, BasePlugInAttribute attribute)
            : base(regName, attribute)
        {
            RegType = regType;
        }

        public Type RegType { get; private set; }

        public override T CreateInstance<T>()
        {
            object result = ObjectUtil.CreateObject(RegType);
            AssertCreateType<T>(RegType);

            return result as T;
        }

        public override T CreateInstance<T>(params object[] args)
        {
            object result = ObjectUtil.CreateObject(RegType, args);
            AssertCreateType<T>(RegType);

            return result as T;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(RegName) ? base.ToString() :
                string.Format(ObjectUtil.SysCulture, "注册名为{0}Code配置单元", RegName);
        }
    }
}
