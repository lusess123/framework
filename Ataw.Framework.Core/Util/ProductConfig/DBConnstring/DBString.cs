using System.Linq;


namespace Ataw.Framework.Core
{
    public class DBString
    {

        public static string CreateRegName(string fControlUnitID, string strProduct)
        {
            if (strProduct.IsEmpty())
            {
                return AtawAppContext.ATAW_DATABASE;
            }
            return string.Format(ObjectUtil.SysCulture, "{0}_{1}_{2}", AtawAppContext.ATAW_DATABASE, fControlUnitID, strProduct);
        }

        public static string GetAtawDatabaseValue(string name)
        {
            var app = AtawAppContext.Current;
            var db = app.DBConfigXml.Databases[name];
            //string res = db.ConnectionString;

            //if (db == null)
            //{
            //    db = app.ProductsXml.Databases[name];
            //    if (db == null)
            //        return null;
            //}
            if (db == null)
            {
                return app.DBConfigXml.Databases.First(a => a.IsDefault).ConnectionString;
            }
            return db.ConnectionString;


        }
        public static string GetAtawControlUnitsProductValue(string fControlUnitID, ProductsType databaseType, string key)
        {
            string productName = databaseType.ToString();
            var database = AtawAppContext.Current.DBConfigXml.Databases.Where(a => a.IsDefault).FirstOrDefault();
            AtawDebug.AssertArgumentNull(database, "默认的数据库配置不能为空", AtawAppContext.Current);
            string strDefaultConnstring = database.ConnectionString;

            if (fControlUnitID.IsEmpty())
                return strDefaultConnstring;

            var xml = AtawAppContext.Current.ProductsXml.ControlUnits;
            var cList = xml.Where(a => a.FControlUnitID == fControlUnitID);
            //IList<ControlUnitItemInfo> m = q.AsQueryable<ControlUnitItemInfo>().ToList();
            if (cList.Count() == 0)
                return strDefaultConnstring;

            string va = "";
            foreach (var item in cList)
            {
                if (!string.IsNullOrEmpty(va))
                {
                    break;
                }
                foreach (var item2 in item.Products)
                {
                    if (item2.Name == productName && item2.Key == key)
                    {
                        va = item2.Value;
                        if (key == "DB")
                        {
                            va = GetAtawDatabaseValue(va); //返回数据库的字符串
                        }
                        break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(va))
                return va;
            else
                return strDefaultConnstring;
            //}
            //catch (Exception)
            //{
            //    return strDefaultConnstring;
            //}

        }
    }
}
