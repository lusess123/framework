
using System.Data;
namespace Ataw.Framework.Core
{
    public abstract class BaseLegal
    {

        public ControlLegalConfig LegalConfig
        {
            private get;
            set;
        }

        public DataTable PostDataTable
        {
            get;
            private set;
        }

        public DataSet PostDataSet
        {
            get;
            private set;
        }

        public int RowIndex
        {
            get;
            private set;
        }

        public string ColumnName
        {
            get;
            private set;
        }

        public virtual string Error
        {
            get;
            private set;
        }

        public string GetErrorMesg()
        {
            return string.Format(ObjectUtil.SysCulture, "表{0}中，第{1}行的字段{2} {3}，不允许提交",
                PostDataTable.TableName, RowIndex, ColumnName, Error);
        }

        protected void SetError(string error)
        {
            Error = error;
        }

        internal void Initialize(DataSet postDataset, DataTable postDataTable, int rowIndex, string columnName, string error)
        {
            PostDataTable = postDataTable;
            PostDataSet = postDataset;
            RowIndex = rowIndex;
            ColumnName = columnName;
            if (!error.IsEmpty())
            {
                Error = error;
            }
        }

        protected abstract bool Legal(object value);

    }
}
