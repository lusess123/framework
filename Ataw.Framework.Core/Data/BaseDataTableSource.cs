using Ataw.Framework.Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
namespace Ataw.Framework.Core
{
    [CodePlug(REG_NAME, BaseClass = typeof(IListDataTable),
      CreateDate = "2012-11-30", Author = "zhengyk", Description = "数据库表访问数据源")]
    public abstract class BaseDataTableSource : ListDataTable
    {
        public const string REG_NAME = "DataTableSource";
        public const string PAGE_SQL = "select * from ( "
                                     + "select row_number() over(order by {1} ) rn,{0} from {2} WHERE 1=1 {3})"
                                     + "tb where rn >(@PageNo)*@pageSize and rn <=(@PageNo + 1)*@pageSize";

        public const string NUM_SQL = "WITH t AS"+
"("+
 "SELECT ROW_NUMBER() OVER(ORDER BY {0}) rowNumber, FID "+
 "FROM {1} "+
") SELECT t1.*,t2.* FROM t,t AS t1,t AS t2 WHERE "+
"t.FID=@fid"+
 "AND t1.rowNumber = t.rowNumber-1 AND t2.rowNumber = t.rowNumber+1";

        private string fRegName;
        private string fPrimaryKey;
        private IDictionary PageItems;
        private IUnitOfData fDbContext;

        public BaseDataTableSource()
        {
            PageItems = AtawAppContext.Current.PageFlyweight.PageItems;
            //if (!DataXmlPath.IsEmpty())
            //    DataFormConfig = DataXmlPath.PlugInPageGet<DataFormConfig>();
        }
        #region Unique


        private bool HasOnlyUniqueColumns(DataRow row, List<ObjectDataView> updateDataViewList)
        {
            string _key = row[PrimaryKey].ToString();
            var _odv = updateDataViewList.FirstOrDefault(a => a.KeyId == _key);
            if (_odv != null)
            {
                var _colColumns = GetModifyColumnName(_odv.objectData);
                _colColumns.ForEach(a =>
                {
                    row[a] = _odv.objectData.Row[a];
                });

                return true;
                //_odv.objectData.MODEFY_COLUMNS.Contains(
            }
            return false;

        }

        private List<string> GetModifyColumnName(ObjectData od)
        {
            List<string> strs = new List<string>();
            foreach (string str in od.MODEFY_COLUMNS)
            {
                var _str = UniqueList.FirstOrDefault(a => a == str);
                if (!_str.IsEmpty())
                {
                    strs.Add(_str);
                }
            }
            // AtawDebug.ThrowImpossibleCode(this);
            return strs;
        }

        private bool HasAllUniqueColumns(ObjectData od)
        {
            //----------
            foreach (string uniStr in UniqueList)
            {
                bool _is = od.MODEFY_COLUMNS.Contains(uniStr);
                // bool _isB = od.MODEFY_COLUMNS.Contains(uniStr);
                // if (_is && _isB) return true;
                if (!_is) return false;
            }
            return true;
        }

        private List<string> GetColumnTexts(DataRow row)
        {
            return UniqueList.Select(a =>
            {
                //string _str = row[a].ToString();
                var _col = DataFormConfig.Columns.FirstOrDefault(b => b.Name == a);
                // if (_col)
                return _col.GetValue(row);
                // return _str;
            }
                ).ToList();
        }

        private List<string> GetColumnValues(DataRow row)
        {
            return UniqueList.Select(a =>
            {
                string _str = row[a].ToString();
                //var _col = DataFormConfig.Columns.FirstOrDefault(b => b.Name == a);
                // if(_col)
                //  return _col.GetValue(row);
                return _str;
            }
                ).ToList();
        }

        private string fColumnDisPlayName;


        private string GetColumnDisPlayName()
        {
            if (fColumnDisPlayName.IsEmpty())
            {
                var _l = DataFormConfig.Columns.FindAll(a => UniqueList.Contains(a.Name)).Select(a => a.DisplayName).ToList();
                fColumnDisPlayName = String.Join(", ", _l);
            }
            return fColumnDisPlayName;

        }

        // private List<string> GetList

        public virtual string CheckUniqueFilterSql
        {
            get;
            set;
        }

        public void CheckUnique(List<ObjectDataView> insertDataViewList, List<ObjectDataView> updateDataViewList, List<string> deleteStringList)
        {
            string _columSelect = String.Join(",", UniqueList);
            // var _updateObjs = updateDataViewList.FindAll(a => HasAllUniqueColumns(a.objectData));
            // var _updateIds = updateDataViewList.Select(a => a.KeyId);

            // deleteStringList.AddRange(_updateIds);
            if (CheckUniqueFilterSql.IsEmpty())
            {
                CheckUniqueFilterSql = " 1 = 1";
            }
            string _strSql = "SELECT {0},{1} FROM {2} WHERE ({3}) ".AkFormat(PrimaryKey, _columSelect, RegName, CheckUniqueFilterSql);
            var _sqlCmd = SqlUtil.SqlByNotAnd(_strSql, PrimaryKey, deleteStringList);
            DataSet ds = _sqlCmd.QueryDataSet(DbContext);
            UniqueRowArrange list = new UniqueRowArrange();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                HasOnlyUniqueColumns(row, updateDataViewList);
                //-----------
                var _isCheck = list.CheckInsert(new UniqueRow(GetColumnValues(row).ToArray()));
                if (!_isCheck)
                {
                    string str = "  {0}  : {1} ".AkFormat(GetColumnDisPlayName(), String.Join(",", GetColumnTexts(row)));
                    ThrowUniError(str);
                }
            }
            //--------------xxxxxxxx---------------------------
            //  insertDataViewList.


