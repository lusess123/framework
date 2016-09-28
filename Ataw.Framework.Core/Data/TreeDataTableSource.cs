using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Ataw.Framework.Core.Tree;

namespace Ataw.Framework.Core
{
    public class KeyNode
    {
        public string RootID { get; set; }
        public int Deep { get; set; }
        public string ID { get; set; }
    }

    [CodePlug(REG_NAME, BaseClass = typeof(IListDataTable),
      CreateDate = "2012-04-22", Author = "ace", Description = "树数据库表访问数据源")]
    public class TreeDataTableSource : BaseDataTableSource, ITree
    {
        private new const string REG_NAME = "TreeDataTableSource";


        private string GetStr(string s, string de)
        {
            if (s.IsEmpty())
                return de;
            else
                return s;
        }

        public virtual string ParentFieldName
        {
            get
            {

                return GetStr(ModuleFormConfig.ParentFieldName, "PID");
            }
        }

        public virtual string IsParentFieldName
        {
            get
            {
                //return ModuleFormConfig.IsParentFieldName;
                return GetStr(ModuleFormConfig.IsParentFieldName, "IS_PARENT");
            }
        }

        public virtual string TextFieldName
        {
            get
            {
                return GetStr(ModuleFormConfig.TextFieldName, "FID");
                // return ModuleFormConfig.TextFieldName;
            }
        }

