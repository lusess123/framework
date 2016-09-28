using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ataw.Framework.Core
{
    [CodePlug(REG_NAME, BaseClass = typeof(IListDataTable),
       CreateDate = "2012-10-5", Author = "zhengyk", Description = "插件数据源")]
    public class PlugInfoSource : ListDataTable
    {
        public const string REG_NAME = "PlugData";

        public PlugInfoSource()
        {
            ObjectType = typeof(PlugModel);
            PrimaryKey = "RegName";
            this.GroupColList = new List<string>();
            this.GroupColList.Add("DllPath");
            this.GroupColList.Add("BassClassName");
        }



        private IEnumerable<ObjectData> fList;

        private IEnumerable<PlugInModel> WhereFilter(IEnumerable<PlugInModel> source, DataRow whereRow)
        {
            source = source.SourceRowWhere(whereRow, "RegName", a => a.Key);
            source = source.SourceRowWhere(whereRow, "BassClassName", a => a.BaseType.Name, SysDataType.None);
            source = source.SourceRowWhere(whereRow, "Desc", a => a.Description);
            source = source.SourceRowWhere(whereRow, "DllPath", a => a.InstanceType.Module.ScopeName);
            source = source.SourceRowWhere(whereRow, "InstanceClassName", a => a.InstanceType.Name);
            source = source.SourceRowWhere(whereRow, "Author", a => a.Author, SysDataType.None);
            source = source.SourceRowWhere(whereRow, "CreateDate", a => a.CreateDate, SysDataType.DateTime);
            source = source.SourceRowWhere(whereRow, "CreateDate_END", a => a.CreateDate, SysDataType.DateTime);
            return source;

        }

        private IEnumerable<ObjectData> GetList()
        {
            var Ioc = AtawIocContext.Current;
            List<PlugModel> list = new List<PlugModel>();
            var sourceList = Ioc.PlugInModelList.ToList();

            // var 
            if (KeyValues != null && KeyValues.Count() > 0 )
            {
                sourceList = sourceList.Where(a => (KeyValues.ToList().Contains(a.Key))).ToList();
            }
            if (!ForeignKeyValue.IsEmpty())
            {

            }
            string _tableName = this.RegName + "_SEARCH";
            if (PostDataSet != null && PostDataSet.Tables[_tableName] != null && PostDataSet.Tables[_tableName].Rows.Count > 0)
            {
                DataRow row = PostDataSet.Tables[_tableName].Rows[0];
                sourceList = WhereFilter(sourceList, row).ToList();
            }

            List<PlugInModel> regList = new List<PlugInModel>();
            if (Pagination != null)
            {

                regList = sourceList.Skip((Pagination.PageIndex) * Pagination.PageSize).Take(Pagination.PageSize).ToList();
            }
            else
            {
                Pagination = new Pagination().FormDataTable(PostDataSet.Tables["PAGER"]);
                //Pagination.PageSize = 
                //regList = source.ToList();
            }
            Pagination.TotalCount = sourceList.Count;
            int i = 0;
            regList.ForEach((a) =>
           {
              
               list.Add(
                   new PlugModel()
                   {
                       RegName = a.Key+ i .ToString() ,
                       BassClassName = a.BaseType.Name,
                       Desc = a.Description,
                       DllPath = a.InstanceType.Module.ScopeName,
                       InstanceClassName = a.InstanceType.Name,
                       Author = a.Author,
                       CreateDate = a.CreateDate.Value<string>()
                   }
                   );
               i++;
           });

            return list.OrderBy(a => a.DllPath).ThenBy(a=>a.BassClassName).ThenBy(a=>a.Author);
        }

        public override List<string> GroupColList
        {
            get;
            set;
        }
        

        public override IEnumerable<ObjectData> List
        {
            get
            {
                if (fList == null)
                {
                    fList = GetList();
                }
                return fList;
            }
            set
            {
                fList = value;
            }
        }

        public override string RegName
        {
            get { return REG_NAME; }
        }


        public override string PrimaryKey
        {
            get;
            set;
            // get { return "RegName"; }
        }

        public override Type ObjectType
        {
            get;
            set;
        }

        public override void AppendTo(DataSet ds)
        {
            base.AppendTo(ds);
            this.Pagination.AppendToDataSet(ds, RegName);


        }

        public override string GetInsertKey(ObjectData data)
        {
            throw new NotImplementedException();
        }
    }


}
