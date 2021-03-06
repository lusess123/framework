﻿using System;
using System.Collections.Generic;
using System.Data;

namespace Ataw.Framework.Core
{


    public class EnumCodeTable : HashCodeTable
    {
        public EnumCodeTable(Type type)
        {
            DataList = new Dictionary<string, HashCodeDataModel>();
            //string key = "SELECTOR_ENUM";
            // HasCache = true;
            AtawDebug.Assert(type.IsEnum, "代码表 只能用于枚举", this);
            var arrs = Enum.GetValues(type);
            int count = arrs.Length;
            foreach (var en in arrs)
            {
                if (en.Equals(arrs.GetValue(count - 1)))
                {
                    var cdm = new HashCodeDataModel()
                    {
                        CODE_VALUE = en.Value<int>().ToString(),
                        CODE_TEXT = en.GetDescription(),
                        CODE_NAME = en.ToString(),
                    };
                    DataList.Add(cdm.CODE_VALUE, cdm);
                }
                else
                {
                    if (en.Equals(arrs.GetValue(0)))
                    {
                        var cdm2 = new HashCodeDataModel()
                        {
                            CODE_VALUE = en.Value<int>().ToString(),
                            CODE_TEXT = en.GetDescription(),
                            CODE_NAME = en.ToString(),
                        };
                        DataList.Add(cdm2.CODE_VALUE, cdm2);
                    }
                    else{
                        var cdm = new HashCodeDataModel()
                        {
                            CODE_VALUE = en.Value<int>().ToString(),
                            CODE_TEXT = en.GetDescription(),
                            CODE_NAME = en.ToString(),
                        };
                        DataList.Add(cdm.CODE_VALUE, cdm);
                    }
                }
                // AtawAppContext.Current.AtawCache.Set<EnumCodeDataModel>(KEY_NAME + en.Value<int>().ToString(), cdm);
                //  AtawAppContext.Current.AtawCache.Set<CodeDataModel>(key + en, new CodeDataModel() {  CODE_TEXT});
               
            }
            //AtawAppContext.Current.AtawCache.Set<CodeDataModel>(, DataList);
        }

        public override IEnumerable<CodeDataModel> FillData(DataSet postDataSet)
        {
            foreach (var item in DataList)
            {
                if (item.Key != "0" || item.Value.CODE_NAME.ToUpper() != "NONE")
                {
                    yield return item.Value;
                }
                else
                {
                    continue;
                }
            }
        }

        public override IEnumerable<CodeDataModel> FillAllData(DataSet postDataSet)
        {
            foreach (var item in DataList)
            {
                if (item.Key != "0" || item.Value.CODE_NAME.ToUpper() != "NONE")
                {
                    yield return item.Value;
                }
                else
                {
                    continue;
                }
            }
        }

    }
}
