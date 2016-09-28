namespace Ataw.Framework.Core
{
    public class DataBaseConfig
    {
        public string Product
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }


        public string Conn
        {
            get;
            set;
        }
        public string Sign
        {
            get;
            set;
        }

        public IUnitOfData FetchUnitofData()
        {
            var app = AtawAppContext.Current;
            if (!Name.IsEmpty())
            {
                return app.FetchUnitofData(Name, false);
            }
            if (!Product.IsEmpty())
            {
                return app.FetchUnitofData(Product, app.FControlUnitID, false);
            }

            return app.UnitOfData;
        }

        public string FetchConnString()
        {
            var app = AtawAppContext.Current;
            if (!Name.IsEmpty())
            {
                return app.DBConfigXml.Databases[Name].ConnectionString;
            }
            if (!Product.IsEmpty())
                return DBString.GetAtawControlUnitsProductValue(app.FControlUnitID, Product.Value<ProductsType>(), "DB");
            return app.DefaultConnString;
        }
    }
}
