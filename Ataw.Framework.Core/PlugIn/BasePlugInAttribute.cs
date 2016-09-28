using System;

namespace Ataw.Framework.Core
{
    /// <remarks>Class<c>BaseAttribute</c>：自定义特性基类，将不同功能类型进行分类，并以特定的自定义特性进行标记，
    /// 通过反射可以访问这些Attribute，并通过集合容器进行记录，实现灵活的插件开发。因此，不同功能类型的自定义特性需要从
    /// <c>BaseAttribute</c>继承
    /// </remarks>
    /// <summary>自定义特性基类</summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public abstract class BasePlugInAttribute : Attribute, IRegName, IAuthor
    {
        /// <summary>
        /// 构造函数，初始化自定义特性基类
        /// </summary>
        /// <param name="regName">注册名，整个应用系统中不能重复注册</param>
        protected BasePlugInAttribute(string regName)
        {
            AtawDebug.AssertArgumentNullOrEmpty(regName, "regName", this);

            RegName = regName;
        }

        /// <value>只读属性<c>RegName</c>：注册名
        /// </value>
        /// <summary>注册名</summary>
        public string RegName { get; private set; }

        /// <value>属性<c>Description</c>：功能描述
        /// </value>
        /// <summary>功能描述</summary>
        public string Description { get; set; }

        /// <value>属性<c>Author</c>：作者
        /// </value>
        /// <summary>作者</summary>
        public string Author { get; set; }

        /// <value>属性<c>CreateDate</c>：创建时间
        /// </value>
        /// <summary>创建时间</summary>
        public string CreateDate { get; set; }

        protected virtual void Assign(BasePlugInAttribute attribute)
        {
            AtawDebug.AssertArgumentNull(attribute, "attribute", this);

            RegName = attribute.RegName;
            Description = attribute.Description;
            Author = attribute.Author;
            CreateDate = attribute.CreateDate;
        }

        internal void SetDefaultValue()
        {
            Author = AtawConst.TOOLKIT;
            CreateDate = DateTime.Today.ToString(AtawConst.DATE_FMT_STR, ObjectUtil.SysCulture);
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(RegName) ? base.ToString() :
                string.Format(ObjectUtil.SysCulture, "注册名为{0}的{1}插件", RegName, GetType());
        }
    }
}
