
namespace Ataw.Framework.Core.Tree
{
    public class TreeModel
    {
        public string ID { get; set; }
        public string ParentID { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public bool? nocheck { get; set; }
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
    }
}