            insertDataViewList.ForEach(a =>
            {
                if (!ForeignKeyValue.IsEmpty() && UniqueList.Contains(ForeignKey))
                {
                    if (!a.objectData.Row.Table.Columns.Contains(ForeignKey))
                    {
                        a.objectData.Row.Table.Columns.Add(ForeignKey);
                    }
                    a.objectData.Row[ForeignKey] = ForeignKeyValue;
                }
                var _isCheck = list.CheckInsert(new UniqueRow(GetColumnValues(a.objectData.Row).ToArray()));
                if (!_isCheck)
                {
                    string str = "  {0}  : {1} ".AkFormat(GetColumnDisPlayName(), String.Join(",", GetColumnTexts(a.objectData.Row)));
                    ThrowUniError(str);
                    //ThrowUniError(GetColumnDisPlayName() + ":" + String.Join(",", GetColumnTexts(a.objectData.Row)));
                }
            });

        }

        public override bool CheckPostData(List<ObjectDataView> insertDataViewList, List<ObjectDataView> updateDataViewList, List<string> deleteStringList)
        {
            CheckServerLegalofInsert(insertDataViewList); /* add by zgl */
            CheckServerLegalofUpdate(updateDataViewList); /* add by zgl */
            if (UniqueList != null && UniqueList.Count() > 0)
                CheckUnique(insertDataViewList, updateDataViewList, deleteStringList);

            return base.CheckPostData(insertDataViewList, updateDataViewList, deleteStringList);
        }

        /* add by zgl */
        public virtual void CheckServerLegalofInsert(List<ObjectDataView> dataViewlist)
        {
            dataViewlist.ForEach(a =>
            {
                //musterofControlLegal集合XML中配置ControlLegal的字段 
                List<string> musterofControlLegal = new List<string>();
                foreach (var kind in DataFormConfig.Columns)
                {
                    if (kind.ControlLegal != null && kind.ControlLegal.Kind.ToString() != "custom" && kind.ControlLegal.Kind.ToString() != "ExpressionLegal")
                    {
                        musterofControlLegal.Add(kind.Name);
                    }
                }
                //验证musterofControlLegal对应的更新数据  
                //包括xml中配置有ControlLegal，但是未在提交数据中的字段
                foreach (string col in musterofControlLegal)
                {
                    var colConfig = DataFormConfig.Columns.FirstOrDefault(c => c.Name == col);
                    if (a.objectData.MODEFY_COLUMNS.Contains(col))
                    {
                        var errorMessage = colConfig.ControlLegal.ErrMsg;
                        if (colConfig.ControlLegal.Kind != LegalKind.ExpressionLegal)
                        {
                            var legal = colConfig.ControlLegal.Kind.ToString().PlugGet<IServerLegal>();
                            var obj = legal.CheckLegal(colConfig, a.objectData);
                            if (!obj.IsLegal)
                            {
                                if (errorMessage == null)
                                {
                                    throw new AtawLegalException(obj.ErrorMessage);
                                }
                                else
                                {
                                    throw new AtawLegalException(errorMessage);
                                }
                            }
                        }
                    }
                    //验证提交数据中没有的字段
                    else
                    {
                        var errorMessage = colConfig.ControlLegal.ErrMsg;
                        if (colConfig.ControlLegal.Kind != LegalKind.ExpressionLegal)
                        {
                            var legal = colConfig.ControlLegal.Kind.ToString().PlugGet<IServerLegal>();
                            //虚字段的提交验证
                            if (!colConfig.SourceName.IsEmpty())
                                a.objectData.SetDataRowValue(colConfig.Name, a.objectData.Row[colConfig.Name].ToString());
                            else
                                a.objectData.SetDataRowValue(colConfig.Name, null);
                            var obj = legal.CheckLegal(colConfig, a.objectData);
                            if (!obj.IsLegal)
                            {
                                if (errorMessage == null)
                                {
                                    throw new AtawLegalException(obj.ErrorMessage);
                                }
                                else
                                {
                                    throw new AtawLegalException(errorMessage);
                                }
                            }
                        }
                    }

                }
            });
        }

        public virtual void CheckServerLegalofUpdate(List<ObjectDataView> dataViewlist)
        {
            dataViewlist.ForEach(a =>
            {
                //musterofControlLegal集合XML中配置ControlLegal的字段 * add by zgl
                List<string> musterofControlLegal = new List<string>();
                foreach (var kind in DataFormConfig.Columns)
                {
                    if (kind.ControlLegal != null && kind.ControlLegal.Kind.ToString() != "custom" && kind.ControlLegal.Kind.ToString() != "ExpressionLegal")
                    {
                        musterofControlLegal.Add(kind.Name);
                    }
                }
                //验证musterofControlLegal对应的更新数据  * add by zgl
                //只验证提交数据和musterofControlLegal都有的字段
                foreach (string col in musterofControlLegal)
                {
                    var colConfig = DataFormConfig.Columns.FirstOrDefault(c => c.Name == col);
                    if (a.objectData.MODEFY_COLUMNS.Contains(col))
                    {
                        var errorMessage = colConfig.ControlLegal.ErrMsg;
                        var legal = colConfig.ControlLegal.Kind.ToString().PlugGet<IServerLegal>();
                        var obj = legal.CheckLegal(colConfig, a.objectData);
                        if (!obj.IsLegal)
                        {
                            if (errorMessage == null)
                            {
                                throw new AtawLegalException(obj.ErrorMessage);
                            }
                            else
                            {
                                throw new AtawLegalException(errorMessage);
                            }
                        }
                    }

                }
            });
        }