        public override void InsertForeach(ObjectData data, DataRow row, string key)
        {
            var pid = data.GetDataRowValue(ParentFieldName);
            if (pid == null)
            {
                data.SetDataRowValue(ParentFieldName, 0);
                pid = "0";
            }
            UpdateLayer(null, pid.ToString());
            data.SetDataRowValue(IsParentFieldName, 0);
            base.InsertForeach(data, row, key);
        }
        public override void UpdateForeach(ObjectData data, DataRow row, string key)
        {
            var pid = data.GetDataRowValue(ParentFieldName);
            UpdateUpdateLayer(key, pid);
            base.UpdateForeach(data, row, key);
        }
        public override void DeleteForeach(string key, string data)
        {
            if (string.IsNullOrEmpty(key)) { throw new AtawException("节点为空", this); }
            var sql = string.Format("SELECT COUNT(1) FROM {0} WHERE {1}=@PARENTID", RegName, ParentFieldName);
            var paratemer = new SqlParameter[]
            {
                new SqlParameter("@PARENTID",key)
            };
            var result = DbContext.QueryObject(sql, paratemer);
            //AtawDebug.Assert(ds.Tables[0].Rows.Count <= 0, "请先删除子节点", 0);
            if (result.Value<int>() > 0)
            {
                throw new AtawException("请先删除子节点", this);
            }

            UpdateDeleteLayer(key);
            base.DeleteForeach(key, data);
        }
        private void UpdateUpdateLayer(string key, object pid)
        {
            if (pid == null) return;
            if (string.IsNullOrEmpty(key)) return;
            string originalPid = GetPidById(key);
            UpdateLayer(originalPid, pid.ToString());
        }
        private void UpdateDeleteLayer(string key)
        {
            if (string.IsNullOrEmpty(key)) return;
            string originalPid = GetPidById(key);
            UpdateLayer(originalPid, null);
        }
        private string GetPidById(string id)
        {
            if (string.IsNullOrEmpty(id)) return "";
            string sql = string.Format("SELECT {0} FROM {1} WHERE {2}=@FID", ParentFieldName, RegName, PrimaryKey);
            var paramters = new SqlParameter[]
            {
                new SqlParameter("@FID", id)
            };
            DataSet dataSet = DbContext.QueryDataSet(sql, paramters);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                return dataSet.Tables[0].Rows[0][0].ToString();
            }
            return string.Empty;
        }
        private void UpdateLayer(string originalPid, string targetPid)
        {
            string sql;
            SqlParameter[] parameters;
            if (!string.IsNullOrEmpty(originalPid) && originalPid != "0")
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@PID",originalPid )
                };
                sql = string.Format("UPDATE {0} SET {1}=0 WHERE {2}=@PID AND (SELECT COUNT(1) FROM {0} WHERE {3}=@PID)<=1", RegName, IsParentFieldName, PrimaryKey, ParentFieldName);
                DbContext.RegisterSqlCommand(sql, parameters);
            }
            if (!string.IsNullOrEmpty(targetPid) && targetPid != "0")
            {
                sql = string.Format("UPDATE {0} SET {1}=1 WHERE {2}=@PID", RegName, IsParentFieldName, PrimaryKey);
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@PID",targetPid )
                };
                DbContext.RegisterSqlCommand(sql, parameters);
            }
        }
        public IList<TreeModel> GetDisplayTreeNode(string key)
        {
            if (string.IsNullOrEmpty(key)) return null;
            string[] keys = key.Split(',');
            var allTreeModels = GetAllTree().ToList();
            KeyNode keyNode;
            IList<KeyNode> keyNodes = new List<KeyNode>();
            //找出非根节点下的最深路径
            foreach (var id in keys)
            {
                keyNode = new KeyNode { Deep = 0, ID = id };
                var deepestTreeModel = allTreeModels.FirstOrDefault(m => m.ID == id);
                if (deepestTreeModel != null && deepestTreeModel.ParentID != "0")
                {
                    FillDeepestData(deepestTreeModel, keyNode, allTreeModels);
                    keyNodes.Add(keyNode);
                }
            }
            var resultKeyNodes = from kn in keyNodes
                                 group kn by kn.RootID into g
                                 select g.FirstOrDefault(p => p.Deep == g.Max(n => n.Deep));

            var resultTreeModes = new List<TreeModel>();
            foreach (var resultKeyNode in resultKeyNodes)
            {
                //填充从根节点到最深层之间的数据

                FillFromRootToDeepestData(resultKeyNode.RootID, resultKeyNode.Deep, resultTreeModes, allTreeModels);

                //填充最深层的兄弟数据和自身
                var currentTreeModel = allTreeModels.FirstOrDefault(m => m.ID == resultKeyNode.ID);
                if (currentTreeModel.ParentID != "0")
                {
                    resultTreeModes.AddRange(allTreeModels.FindAll(m => m.ParentID == currentTreeModel.ParentID));
                }
            }
            // 填充从根节点数据
            foreach (var rootTreeModel in allTreeModels)
            {
                if (rootTreeModel.ParentID == "0")
                {
                    resultTreeModes.Add(rootTreeModel);
                }
            }
            foreach (var resultTreeModel in resultTreeModes)
            {
                if (key.Contains(resultTreeModel.ID))
                {
                    resultTreeModel.IsSelected = true;
                }
            }
            return resultTreeModes;
        }
        private void FillFromRootToDeepestData(string parentID, int deepest, IList<TreeModel> resultTreeModels, List<TreeModel> treeModels, int deep = 0)
        {
            if (deep > 0)
            {
                var nextTreeModels = treeModels.FindAll(m => m.ParentID == parentID);
                ++deep;
                if (deepest >= deep)
                {
                    foreach (var nextTreeModel in nextTreeModels)
                    {
                        resultTreeModels.Add(nextTreeModel);
                        FillFromRootToDeepestData(nextTreeModel.ID, deepest, resultTreeModels, treeModels, deep);
                    }
                }
            }
        }
        private void FillDeepestData(TreeModel treeModel, KeyNode keyNode, IList<TreeModel> treeModels)
        {
            var preTreeModel = treeModels.FirstOrDefault(m => m.ID == treeModel.ParentID && m.ID != m.ParentID);
            //防止死循环
            if (preTreeModel != null)
            {
                if (preTreeModel.ParentID == "0")
                {
                    keyNode.RootID = preTreeModel.ID;
                }
                else
                {
                    keyNode.Deep++;
                    FillDeepestData(preTreeModel, keyNode, treeModels);
                }
            }
        }
        public IList<TreeModel> GetChildrenNode(string key)
        {
            if (string.IsNullOrEmpty(key)) return null;
            string sql = string.Format("SELECT {0},{1},{2},{3} FROM {4} WHERE {1}=@PARENTID", PrimaryKey, ParentFieldName, TextFieldName, IsParentFieldName, RegName);
            var sqlparameter = new SqlParameter[]
            {
                new SqlParameter("@PARENTID",key)
            };
            var allTreeDataSet = DbContext.QueryDataSet(sql);
            return FillData(allTreeDataSet);
        }

        public IList<TreeModel> GetAllTree()
        {
            string sql = string.Format("SELECT {0},{1},{2},{3} FROM {4}", PrimaryKey, ParentFieldName, TextFieldName, IsParentFieldName, RegName);
            var allTreeDataSet = DbContext.QueryDataSet(sql);
            return FillData(allTreeDataSet);
        }
        private IList<TreeModel> FillData(DataSet dataSet)
        {
            if (dataSet == null) return null;
            var table = dataSet.Tables[0];
            if (table.Rows.Count <= 0) return null;
            var treeModels = new List<TreeModel>();
            foreach (DataRow row in table.Rows)
            {
                treeModels.Add(new TreeModel
                {
                    ID = row[PrimaryKey].ToString(),
                    ParentID = row[ParentFieldName].ToString(),
                    Name = row[TextFieldName].ToString(),
                    IsLeaf = !row[IsParentFieldName].Value<bool>()
                });
            }
            return treeModels;
        }
        //private void UpdateLayer()
        //{
        //    DataTable table = PostDataSet.Tables[RegName];
        //    if (table != null)
        //    {

        //        if (table.Columns.Contains(ModuleFormConfig.ParentFieldName))
        //        {
        //            //将被附加的父节点的Layer改为1(树杈)
        //            var parentID = table.Rows[0][ModuleFormConfig.ParentFieldName].ToString();
        //            string sql = string.Format("UPDATE {0} SET {1}=1 WHERE {2}=@FID", RegName, ModuleFormConfig.IsParentFieldName, PrimaryKey);
        //            SqlParameter[] paramters = new SqlParameter[]
        //                {
        //                    new SqlParameter("@FID",parentID )
        //                };
        //            DbContext.RegisterSqlCommand(sql, paramters);

        //            //将该节点之前的的父节点(若没有其他子节点)的Layer改为0(叶子)
        //            if (table.Columns.Contains(PrimaryKey))
        //            {
        //                sql = string.Format("UPDATE {0} SET {1}=0 WHERE {2} NOT IN(SELECT DISTINCT {3} FROM {0} WHERE {3} IN (SELECT {3} FROM {0} WHERE {2}=@FID) AND {2}<>@FID) AND {2} IN (SELECT {3} FROM {0} WHERE {2}=@FID)", RegName, ModuleFormConfig.IsParentFieldName, PrimaryKey, ModuleFormConfig.ModuleFormConfig.ParentFieldName);
        //                paramters = new SqlParameter[]
        //                {
        //                    new SqlParameter("@FID", table.Rows[0][PrimaryKey])
        //                };
        //                DbContext.RegisterSqlCommand(sql, paramters);
        //            }
        //        }
        //    }

        //}
        //protected override string AdditionalConditionSql(string sql)
        //{
        //    //首次加载,不返回数据
        //    if (PostDataSet == null) {
        //        return sql += " AND 1=2";
        //    }
        //    return sql;
        //}
    }
}
