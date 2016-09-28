using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core.Instance
{
      [CodePlug("ObjectListSource", BaseClass = typeof(IListDataTable),
       CreateDate = "2016-08-23", Author = "zhengyk", Description = "内存对象插件数据源")]
    public class ObjectListSource : ListDataTable
    {
          private IEnumerable<ObjectData> fList;
          protected virtual IEnumerable<ObjectData> GetList()
          {
              return new List<ObjectData>();
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


          public override Type ObjectType
          {
              get;
              set;
          }

          public override string PrimaryKey
          {
              get;
              set;
          }

          public override string GetInsertKey(ObjectData data)
          {
              return "";
          }
    }
}