        private void ThrowUniError(string name)
        {
            throw new AtawException("{0}已经存在".AkFormat(name), this);
        }

        #endregion


      


        protected virtual string SetSelectTable(string tableName)
        {
            return tableName;         
        }

        public virtual   IUnitOfData DbContext
        {
            get
            {
                if (fDbContext == null)
                {
                    var app = AtawAppContext.Current;
                    if (DataBase != null)
                    {
                        fDbContext = DataBase.FetchUnitofData();
                    }
                    else
                    {
                        fDbContext = app.UnitOfData;
                    }


                }

                return fDbContext;


            }
        }

        public override Type ObjectType
        {
            get;
            set;
        }

        public override string RegName
        {
            get { return fRegName; }
        }

        public override string PrimaryKey
        {
            get { return fPrimaryKey; }
            set { fPrimaryKey = value; }
        }

        protected DataSet DataSet
        {
            get;
            set;
        }

        private List<ObjectData> fList;

        protected virtual ObjectData ForeachGetList(ObjectData od)
        {
            return od;
        }

        public override IEnumerable<ObjectData> List
        {
            get
            {
                if (fList == null)
                {
                    InitializeDataSet();
                    fList = new List<ObjectData>();
                    DataTable dt = DataSet.Tables["Table"];
                    dt.TableName = RegName;
                    var rows = dt.Rows;
                    // Pagination.TotalCount = rows.Count;
                    for (int i = 0; i < rows.Count; i++)
                    {
                        ObjectData od = new ObjectData();
                        od.Row = rows[i];
                        fList.Add(od);
                    }
                    foreach (var od in fList)
                        ForeachGetList(od);

                }
                if (fList == null)
                    fList = new List<ObjectData>();
                return fList;
            }
            set
            {
                fList = value.ToList();
                //base.List = value;
            }
        }
        private string WhereNaviStringBuilder(ColumnConfig column, List<SqlParameter> sqlList, string val)
        {
            if (string.IsNullOrEmpty(val)) return string.Empty;
            var naviControlType = column.Navigation.ControlType;
            if (naviControlType == default(ControlType))
            {
                switch (column.ControlType)
                {
                    case ControlType.CheckBox:
                        naviControlType = ControlType.CheckBoxNavi;
                        break;
                    case ControlType.Combo:
                    case ControlType.Radio:
                        naviControlType = ControlType.RadioNavi;
                        break;
                    case ControlType.TreeSingleSelector:
                        naviControlType = ControlType.TreeSingleNavi;
                        break;
                    case ControlType.TreeMultiSelector:
                        naviControlType = ControlType.TreeMultiNavi;
                        break;
                }
            }
            string navColName = column.SourceName.IsEmpty() ? column.Name : column.SourceName;
            switch (naviControlType)
            {
                case ControlType.CheckBoxNavi:


                    string[] _valCheckBox = val.Split(',');
                   // List<string> paramCheckBox = new List<string>();
                    string _sql = "1=2";
                    for (int i = 0; i < _valCheckBox.Length; i++)
                    {
                        //string _paramName = navColName + i.ToString();
                        //paramCheckBox.Add("@{0}{1}".AkFormat(seachColName, i.ToString()));
                       

                        if (column.ControlType == ControlType.CheckBox || column.ControlType == ControlType.MultiSelector || column.ControlType == ControlType.MultiSelector)
                        {
                            sqlList.Add(new SqlParameter("@{0}{1}".AkFormat(navColName, i.ToString()), "%{0}%".AkFormat(_valCheckBox[i])));
                            _sql = " {2} OR  {0} like  @{0}{1} ".AkFormat(navColName, i.ToString(), _sql);
                        }
                        else
                        {
                            _valCheckBox[i] = _valCheckBox[i].Replace("\"","");
                            sqlList.Add(new SqlParameter("@{0}{1}".AkFormat(navColName, i.ToString()), "{0}".AkFormat(_valCheckBox[i])));
                            _sql = " {2} OR  {0} = @{0}{1} ".AkFormat(navColName, i.ToString(), _sql);
                           
                        }
                    }
                    //return string.Format(ObjectUtil.SysCulture, " AND {0} IN ({1})", seachColName, String.Join<string>(",", paramCheckBox));
                    return  " AND ({0})".AkFormat( _sql);

                   // break;
                case ControlType.RadioNavi:
              
                case ControlType.NaviFilter:
                case ControlType.TreeMultiNavi:
                    string[] _val = val.Split(',');
                    List<string> param = new List<string>();
                    for (int i = 0; i < _val.Length; i++)
                    {
                        param.Add("@{0}{1}".AkFormat(column.Name, i.ToString()));
                        sqlList.Add(new SqlParameter(string.Format("@{0}", navColName + i.ToString()), _val[i]));
                    }
                    return string.Format(ObjectUtil.SysCulture, " AND {0} IN ({1})", navColName, String.Join<string>(",", param));
                case ControlType.SingleRadioNavi:
                    if (val == "1")
                    {
                        sqlList.Add(new SqlParameter(string.Format("@{0}", column.Name), true));
                        return string.Format(ObjectUtil.SysCulture, " AND {0} = @{0}", column.Name);
                    }
                    else if (val == "0")
                    {
                        sqlList.Add(new SqlParameter(string.Format("@{0}", column.Name), false));
                        return string.Format(ObjectUtil.SysCulture, " AND ({0} = @{0} or {0} is null)", column.Name);
                    }
                    return string.Empty;
                case ControlType.TreeSingleNavi:
                    var ids = val.Split(',').ToList();
                    if (column.Selector != null && column.Selector.Descendant)
                    {
                        string NaviRegName = column.Navigation.RegName;
                        if (NaviRegName == null)
                        {
                            throw new Exception("请检查RegName");
                        }
                        var treeCodeTable = NaviRegName.CodePlugIn<CodeTable<CodeDataModel>>() as TreeCodeTable;
                        var _list = treeCodeTable.GetDescendent(val);
                        ids = _list.Select(a => a.CODE_VALUE).ToList();
                        ids.Add(val);
                    }
                    List<string> treeSingleNaviParam = new List<string>();
                    for (int i = 0; i < ids.Count; i++)
                    {
                        treeSingleNaviParam.Add("@{0}{1}".AkFormat(navColName, i.ToString()));
                        sqlList.Add(new SqlParameter(string.Format("@{0}", navColName + i.ToString()), ids[i]));
                    }
                    return string.Format(ObjectUtil.SysCulture, " AND {0} IN ({1})", navColName, String.Join<string>(",", treeSingleNaviParam));
                default:
                    return string.Empty;
            }
        }
        private string WhereSearchStringBuilder(ColumnConfig column, List<SqlParameter> sqlList, string val, bool isEndTime)
        {
            // if (column.Search == null) return string.Empty;
            if (column.Navigation != null && column.Navigation.IsAvailable) return string.Empty;

            string seachColName = column.SourceName.IsEmpty() ? column.Name : column.SourceName;
            ControlType searchControlType = column.ControlType;
            if(column.Search != null && column.Search.ControlType != ControlType.None ){
                searchControlType = column.Search.ControlType;
            }
                //()&& (column.Search.ControlType != null)
            switch (searchControlType)
            {

                case ControlType.Selector:
                    //如果配置了Search的islike是true 
                    if (column.Search.IsLike == true )
                    {
                        var rN = column.RegName;
                        DataSet dataSet = null;
                        if (column.DetailRegName != null && !column.DetailRegName.Equals(""))
                        {
                            rN = column.DetailRegName;
                        }
                        //根据配置的column.RegName和输入的关键字val查询
                        var dt = AtawIocContext.Current.FetchInstance<CodeTable<CodeDataModel>>(rN);
                        string sql = "";
                        if ((dt as SingleCodeTable<CodeDataModel>) != null && (dt as SingleCodeTable<CodeDataModel>).TableName != null && (dt as SingleCodeTable<CodeDataModel>).TextField != null && (dt as SingleCodeTable<CodeDataModel>).ValueField != null)
                        {
                            sql = string.Format("select {0} from {1} where {2} like '%{3}%'", (dt as SingleCodeTable<CodeDataModel>).ValueField, (dt as SingleCodeTable<CodeDataModel>).TableName, (dt as SingleCodeTable<CodeDataModel>).TextField, val);
                            if ((dt as SingleCodeTable<CodeDataModel>).Where != null && !(dt as SingleCodeTable<CodeDataModel>).Where.Equals(""))
                            {
                                sql = string.Format("{0} and {1}", sql, (dt as SingleCodeTable<CodeDataModel>).Where);

                                dataSet = DbContext.QueryDataSet(sql);

                            }
                        }


                        //var codemodels = dt.Search(null, val);
                        //if (codemodels != null && codemodels.Count() != 0)
                        if (dataSet != null && dataSet.Tables[0] != null)
                        {
                            //var searchVal = codemodels.Select(m => m.CODE_VALUE).ToList();
                            //List<string> selectorParam = new List<string>();
                            //for (int i = 0; i < searchVal.Count; i++)
                            //{
                            //    selectorParam.Add("@" + seachColName + i.ToString());
                            //    sqlList.Add(new SqlParameter(string.Format("@{0}", seachColName + i.ToString()), searchVal[i]));
                            //}
                            string myFid = "";
                            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                            {
                                myFid = "{0}'{1}',".AkFormat(myFid, dataSet.Tables[0].Rows[i][0]);
                                //myFid += "'" + dataSet.Tables[0].Rows[i][0] + "',";
                            }
                            if (!myFid.IsAkEmpty())
                            {
                                myFid = myFid.Substring(0, myFid.Length - 1);
                                return string.Format(ObjectUtil.SysCulture, " AND {0} IN ({1})", seachColName, myFid);
                            }
                            else
                            {
                                return " AND 1=2 ";
                            }
                        }
                        else
                        {
                            string temp = "%{0}%".AkFormat(val);
                            sqlList.Add(new SqlParameter(string.Format("@{0}", seachColName), temp));
                            return string.Format(ObjectUtil.SysCulture, " AND {0} LIKE @{0}", seachColName);
                        }
                    }
                    else
                    {
                        sqlList.Add(new SqlParameter(string.Format("@{0}", seachColName), val));
                        return string.Format(ObjectUtil.SysCulture, " AND {0} = @{0}", seachColName);
                    }
                    
                case ControlType.Date:
                case ControlType.DateTime:
                    if (isEndTime)
                    {

                        if (searchControlType == ControlType.Date)
                        {
                            DateTime time = val.Value<DateTime>();
                            if (time != default(DateTime))
                            {
                                time = time.AddDays(1);
                                sqlList.Add(new SqlParameter(string.Format("@{0}{1}", seachColName, "_END"), time));
                            }
                        }
                        else
                        {
                            sqlList.Add(new SqlParameter(string.Format("@{0}{1}", seachColName, "_END"), val));//若有dataspan则时间参数会重复,故需加_END
                        }
                        return string.Format(ObjectUtil.SysCulture, " AND {0} < @{1}", seachColName, seachColName + "_END");
                    }
                    else
                    {
                        sqlList.Add(new SqlParameter(string.Format("@{0}", seachColName), val));
                        return string.Format(ObjectUtil.SysCulture, " AND {0} >= @{0}", seachColName);
                    }
                case ControlType.CheckBox:
                    string[] _valCheckBox = val.Split(',');
                   // List<string> paramCheckBox = new List<string>();
                    string _sql = " 1=1 ";
                    for (int i = 0; i < _valCheckBox.Length; i++)
                    {
                        string _paramName  =  seachColName + i.ToString();
                        //paramCheckBox.Add("@{0}{1}".AkFormat(seachColName, i.ToString()));
                        sqlList.Add(new SqlParameter("@"+_paramName, "%{0}%".AkFormat(_valCheckBox[i])));

                        _sql = " {2} AND  {0} LIKE @{0}{1} ".AkFormat(seachColName, i.ToString(),_sql);
                    }
                    //return string.Format(ObjectUtil.SysCulture, " AND {0} IN ({1})", seachColName, String.Join<string>(",", paramCheckBox));
                    return  " AND ({0})".AkFormat( _sql);
                case ControlType.Combo:
                case ControlType.Radio:
                case ControlType.TreeMultiSelector:
                    string[] _val = val.Split(',');
                    List<string> param = new List<string>();
                    for (int i = 0; i < _val.Length; i++)
                    {
                        param.Add("@" + seachColName + i.ToString());
                        sqlList.Add(new SqlParameter(string.Format("@{0}{1}", seachColName, i.ToString()), _val[i]));
                    }
                    return string.Format(ObjectUtil.SysCulture, " AND {0} IN ({1})", seachColName, String.Join<string>(",", param));
                case ControlType.TreeSingleSelector:
                    var strs = val.Split(',').ToList();
                    if (column.Selector != null && column.Selector.Descendant)
                    {
                        string _treeRegName = column.RegName;
                        if (_treeRegName == null)
                        {
                            throw new Exception("请检查RegName");
                        }
                        var _cd = _treeRegName.CodePlugIn<CodeTable<CodeDataModel>>() as TreeCodeTable;
                        var _list = _cd.GetDescendent(val);
                        strs = _list.Select(a => a.CODE_VALUE).ToList();
                        strs.Add(val);
                    }
                    List<string> treeSingleSelectorParam = new List<string>();
                    for (int i = 0; i < strs.Count; i++)
                    {
                        treeSingleSelectorParam.Add("@{0}{1}".AkFormat(seachColName, i.ToString()));
                        sqlList.Add(new SqlParameter(string.Format("@{0}{1}", seachColName, i.ToString()), strs[i]));
                    }
                    return string.Format(ObjectUtil.SysCulture, " AND {0} IN ({1})", seachColName, String.Join<string>(",", treeSingleSelectorParam));
                case ControlType.SingleCheckBox:
                    sqlList.Add(new SqlParameter(string.Format("@{0}", seachColName), val == "1" ? true : false));
                    if (val == "1")
                    {
                        return string.Format(ObjectUtil.SysCulture, " AND {0} = @{0}", seachColName);
                    }
                    else
                    {
                        return string.Format(ObjectUtil.SysCulture, " AND ({0} = @{0} or {0} is null)", seachColName);
                    }
               
                default:
                    if (column.Search != null && column.Search.IsLike == true )
                    {
                        string temp = "%{0}%".AkFormat(val);
                        sqlList.Add(new SqlParameter(string.Format("@{0}", seachColName), temp));
                        return string.Format(ObjectUtil.SysCulture, " AND {0} LIKE @{0}", seachColName);
                    }
                    else
                    {
                        sqlList.Add(new SqlParameter(string.Format("@{0}", seachColName), val));
                        return string.Format(ObjectUtil.SysCulture, " AND {0} = @{0}", seachColName);
                    }
                    //sqlList.Add(new SqlParameter(string.Format("@{0}", seachColName), val));
                    //return string.Format(ObjectUtil.SysCulture, " AND {0} = @{0}", seachColName);
            }
        }
        protected virtual string WhereStringBuilder(DataRow row, List<SqlParameter> sqlList)//增加参数sqlList，参数化查询
        {
            
            string res = "";
            var columns = row.Table.Columns;
            //预处理
            foreach (DataColumn col in columns)
            {
                string colName = col.ColumnName;
                bool isLike = colName.EndsWith("_END");
                //这种情况，控件肯定是时间控件这种的
                if (isLike)
                {
                    colName = colName.Replace("_END", "");
                    var _timeCol = DataFormConfig.Columns.FirstOrDefault(a => a.Name == colName);
                    if (_timeCol != null)
                    {
                        _timeCol.ControlType = ControlType.DateTime;
                    }
                }
            }


            foreach (DataColumn col in columns)
            {
                string val = row[col.ColumnName].ToString();
                if (!val.IsEmpty())
                {
                    string colName = col.ColumnName;
                    var column = DataFormConfig.Columns.FirstOrDefault(a => a.Name == colName);
                    bool isLike = colName.EndsWith("_LIKE");
                    bool isEndTime = false;
                    if (isLike && column == null)
                    {
                        colName = colName.Replace("_LIKE", "");

                    }
                    isEndTime = colName.EndsWith("_END");
                    if (isEndTime && column == null)
                    {
                        colName = colName.Replace("_END", "");
                        
                    }
                    column = DataFormConfig.Columns.FirstOrDefault(a => a.Name == colName);
                    if (column != null )
                    {
                        if (column.Kind == ColumnKind.Data)
                        {
                            if (column.Navigation != null && column.Navigation.IsAvailable)
                            {
                                res += WhereNaviStringBuilder(column, sqlList, val);
                            }
                            else
                            {
                                res += WhereSearchStringBuilder(column, sqlList, val, isEndTime);
                            }
                        }
                        else
                        {
                            res += WhereSearchStringBuilderByVisualData(column, sqlList, val, isEndTime);
                        }
                    }
                    
                }
            }
            return res;
        }
        protected virtual string  WhereSearchStringBuilderByVisualData(ColumnConfig column, List<SqlParameter> sqlList, string val, bool isEndTime)
        {
            return "";
        }
        private string SetWhereFilterSqlByFormConfig(string where)
        {
            MacroConfig mconfig = ModuleFormConfig.AndFilterSql;
            if (mconfig != null && !mconfig.Value.IsEmpty())
            {
                string _andSql = mconfig.ExeValue();
                where += " AND " + _andSql;
            }
            return where;
        }
        private void SetPrimaryKey()
        {
            if (PrimaryKey.IsEmpty())
            {
                var bean = DataFormConfig.Columns.FirstOrDefault(a => a.IsKey);
                AtawDebug.AssertNotNull(bean, string.Format(ObjectUtil.SysCulture, " 数据源{0}的 DataXML 必须配置主键", REG_NAME), this);
                PrimaryKey = bean.Name;

            }
        }


