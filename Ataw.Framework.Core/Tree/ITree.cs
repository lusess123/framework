using System.Collections.Generic;

namespace Ataw.Framework.Core.Tree
{
    interface ITree
    {
        IList<TreeModel> GetDisplayTreeNode(string key);
        IList<TreeModel> GetChildrenNode(string key);
        IList<TreeModel> GetAllTree();
    }
}
