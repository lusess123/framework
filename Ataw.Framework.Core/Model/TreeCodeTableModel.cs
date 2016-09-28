using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    public class TreeCodeTableModel : CodeDataModel
    {
        public bool isParent { get; set; }
        private bool isLeaf;
        public bool IsLeaf
        {
            get { return isLeaf; }
            set
            {
                this.isLeaf = value;
                this.nocheck = !value;
                //this.chkDisabled = !value;
            }
        }
        //public bool IsLeaf { get; set; }
        public IEnumerable<TreeCodeTableModel> Children;
        public string ParentId { get; set; }
        public bool open { get; set; }
        public string Arrange { get; set; }
        public object ExtData { get; set; }
        public int Order { get; set; }
        public int LayerLevel { get; set; }
        //public bool chkDisabled { get; set; }
        public bool? nocheck { get; set; }
        // public bool IsSelect { get; set; }
        public bool IsHidden { get; set; }
    }
}
