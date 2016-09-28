using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace Ataw.Framework.Core
{
    [CodePlug("AreaDataTreeCodeTable", BaseClass = typeof(CodeTable<CodeDataModel>),
    CreateDate = "2012-11-5", Author = "", Description = "部门树数据源")]
    public class AreaDataTreeCodeTable : TreeCodeTable
    {
        public AreaDataDbContext DbContext { get; set; }

        public AreaDataTreeCodeTable()
        {
            DbContext = new AreaDataDbContext(AtawAppContext.Current.DefaultConnString);
        }

        public override TreeCodeTableModel GetDisplayTreeNode(string key)
        {
            TreeCodeTableModel result = new TreeCodeTableModel();
            List<CodeDataModel> list = new List<CodeDataModel>();
            if (key == "0")
            {
                list = DbContext.TreeCodeTable.Where(a => a.CODE_VALUE.EndsWith("000"))
                    // .Where(a => a.CODE_VALUE.StartsWith(key))
                   .Select(a => new CodeDataModel()
                   {
                       CODE_VALUE = a.CODE_VALUE,
                       CODE_TEXT = a.CODE_NAME

                   }).ToList();
                list.Add(new CodeDataModel() { CODE_TEXT = "所有", CODE_VALUE = "0" });
            }
            var root = list.Where(a => a.CODE_VALUE == key).FirstOrDefault();
            if (root != null)
            {
                result.CODE_TEXT = root.CODE_TEXT;
                result.CODE_VALUE = root.CODE_VALUE;
                if (list.Count > 1)
                {
                    result.isParent = true;
                    result.IsLeaf = false;
                    result.nocheck = OnlyLeafCheckbox ? result.isParent.Value<bool?>() : null;
                    //result.IsSelect = false;
                    result.Children = new List<TreeCodeTableModel>();
                }
                list.ForEach(a =>
                {
                    if (a != root)
                    {
                        ((List<TreeCodeTableModel>)result.Children).Add(
                            new TreeCodeTableModel()
                            {
                                CODE_VALUE = a.CODE_VALUE,
                                CODE_TEXT = a.CODE_TEXT,
                                isParent = true
                            }
                            );
                    }
                });
            }

            return result;
        }

        public override TreeCodeTableModel GetChildrenNode(string key)
        {
            TreeCodeTableModel result = new TreeCodeTableModel();
            List<CodeDataModel> list = new List<CodeDataModel>();
            if (key == "0")
            {
                list = DbContext.TreeCodeTable.Where(a => a.CODE_VALUE.EndsWith("000"))
                    //.Where(a => a.CODE_VALUE.StartsWith(key))
                   .Select(a => new CodeDataModel()
                   {
                       CODE_VALUE = a.CODE_VALUE,
                       CODE_TEXT = a.CODE_NAME
                   }).ToList();
            }
            else
            {
                string keySub = key.Substring(0, 3);
                list = DbContext.TreeCodeTable
                   .Where(a => a.CODE_VALUE.StartsWith(keySub))
                   .Select(a => new CodeDataModel()
               {
                   CODE_VALUE = a.CODE_VALUE,
                   CODE_TEXT = a.CODE_NAME
               }).ToList();
            }
            var root = list.Where(a => a.CODE_VALUE == key).FirstOrDefault();
            if (root != null)
            {
                result.CODE_TEXT = root.CODE_TEXT;
                result.CODE_VALUE = root.CODE_VALUE;
                if (list.Count > 1)
                {
                    result.isParent = true;
                    result.IsLeaf = false;
                    result.nocheck = OnlyLeafCheckbox ? true.Value<bool?>() : null;
                    //result.IsSelect = false;
                    result.Children = new List<TreeCodeTableModel>();
                }
                list.ForEach(a =>
                {
                    if (a != root)
                    {
                        ((List<TreeCodeTableModel>)result.Children).Add(
                            new TreeCodeTableModel()
                            {
                                CODE_VALUE = a.CODE_VALUE,
                                CODE_TEXT = a.CODE_TEXT
                            }
                            );
                    }
                });
            }

            return result;
        }

        public override CodeDataModel this[string key]
        {
            get
            {
                var bean = DbContext.TreeCodeTable.Where(a => a.CODE_VALUE == key).First();
                return new CodeDataModel()
                {
                    CODE_TEXT = bean.CODE_NAME,
                    CODE_VALUE = bean.CODE_VALUE
                };
                //throw new System.NotImplementedException(); 

            }
        }

        public override IEnumerable<CodeDataModel> FillData(DataSet postDataSet)
        {
            return DbContext.TreeCodeTable.Take(10).Select(a => new CodeDataModel()
            {
                CODE_VALUE = a.CODE_VALUE,
                CODE_TEXT = a.CODE_NAME
            }).ToList();
        }

        //public override IEnumerable<CodeDataModel> FillData(DataSet postDataSet, Pagination pagination)
        //{
        //    return null;
        //}

        public override IEnumerable<CodeDataModel> BeginSearch(DataSet postDataSet, string key)
        {
            return DbContext.TreeCodeTable.Where(a => a.CODE_NAME.StartsWith(key))
                 .Select(a => new CodeDataModel()
                 {
                     CODE_VALUE = a.CODE_VALUE,
                     CODE_TEXT = a.CODE_NAME
                 }).ToList();
        }

        //public override IEnumerable<CodeDataModel> BeginSearch(DataSet postDataSet, string key, Pagination pagination)
        //{
        //    return null;
        //}

        public override IEnumerable<CodeDataModel> Search(DataSet postDataSet, string key)
        {
            return DbContext.TreeCodeTable.Where(a => a.CODE_NAME.Contains(key))
                .Select(a => new CodeDataModel()
            {
                CODE_VALUE = a.CODE_VALUE,
                CODE_TEXT = a.CODE_NAME
            }).ToList();
        }

        //public override IEnumerable<CodeDataModel> Search(DataSet postDataSet, string key, Pagination pagination)
        //{
        //    return null;
        //}

        public override IEnumerable<CodeDataModel> FillAllData(DataSet postDataSet)
        {
            return DbContext.TreeCodeTable.Select(a => new CodeDataModel()
             {
                 CODE_VALUE = a.CODE_VALUE,
                 CODE_TEXT = a.CODE_NAME
             }).ToList();
        }

        public override TreeCodeTableModel GetAllTree()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<CodeDataModel> GetDescendent(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}
