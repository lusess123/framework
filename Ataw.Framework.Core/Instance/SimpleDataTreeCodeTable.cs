using System.Collections.Generic;
using System.Linq;

namespace Ataw.Framework.Core
{
    public abstract class SimpleDataTreeCodeTable : TreeCodeTable
    {
        private bool isFirst = true;

        private TreeCodeTableModel MergeOutHouse(TreeCodeTableModel person, TreeCodeTableModel house, bool isSrc)
        {
            string pid = person.ParentId;
            TreeCodeTableModel pNode = getTreeNode(pid);
            var _list = GetChildrenNode(pid);
            pNode.open = true;
            var __list = _list.Children.ToList();
            for (int i = 0; i < __list.Count; i++)//必须这么写
            {
                if (__list[i].CODE_VALUE == person.CODE_VALUE)
                {
                    __list[i] = person;
                    //person.IsSelect = true;
                }
            }
            pNode.Children = __list;

            TreeCodeTableModel result = MergeInHouse(pNode, house, house);
            if (result == null)
            {
                return MergeOutHouse(pNode, house, false);
            }

            return result;

        }
        private TreeCodeTableModel MergeInHouse(TreeCodeTableModel person, TreeCodeTableModel house, TreeCodeTableModel bighouse)
        {
            if (house.CODE_VALUE == person.CODE_VALUE)
            {
                house.open = person.Children != null;
                if (!house.IsSelect)
                    house.IsSelect = person.IsSelect;
                house.Children = person.Children;
                return bighouse;
            }
            else
            {
                if (house.Children != null)
                {
                    var _list = house.Children;
                    //for (int i = 0; i < _list.Count(); i++)
                    //{
                    foreach (TreeCodeTableModel bean in house.Children)
                    {
                        // TreeCodeTableModel _o = bean;
                        var _res = MergeInHouse(person, bean, bighouse);
                        if (_res != null)
                            return _res;
                        // return _bean;
                    }
                    //}
                }
                return null;
            }
        }
        private TreeCodeTableModel GetDisplayTreeNodeByNode(TreeCodeTableModel node, IEnumerable<TreeCodeTableModel> list)
        {
            if (node != null)
            {
                if (isFirst)
                    isFirst = true;
                node.Children = list;
                node.open = true;
                string pid = node.ParentId;

                var _list = GetChildrenNode(pid);

                var __list = _list.Children.ToList();
                for (int i = 0; i < __list.Count; i++)//必须这么写
                {
                    if (__list[i].CODE_VALUE == node.CODE_VALUE)
                        __list[i] = node;
                }
                _list.Children = __list;
                if (pid != this.Root)
                {
                    var _pnode = GetDisplayTreeNodeByNode(getTreeNode(pid), _list.Children);
                    return _pnode == null ? _list : _pnode;
                }
                else
                {
                    return _list;
                }
            }
            return null;

        }

        #region 新成员
        protected abstract IEnumerable<TreeCodeTableModel> SimpleDataList
        {
            get;
            set;
        }

        protected virtual TreeCodeTableModel getTreeNode(string key)
        {
            return SimpleDataList.Where(a => a.CODE_VALUE == key).FirstOrDefault();
        }

        #endregion

        #region codetable 成员
        public override CodeDataModel this[string key]
        {
            get
            {

                string _classname = "{0}_{1}_{2}".AkFormat(this.GetType().Name, Sign ?? "",this.Product??"",key);
                //this.GetType().FullName +  +   +  typeof(this.SimpleDataList);
                var _pf = AtawAppContext.Current.PageFlyweight;
              
                return _pf.GetAndSet<CodeDataModel>(_classname+key, () => getTreeNode(key));


                //return ;
            }
        }
        #endregion

        #region 树成员


        protected virtual IEnumerable<TreeCodeTableModel> GetChildrenNodes(string key)
        {
            return SimpleDataList.Where(a => a.ParentId == key);
        }

