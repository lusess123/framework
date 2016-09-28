using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class RightConfig : IReadXmlCallback
    {
        public FunctionRightConfig FunctionRights { get; set; }

        [XmlArrayItem("ColumnRight")]
        public List<ColumnRightConfig> ColumnRights { get; set; }


        void IReadXmlCallback.OnReadXml()
        {
            if (ColumnRights != null)
            {
                ColumnRights.ForEach(colRight =>
                    {
                        if (colRight.Add != null)
                        {
                            colRight.Add.ForEach(a =>
                                {
                                    if (a.DefaultValue == null)
                                        a.DefaultValueStr = string.Empty;
                                    else if (a.DefaultValue.Value.IsEmpty())
                                        a.DefaultValueStr = string.Empty;
                                    else if (!a.DefaultValue.NeedParse)
                                        a.DefaultValueStr = a.DefaultValue.Value;
                                    else
                                        a.DefaultValueStr = a.DefaultValue.ExeValue();
                                    if (a.InternalShowPage.IsEmpty())
                                        a.ShowPage = PageStyle.All;
                                    else
                                    {
                                        var arr = a.InternalShowPage.Split('|');
                                        arr.ToList().ForEach(str =>
                                        {
                                            a.ShowPage = a.ShowPage | str.ToEnum<PageStyle>();
                                        });
                                    }

                                });
                        }
                    });
            }
        }
    }
}
