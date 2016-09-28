using System;

namespace Ataw.Framework.Core
{
    /// <remarks>Class<c>BaseAttribute</c>���Զ������Ի��࣬����ͬ�������ͽ��з��࣬�����ض����Զ������Խ��б�ǣ�
    /// ͨ��������Է�����ЩAttribute����ͨ�������������м�¼��ʵ�����Ĳ����������ˣ���ͬ�������͵��Զ���������Ҫ��
    /// <c>BaseAttribute</c>�̳�
    /// </remarks>
    /// <summary>�Զ������Ի���</summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public abstract class BasePlugInAttribute : Attribute, IRegName, IAuthor
    {
        /// <summary>
        /// ���캯������ʼ���Զ������Ի���
        /// </summary>
        /// <param name="regName">ע����������Ӧ��ϵͳ�в����ظ�ע��</param>
        protected BasePlugInAttribute(string regName)
        {
            AtawDebug.AssertArgumentNullOrEmpty(regName, "regName", this);

            RegName = regName;
        }

        /// <value>ֻ������<c>RegName</c>��ע����
        /// </value>
        /// <summary>ע����</summary>
        public string RegName { get; private set; }

        /// <value>����<c>Description</c>����������
        /// </value>
        /// <summary>��������</summary>
        public string Description { get; set; }

        /// <value>����<c>Author</c>������
        /// </value>
        /// <summary>����</summary>
        public string Author { get; set; }

        /// <value>����<c>CreateDate</c>������ʱ��
        /// </value>
        /// <summary>����ʱ��</summary>
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
                string.Format(ObjectUtil.SysCulture, "ע����Ϊ{0}��{1}���", RegName, GetType());
        }
    }
}
