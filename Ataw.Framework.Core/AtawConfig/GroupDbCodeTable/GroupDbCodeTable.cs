namespace Ataw.Framework.Core
{
    public class GroupDbCodeTable : SingleCodeTable<CodeDataModel>
    {
        public GroupDbCodeTable(GroupDbCodeTableXml xml)
        {
            Xml = xml;
            TableName = xml.TableName;
            TextField = xml.TextField;
            ValueField = xml.ValueField;
            RegNameField = xml.RegNameField;
            Where = "{0} = '{1}'".AkFormat(RegNameField, RegName);
        }

        public GroupDbCodeTableXml Xml
        {
            get;
            set;
        }

        public string RegNameField
        {
            get;
            set;
        }

        public override string RegName
        {
            get;
            set;
        }

        public override string TableName
        {
            get;
            set;
        }

        public override string TextField
        {
            get;
            set;
        }

        public override string ValueField
        {
            get;
            set;
        }
    }
}
