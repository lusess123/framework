using System.Collections.Generic;
using Ataw.Framework.Core.Instance;

namespace Ataw.Framework.Core
{
    [CodePlug("TREE_TESTCodeTable", BaseClass = typeof(CodeTable<CodeDataModel>),
  CreateDate = "2012-11-5", Author = "", Description = "测试树数据源")]
    public class TestTreeCodeTable : DbTreeCodeTable
    {
        /// <summary>
        /// 这个作废了
        /// </summary>
        protected override IEnumerable<TreeCodeTableModel> SimpleDataList
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        public override string TableName
        {
            get { return "AtawControlTest"; }
        }
        public override bool OnlyLeafCheckbox { get { return false; } }
        public override string CodeValueField
        {
            get { return "FID"; }
        }

        public override string CodeTextField
        {
            get { return "AtawText"; }
        }

        public override string ParentIdField
        {
            get { return "ParentID"; }
        }

        public override string IsParentField
        {
            get { return "ISLAYER"; }
        }
    }
}
