using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    public class XmlCodeTable : HashCodeTable
    {
        public XmlCodeTable(DataDict dd)
        {
            DataList = new Dictionary<string, HashCodeDataModel>();
            //string key = "SELECTOR_ENUM";
            // HasCache = true;
            //AtawDebug.Assert(type.IsEnum, "代码表 只能用于枚举", this);
            foreach (var en in dd.DDItems)
            {
                var cdm = new HashCodeDataModel()
                {
                    CODE_VALUE = en.Key,
                    CODE_TEXT = en.Value,
                    CODE_NAME = en.Key
                };
                // AtawAppContext.Current.AtawCache.Set<EnumCodeDataModel>(KEY_NAME + en.Value<int>().ToString(), cdm);
                //  AtawAppContext.Current.AtawCache.Set<CodeDataModel>(key + en, new CodeDataModel() {  CODE_TEXT});
                DataList.Add(cdm.CODE_VALUE, cdm);
            }
        }
    }
}