        protected virtual string SetSelectSql()
        {
            return string.Join(",",
                DataFormConfig.Columns.Where(b => b.Kind == Ataw.Framework.Core.ColumnKind.Data).
                Select(a => " {0} AS {1} ".AkFormat(a.SourceName.IsEmpty() ? a.Name : a.SourceName, a.Name)));
        }

        private List<SqlParameter> paraList;
        private string sql;

        protected virtual void InitializeDataSet()
        {

            SetSqlSelect();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Environment.NewLine);
            sb.AppendLine(sql);
            sb.AppendLine(Environment.NewLine);
            DBUtil.DbCommandToString(paraList.ToList(), sb);
            AtawTrace.WriteCustomFile("BaseDataTableSource", sb.ToString());

            DataSet = DbContext.QueryDataSet(sql, paraList.ToArray());
            //设置外键的值
            //this.ModuleFormConfig.

        }

        private void SetSqlSelect()
        {
            string where = "";
            List<SqlParameter> sqlList = new List<SqlParameter>();
            if (KeyValues.Count() == 0)
            {
                string searchTable = RegName + "_SEARCH";
                if (PostDataSet != null && PostDataSet.Tables[searchTable] != null && PostDataSet.Tables[searchTable].Rows.Count > 0)
                {
                    DataRow row = PostDataSet.Tables[searchTable].Rows[0];
                    where = WhereStringBuilder(row, sqlList);
                }
                else
                {
                    if (IsFillEmpty)
                    {
                        where = "  AND 1=2 ";
                    }
                }
            }
            else
            {
                //当外键也为空的时候
                if (ForeignKeyValue.IsEmpty())
                    where = SetWhereByKeyValue(sqlList);
                else
                {
                    where = string.Format(" AND {0}=@{0}", ForeignKey);
                    sqlList.Add(new SqlParameter(string.Format("@{0}", ForeignKey), ChangeForeignKeyValue(ForeignKeyValue)));//获取子表过滤条件
                }
            }
            where = SetWhereFilterSqlByFormConfig(where);
            where = AdditionalConditionSql(where);
            string countSql = string.Format(ObjectUtil.SysCulture, "SELECT COUNT(*) FROM {0} WHERE 1=1 {1}", SetSelectTable(RegName), where);
            Pagination.TotalCount = DbContext.QueryObject(countSql, sqlList.ToArray()).Value<int>();
            string _selectStr = SetSelectSql();
            string orderName = PrimaryKey + " DESC ";
            if (Order.IsEmpty())
            {
                if (XmlColumns.Contains("CREATE_TIME"))
                {
                    orderName = "CREATE_TIME DESC ";
                }
                if (XmlColumns.Contains("UPDATE_TIME"))
                {
                    orderName = "UPDATE_TIME DESC ";
                }
            }
            else
            {
                orderName = Order;
            }
            if (!Pagination.SortName.IsEmpty())
            {
                if (Pagination.IsASC)
                    orderName = Pagination.SortName + " ASC";
                else
                    orderName = Pagination.SortName + " DESC";
            }
            paraList = new List<SqlParameter>();

            foreach (var qsql in sqlList)//对象克隆，防止参数重复
            {
                SqlParameter sqlP = new SqlParameter();
                qsql.ObjectClone(sqlP);
                paraList.Add(sqlP);
            }
            sql = string.Format(ObjectUtil.SysCulture, PAGE_SQL, _selectStr, orderName, SetSelectTable( RegName), where);
            paraList.Add(new SqlParameter("@PageNo", SqlDbType.Int) { SqlValue = Pagination.PageIndex });
            paraList.Add(new SqlParameter("@pageSize", SqlDbType.Int) { SqlValue = Pagination.PageSize });
        }

        protected virtual string ChangeForeignKeyValue(string value)
        {
            //string __relationKey = "_foreignkey_{0}_{1}".AkFormat(_relation.MasterForm, _relation.MasterField);
            //AtawAppContext.Current.SetItem(__relationKey, _value);
            return value;
        }

        private string SetWhereByKeyValue(List<SqlParameter> sqlList)
        {
            string where = "AND (";
            int flag = 0;
            foreach (string val in KeyValues)
            {
                string parm = val.Replace("-", "_");
                flag++;
                if (flag == KeyValues.Count())
                {
                    where = where + string.Format(ObjectUtil.SysCulture, "{0} = @{1}", PrimaryKey, parm);
                }
                else
                {
                    where = where + string.Format(ObjectUtil.SysCulture, "{0} = @{1} OR ", PrimaryKey, parm);
                }
                sqlList.Add(new SqlParameter(string.Format("@{0}", parm), val));
            }
            where = where + ")";
            return where;
        }
        public override void Initialize(ModuleFormInfo info)
        {
            base.Initialize(info);
            // base.Initialize(dataSet, pageSize, keyValue, foreignKeyValue, tableName, primaryKey, foreignKey, isFillEmpty, dataXmlPath);
            XmlColumns = new HashSet<string>();
            DataFormConfig.Columns.ForEach(a =>
            {
                XmlColumns.Add(a.Name);
            });
            if (fRegName.IsEmpty())
            {
                //修复配置表制定名称的bug
                if (!info.ModuleFormConfig.TableName.IsEmpty())
                {
                    fRegName = info.ModuleFormConfig.TableName;
                }
                else
                {
                    fRegName = DataFormConfig.TableName;
                }
            }
            if (fPrimaryKey.IsEmpty())
            {
                fPrimaryKey = DataFormConfig.PrimaryKey;
            }
            SetPrimaryKey();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DataSet != null)
                    DataSet.Dispose();
            }
            base.Dispose(disposing);
        }
        public override void AppendTo(DataSet ds)
        {
            //base.AppendTo(ds);
            DataTable dt = DataSet.Tables[RegName];
            bool isRight = dt.Columns.Contains("BUTTON_RIGHT");
            if (!isRight)
            {
                dt.Columns.Add("BUTTON_RIGHT", typeof(string));
            }
            foreach (var obj in List)
            {
                obj.Row["BUTTON_RIGHT"] = obj.BUTTON_RIGHT;
            }
            if (!ds.Tables.Contains(dt.TableName))
            {
                ds.Tables.Add(dt.Copy());
            }
            else
                DataSetUtil.MegerDataTable(dt, ds.Tables[dt.TableName], PrimaryKey);
            this.Pagination.AppendToDataSet(ds, RegName);
        }
        public override void Merge(bool isBat)
        {
            // SetPrinaryKey();

            base.Merge(isBat);
            if (!isBat)
            {
                //int res = DbContext.ADOSubmit();
                //Result = new JsResponseResult<int>() { ActionType = JsActionType.Object, Obj = res };
            }
        }

        public override void InsertForeach(ObjectData data, DataRow row, string key)
        {
            // base.InsertForeach(data, row);
            data.SetDataRowValue("TIMESSTAMP", TimeUtil.GetTimesSpan());
            List<SqlParameter> sqlList = new List<SqlParameter>();
            string inserSql = CreateInsertSql(data, sqlList, key);
            if (!inserSql.IsEmpty())
            {
                DbContext.RegisterSqlCommand(inserSql, sqlList.ToArray());
            }
        }
           
        private void LegalRow(ObjectData data)
        {
            foreach (string str in data.MODEFY_COLUMNS)
            {
                // ColumnLegalHashTable.
            }
        }

        public override void UpdateForeach(ObjectData data, DataRow row, string key)
        {
            //base.UpdateForeach(data, row);
            if (data.MODEFY_COLUMNS.Count > 1)
            {
                if (ModuleFormConfig.IsSafeMode)
                {
                    string sql = string.Format(ObjectUtil.SysCulture, "SELECT TIMESSTAMP FROM {0} WHERE {1}=@FID", RegName, PrimaryKey);
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@FID",key)
                    };
                    var timesStamp = DbContext.QueryObject(sql, parameters);
                    var originTimeStamp = data.GetDataRowValue("TIMESSTAMP");
                    if (timesStamp != DBNull.Value && originTimeStamp != null)
                    {
                        if (timesStamp.ToString() != originTimeStamp.ToString())
                        {
                            throw new AtawLegalException("数据已发生变化,请刷新后重新更新");
                        }
                    }
                }
                data.SetDataRowValue("TIMESSTAMP", TimeUtil.GetTimesSpan());
                List<SqlParameter> sqlList = new List<SqlParameter>();
                string updateSql = CreateUpdateSql(data, sqlList);
                if (!updateSql.IsEmpty())
                {
                    DbContext.RegisterSqlCommand(updateSql, sqlList.ToArray());
                }
            }
        }

        public override void DeleteForeach(string key, string data)
        {
            List<SqlParameter> sqlList = new List<SqlParameter>();
            string sql = CreateDeleteSql(key, sqlList);
            if (!sql.IsEmpty())
            {
                DbContext.RegisterSqlCommand(sql, sqlList.ToArray());
            }
            //base.DeleteForeach(key, data);
        }

        protected virtual string CreatePhyDeleteSql(string key, List<SqlParameter> sqlList)
        {
            string sql = " DELETE  FROM {0} WHERE {1}=@key AND 1=1 ";

            sqlList.Add(new SqlParameter("@key", key));
            //sql = string.Format(ObjectUtil.SysCulture, sql, RegName, PrimaryKey, key);
            sql = string.Format(ObjectUtil.SysCulture, sql, RegName, PrimaryKey);

            return sql;
        }

        protected virtual string CreateDeleteSql(string key, List<SqlParameter> sqlList)
        {
            //string sql = string.Format(" DELETE  FROM {0} WHERE {1}=@key AND 1=1 ");
            return CreatePhyDeleteSql(key, sqlList);
        }

        protected virtual string CreateInsertSql(ObjectData od, List<SqlParameter> sqlList, string key)//增加参数sqlList，参数化查询
        {
            string paraName = "";
            HashSet<string> _modefyColumns = od.MODEFY_COLUMNS;
            if (!_modefyColumns.Contains(PrimaryKey))
            {
                _modefyColumns.Add(PrimaryKey);
            }
            //added by sj
            if (!ForeignKey.IsEmpty())
            {
                _modefyColumns.Add(ForeignKey);
            }


            string sql = "INSERT INTO {0} ({1}) VALUES ({2})";
            List<string> vals = new List<string>();
            List<string> colNames = new List<string>();
            bool hasPriKey = false;
            bool isNull = true;
            foreach (string col in _modefyColumns)
            {
                if (FullColumns.FirstOrDefault(a => a.Name == col && a.Kind == ColumnKind.Data) != null)
                {

                    string val = "";
                    if (col == PrimaryKey)
                    {
                        AtawDebug.AssertArgument(!hasPriKey, col, string.Format(ObjectUtil.SysCulture, "表{0}主键只能有一个", RegName), this);
                        hasPriKey = true;
                        val = key;
                        // FileID = "";
                        //added by sj 保存主表主键，当有它子表时作为外键
                        if (this.PostDataSet.Tables[PAGE_SYS] != null && ForeignKey.IsEmpty())
                        {
                            this.PostDataSet.Tables[PAGE_SYS].Rows[0][KEYVALUE] = val;
                        }
                    }
                    else if (col == ForeignKey)
                    {   
                        val = ForeignKeyValue;
                    }
                    else
                    {
                        val = od.Row[col].ToString();
                    }
                    paraName = "@" + col;
                    sqlList.Add(new SqlParameter(paraName, val));
                    vals.Add(paraName);
                    colNames.Add(col);
                    isNull = false;
                }

            }
            string cols = string.Join(",", colNames);

            foreach (ColumnConfig column in FullColumns.Where(a => a.Kind == ColumnKind.Data))
            {
                string mod = column.Name;
                if (!_modefyColumns.Contains(mod))
                {
                    if (mod == "CREATE_TIME" || mod == "UPDATE_TIME")
                    {
                        cols += "," + mod;
                        paraName = "@" + mod;
                        sqlList.Add(new SqlParameter(paraName, DbContext.Now.ToString()));
                        vals.Add(paraName);
                        isNull = false;
                    }

                    if (mod == "CREATE_ID" || mod == "UPDATE_ID")
                    {
                        cols += "," + mod;
                        paraName = "@" + mod;
                        sqlList.Add(new SqlParameter(paraName, AtawAppContext.Current.UserId));
                        vals.Add(paraName);
                        isNull = false;
                    }

                }
            }
            if (isNull)
                return "";
            sql = string.Format(ObjectUtil.SysCulture, sql, RegName, cols, string.Join(",", vals));
            return sql;
        }

        protected virtual string CreateUpdateSql(ObjectData od, List<SqlParameter> sqlList)//增加参数sqlList，参数化查询
        {
            //if(od.MODEFY_COLUMNS.)
            string cols = string.Join(",", od.MODEFY_COLUMNS);
            string sql = "UPDATE {0} SET {1} WHERE {2} = '{3}' ";
            List<string> vals = new List<string>();
            //List<string> colNames = new List<string>();
            bool isNull = true;
            string key = "";
            foreach (string col in od.MODEFY_COLUMNS)
            {
                if (FullColumns.FirstOrDefault(a => a.Name == col && a.Kind == ColumnKind.Data) != null)
                {
                    string val = od.Row[col].ToString();
                    if (col != PrimaryKey)
                    {
                        string temp = val;
                        val = string.Format(ObjectUtil.SysCulture, "{0} = @{0} ", col);
                        sqlList.Add(new SqlParameter(string.Format("@{0}", col), temp));
                        vals.Add(val);
                        //colNames.Add(col);
                        isNull = false;
                    }
                    else
                        key = val;

                }
            }

            foreach (ColumnConfig column in FullColumns.Where(a => a.Kind == ColumnKind.Data))
            {
                string mod = column.Name;
                if (!od.MODEFY_COLUMNS.Contains(mod))
                {
                    if (mod == "UPDATE_TIME")
                    {
                        string val2 = string.Format(ObjectUtil.SysCulture, " UPDATE_TIME  = @{0} ", mod);
                        sqlList.Add(new SqlParameter(string.Format("@{0}", mod), DbContext.Now.ToString()));
                        vals.Add(val2);
                        isNull = false;

                    }

                    if (mod == "UPDATE_ID")
                    {
                        string val2 = string.Format(ObjectUtil.SysCulture, " UPDATE_ID  = @{0} ", mod);
                        sqlList.Add(new SqlParameter(string.Format("@{0}", mod), AtawAppContext.Current.UserId));
                        vals.Add(val2);
                        isNull = false;
                    }

                }
            }
            if (isNull) return "";
            sql = string.Format(ObjectUtil.SysCulture, sql, RegName, string.Join(",", vals), PrimaryKey, key);
            return sql;
        }

        protected virtual string AdditionalConditionSql(string sql)
        {
            if ("IsSysAdditionalTypeFControlUnit".AppKv<bool>(false))
            {   
                if (this.DataFormConfig.TableName.Length > 2 && this.DataFormConfig.TableName.Substring(0, 3) != "WF_")
                    return "{0} AND FControlUnitID='{1}' AND ((ISDELETE IS NULL) OR (ISDELETE = 0))".AkFormat(sql, AtawAppContext.Current.FControlUnitID);
                else
                    return sql;
            }
            else
                return sql;
        }
        //private 


        public override string GetInsertKey(ObjectData data)
        {
            //throw new NotImplementedException();
            return DbContext.GetUniId();
        }

        public override NumModel GetNumModel()
        {
            return base.GetNumModel();
        }
    }
}
