
namespace Ataw.Framework.Core
{
    public class GroupDbCodeTableXml : FileXmlConfigBase, IReadXmlCallback
    {
        public string TableName
        {
            get;
            set;
        }

        public string RegNameField
        {
            get;
            set;
        }

        public string TextField
        {
            get;
            set;
        }

        public string ValueField
        {
            get;
            set;
        }

        public bool HasCache
        {
            get;
            set;
        }

        public string Where
        {
            get;
            set;
        }


        public void OnReadXml()
        {
            AtawDebug.AssertNotNullOrEmpty(TableName, "表名不能为空", this);
            AtawDebug.AssertNotNullOrEmpty(RegNameField, "注册名字段不能为空", this);
            AtawDebug.AssertNotNullOrEmpty(TextField, "文本显示字段不能为空", this);
            AtawDebug.AssertNotNullOrEmpty(ValueField, "值字段不能为空", this);
            //throw new System.NotImplementedException();

        }
    }
}
