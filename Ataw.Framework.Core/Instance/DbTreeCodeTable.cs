using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Ataw.Framework.Core.Instance
{
    public abstract class DbTreeCodeTable : SimpleDataTreeCodeTable
    {
        /// <summary>
        /// 表名
        /// </summary>
        public abstract string TableName
        {
            get;
        }

        public abstract string CodeValueField
        {
            get;
        }
        public abstract string CodeTextField
        {
            get;
        }
        public abstract string ParentIdField
        {
            get;
        }

        public abstract string IsParentField
        {
            get;
        }

        public virtual AtawDbContext DbContext
        {
            get;
            set;
        }

        public DbTreeCodeTable()
        {
            var app = AtawAppContext.Current;
            DbContext = (AtawDbContext)AtawAppContext.Current.FetchUnitofData(app.FControlUnitID, Product);
        }


        public virtual DataRow Row
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展查询方法
        /// </summary>
        /// <param name="row">参数列表</param>
        /// <returns></returns>
        protected virtual string where()
        {
            string sql = "";
            if (Row != null)
            {
                DataColumnCollection cols = Row.Table.Columns;
                foreach (DataColumn col in cols)
                {
                    sql += string.Format(ObjectUtil.SysCulture, " AND {0} = '{1}'", col.ColumnName, Row[col.ColumnName].ToString());
                    //sql += "AND " + col.ColumnName + " = '" + Row[col.ColumnName].ToString() + "'";
                }
            }
            return sql;
        }

        protected override TreeCodeTableModel getTreeNode(string key)
        {
            var ds = getDataSet(0, "", string.Format(ObjectUtil.SysCulture, " {0} = '{1}'" + where(), CodeValueField, key), null);
            //string.Format(ObjectUtil.SysCulture,
            return SelectTree(ds.Tables[0]).FirstOrDefault();
        }


        public override TreeCodeTableModel GetChildrenNode(string key)
        {
            var ds = getDataSet(0, "", string.Format(ObjectUtil.SysCulture, " {0} = '{1}' " + where(), ParentIdField, key), null);
            TreeCodeTableModel root = new TreeCodeTableModel()
            {
                CODE_TEXT = "",
                CODE_VALUE = key,
                IsLeaf = false,
                isParent = true,
                nocheck = OnlyLeafCheckbox ? true.Value<bool?>() : null
                //IsSelect = true
            };
            root.Children = SelectTree(ds.Tables[0]);
            return root;
        }

        public override CodeDataModel this[string key]
        {
            get
            {
                var ds = getDataSet(0, "", string.Format(ObjectUtil.SysCulture, " {0} = '{1}' ", CodeValueField, key), null);
                return Select(ds.Tables[0]).FirstOrDefault();
            }
        }

        public override IEnumerable<CodeDataModel> FillData(DataSet postDataSet)
        {
            var ds = getDataSet(10, "", "", null);
            return Select(ds.Tables[0]);
        }

        public override IEnumerable<CodeDataModel> BeginSearch(System.Data.DataSet postDataSet, string key)
        {
            var ds = getDataSet(0, "", string.Format(ObjectUtil.SysCulture, " {0} LIKE '%{1}' ", CodeTextField, key), null);
            return Select(ds.Tables[0]);
        }

        public override IEnumerable<CodeDataModel> Search(DataSet postDataSet, string key)
        {
            var ds = getDataSet(0, "", string.Format(ObjectUtil.SysCulture, " {0} LIKE '%{1}' ", CodeTextField, key), null);
            return Select(ds.Tables[0]);
        }

        public override IEnumerable<CodeDataModel> FillAllData(DataSet postDataSet)
        {
            //SqlParameter fidP = new SqlParameter("",
            var ds = getDataSet(0, "", "", null);
            return Select(ds.Tables[0]);
        }

        protected virtual  IEnumerable<TreeCodeTableModel> SelectTree(DataTable table)
        {
            List<TreeCodeTableModel> result = new List<TreeCodeTableModel>();
            // DataSet ds = getDataSet(0, "", "", null);
            foreach (DataRow row in table.Rows)
            {
                result.Add(new TreeCodeTableModel()
                {
                    CODE_TEXT = CodeTextFormat(row),
                    CODE_VALUE = row[CodeValueField].ToString(),
                    IsLeaf = !row[IsParentField].Value<bool>(),
                    isParent = row[IsParentField].Value<bool>(),
                    ParentId = row[ParentIdField].ToString(),
                    nocheck = OnlyLeafCheckbox ? row[IsParentField].Value<bool?>() : null
                    // IsSelect = true
                    //  Row = row
                });
            }
            return result;
        }

        protected virtual string CodeTextFormat(DataRow row)
        {
            return row[CodeTextField].ToString();
        }

        protected  virtual IEnumerable<CodeDataModel> Select(DataTable table)
        {
            List<CodeDataModel> result = new List<CodeDataModel>();
            // DataSet ds = getDataSet(0, "", "", null);
            foreach (DataRow row in table.Rows)
            {
                result.Add(new CodeDataModel()
                {
                    CODE_TEXT = CodeTextFormat(row),
                    CODE_VALUE = row[CodeValueField].ToString()
                    //  Row = row
                });
            }
            return result;
        }

        protected DataSet getDataSet(int top, string fields, string where, params SqlParameter[] paras)
        {
            string strTop = "";
            if (top > 0)
            {
                strTop = " TOP " + top.ToString();
            }
            string strFields = " * ";
            if (!fields.IsEmpty())
            {
                strFields = fields;
            }
            string strWhere = " WHERE 1=1 ";
            if (!where.IsEmpty())
            {
                strWhere += (" AND " + where);
            }
            DataSet ds = DbContext.QueryDataSet(string.Format(ObjectUtil.SysCulture,
                "SELECT {0}{1} FROM {2}{3}{4}", strTop, strFields, TableName, 
                strWhere,OrderSqlCreate()), paras);
            return ds;
        }

        protected virtual string OrderSqlCreate()
        {
            return "";
        }

        private void SetDescendent(string key, List<CodeDataModel> list)
        {
            //list.Add(getTreeNode(key));
            var _root = GetChildrenNode(key);
            if (_root.Children != null && _root.Children.Count() > 0)
            {
                foreach (var item in _root.Children)
                {
                    list.Add(item);
                    SetDescendent(item.CODE_VALUE, list);
                }
            }

        }
        public override IEnumerable<CodeDataModel> GetDescendent(string key)
        {
            List<CodeDataModel> list = new List<CodeDataModel>();
            SetDescendent(key, list);
            return list;
        }


        private IEnumerable<TreeCodeTableModel> fSimpleDataList;

        protected override IEnumerable<TreeCodeTableModel> SimpleDataList
        {
            get
            {
                if (this.fSimpleDataList == null) {
                    var ds = getDataSet(0, "", "", null);
                    fSimpleDataList = SelectTree(ds.Tables[0]);
                   
                }
                return fSimpleDataList;
            }
            set
            {
                fSimpleDataList = value;
            }
        }
        public string DeleteNode(string key)
        {
            //AtawDebug.Assert(!string.IsNullOrEmpty(key), "请先删除子节点", 0);
            if (string.IsNullOrEmpty(key)) { return (new JsResponseResult<object> { ActionType = JsActionType.Object, Obj = new { type = 0, message = "节点为空" } }).ToString(); }
            var ds = getDataSet(0, "", string.Format(ObjectUtil.SysCulture, " {0} = '{1}' " + where(), ParentIdField, key), null);
            //AtawDebug.Assert(ds.Tables[0].Rows.Count <= 0, "请先删除子节点", 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return (new JsResponseResult<object> { ActionType = JsActionType.Object, Obj = new { type = 0, message = "请先删除子节点" } }).ToString();
            }

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@FID",key)
            };
            DbContext.QueryObject(string.Format("UPDATE {0} SET {1}=0 WHERE {2} NOT IN(SELECT DISTINCT {3} FROM {0} WHERE {3} IN (SELECT {3} FROM {0} WHERE {2}=@FID) AND {2}<>@FID) AND {2} IN (SELECT {3} FROM {0} WHERE {2}=@FID)", TableName, IsParentField, CodeValueField, ParentIdField), param);
            param = new SqlParameter[]
            {
                new SqlParameter("@FID",key)
            };
            DbContext.QueryObject(string.Format(ObjectUtil.SysCulture, "DELETE FROM  {0} WHERE {1}=@FID", TableName, CodeValueField), param);

            return (new JsResponseResult<object> { ActionType = JsActionType.Object, Obj = new { type = 1, message = "删除成功" } }).ToString();

        }
        public string DropNodes(string keys, string targetNodeId)
        {
            try
            {
                if (string.IsNullOrEmpty(keys) || string.IsNullOrEmpty(targetNodeId))
                {
                    return (new JsResponseResult<int> { ActionType = JsActionType.Alert, Content = "要拖拽或目标节点不明确", Obj = 0 }).ToString();
                }
                //将被附加的父节点的Layer改为1(树杈)
                string sql = string.Format("UPDATE {0} SET {1}=1 WHERE {2}=@FID", TableName, IsParentField, CodeValueField);
                SqlParameter[] paramters = new SqlParameter[]
                        {
                            new SqlParameter("@FID",targetNodeId )
                        };
                DbContext.RegisterSqlCommand(sql, paramters);

                //将该节点之前的的父节点(若没有其他子节点)的Layer改为0(叶子)
                var ids = keys.Split(',');
                foreach (var id in ids)
                {
                    sql = string.Format("UPDATE {0} SET {1}=0 WHERE {2} NOT IN(SELECT DISTINCT {3} FROM {0} WHERE {3} IN (SELECT {3} FROM {0} WHERE {2}=@FID) AND {2}<>@FID) AND {2} IN (SELECT {3} FROM {0} WHERE {2}=@FID)", TableName, IsParentField, CodeValueField, ParentIdField);
                    paramters = new SqlParameter[]
                        {
                            new SqlParameter("@FID", id)
                        };
                    DbContext.RegisterSqlCommand(sql, paramters);
                }
                //移动
                var idParams = new List<string>();
                var listParamter = new List<SqlParameter>();
                for (int i = 0; i < ids.Length; i++)
                {
                    idParams.Add("@FID" + i.ToString());
                    listParamter.Add(new SqlParameter("@FID" + i.ToString(), ids[i]));
                }
                listParamter.Add(new SqlParameter("@ParentID", targetNodeId));
                sql = string.Format("UPDATE {0} SET {1}=@ParentID WHERE {2} IN ({3})", TableName, ParentIdField, CodeValueField, string.Join<string>(",", idParams));
                DbContext.RegisterSqlCommand(sql, listParamter.ToArray());
                DbContext.Submit();
                return (new JsResponseResult<int> { ActionType = JsActionType.Alert, Content = "移动成功", Obj = 1 }).ToString();
            }
            catch
            {
                return (new JsResponseResult<int> { ActionType = JsActionType.Alert, Content = "移动失败", Obj = 1 }).ToString();
            }
        }
    }
}