        public override TreeCodeTableModel GetChildrenNode(string key)
        {
            TreeCodeTableModel root = new TreeCodeTableModel()
            {
                CODE_TEXT = "",
                CODE_VALUE = key,
                IsLeaf = false,
                isParent = true,
                nocheck = OnlyLeafCheckbox ? true.Value<bool?>() : null
                //IsSelect = true
            };
            root.Children = GetChildrenNodes(key);
            return root;
        }

        protected virtual void OnTreeNodeSetting(TreeCodeTableModel nodeModel)
        {

        }

        public override TreeCodeTableModel GetDisplayTreeNode(string key)
        {
            if (KeyValues == null)
            {
                TreeCodeTableModel bean = getTreeNode(key);
                if (bean != null)
                {
                    bean.IsSelect = true;
                    return GetDisplayTreeNodeByNode(bean, null);
                }
                return bean;
            }
            else
            {
                TreeCodeTableModel bean = null;
                foreach (string _key in KeyValues)
                {
                    TreeCodeTableModel _bean = getTreeNode(_key);
                    if (_bean != null)
                    {
                        _bean.IsSelect = true;
                        if (bean != null)
                        {

                            TreeCodeTableModel beanM = MergeInHouse(_bean, bean, bean);
                            if (beanM == null)
                            {
                                beanM = MergeOutHouse(_bean, bean, true);
                            }
                            bean = beanM;
                        }
                        else
                        {
                            bean = GetDisplayTreeNodeByNode(_bean, null);
                        }
                    }
                }
                return bean;
            }

        }

        public override TreeCodeTableModel GetAllTree()
        {
            TreeCodeTableModel root = new TreeCodeTableModel();
            root.CODE_VALUE = Root;
            ArrangeBuilder ab = new ArrangeBuilder();


            if (Root == "0")
            {
                root.CODE_TEXT = "";
                root.IsLeaf = false;
                root.isParent = true;
                root.nocheck = OnlyLeafCheckbox ? root.isParent.Value<bool?>() : null;
            }
            else
            {
                root = getTreeNode(Root);
            }
            // root.Arrange = ab.GetText();
            BindChild(root, ab);
            return root;
        }

        private void BindChild(TreeCodeTableModel node, ArrangeBuilder ab)
        {
            if (this.KeyValues != null && this.KeyValues.Count() > 0 )
            {
                var _kvs = this.KeyValues.ToList();

                foreach (var k in _kvs)
                {
                    if (node.CODE_VALUE == k)
                    {
                        node.IsSelect = true;
                        break;
                    }
                }
            }

            node.Arrange = ab.GetText();
            node.LayerLevel = ab.LayerLevel;
            this.OnTreeNodeSetting(node);
            var _nodes = GetChildrenNodes(node.CODE_VALUE).ToList();
            if (_nodes != null && _nodes.Count > 0)
            {
                node.Children = _nodes;
                node.isParent = true;

                ArrangeBuilder abb = ab.Copy();
                abb.AddLevelNode();
                for (int i = 0; i < node.Children.Count(); i++)
                {
                    abb.AddNode();
                    BindChild((node.Children as IList<TreeCodeTableModel>)[i], abb);
                }

            }
            else
            {
                node.isParent = false;
                node.Children = new List<TreeCodeTableModel>();
            }
        }
        #endregion

        private void SetDescendent(string key, List<CodeDataModel> list)
        {
            var _list = GetChildrenNodes(key);
            if (_list.Count() > 0)
            {
                foreach (var l in _list)
                {
                    string _key = l.CODE_VALUE;
                    list.Add(l);

                    SetDescendent(_key,list);
                }
            }
        }

        public override IEnumerable<CodeDataModel> GetDescendent(string key)
        {
            List<CodeDataModel> list = new List<CodeDataModel>();
            SetDescendent(key, list);
            return list;
            //throw new System.NotImplementedException();
        }


        public override IEnumerable<CodeDataModel> FillData(System.Data.DataSet postDataSet)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<CodeDataModel> BeginSearch(System.Data.DataSet postDataSet, string key)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<CodeDataModel> Search(System.Data.DataSet postDataSet, string key)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<CodeDataModel> FillAllData(System.Data.DataSet postDataSet)
        {
            throw new System.NotImplementedException();
        }
    }
}
